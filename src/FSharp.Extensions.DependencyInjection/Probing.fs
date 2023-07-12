namespace FSharp.Extensions.DependencyInjection

open System
open System.Reflection
open FSharp.Collections.ParallelSeq
open Microsoft.FSharp.Reflection

module Probing =

    let inline private check ([<InlineIfLambda>] f: Type -> bool) msg t =
        if f t then
            true
        else
            failwith $"'{t.FullName}' is not a valid use of the InjectedFunction attribute: {msg}"

    /// Ensure that the discovered properties have the InjectedFunction flag
    let inline private filterValidProperties seqFilter props =
        /// Ensure that the discovered property is a struct-wrapped function in a single-case DU
        let inline isOfStructWrapperUnionTypeWithSingleFunctionCase (type': Type) =
            let isStruct = check (fun t -> t.IsValueType) "Type must be a struct"
            let isDU = check FSharpType.IsUnion "Type must be a discriminated union"

            let isValidDU t =
                let cases = FSharpType.GetUnionCases t

                let hasOneCase =
                    t |> check (fun _ -> cases.Length = 1) "Union must have exactly one case"

                if hasOneCase then
                    let fields =
                        cases |> Array.map (fun caseField -> caseField.GetFields()) |> Array.head

                    let caseHasOneField =
                        t |> check (fun _ -> fields.Length = 1) "Union case must have exactly one field"

                    if caseHasOneField then
                        let caseField = fields |> Array.head

                        let caseFieldIsFunction =
                            caseField.PropertyType
                            |> check FSharpType.IsFunction "Union case field must be a function type"

                        caseFieldIsFunction
                    else
                        false
                else
                    false

            isStruct type' && isDU type' && isValidDU type'

        /// Ensure that the discovered property has the InjectedFunction flag
        let inline hasInjectedFunctionAttribute (prop: PropertyInfo) =
            prop.CustomAttributes
            // Has attribute
            |> Seq.exists (fun attr -> attr.AttributeType = typeof<InjectedFunctionAttribute>)
            // Is a value of a single-case DU struct
            && isOfStructWrapperUnionTypeWithSingleFunctionCase prop.PropertyType

        props |> seqFilter hasInjectedFunctionAttribute

    /// Create struct tuples of the function-pointer wrapper type and its instantiated value for each property
    let inline private createTypeValuePairs seqMap props =
        /// Create a struct tuple of the function-pointer wrapper type and its instantiated value
        let inline createTypeValuePair (prop: PropertyInfo) =
            struct (prop.PropertyType, prop.GetMethod.Invoke(null, [||]))

        props |> seqMap createTypeValuePair

    /// Retrieve all assemblies currently loaded
    let inline private getAllAssemblies seqOfArray =
        AppDomain.CurrentDomain.GetAssemblies >> seqOfArray

    /// Scan assembly for types with static properties marked with InjectedFunctionAttribute
    let inline private findInjectedFunctionsInAssembly seqCollect seqFilter assemblies =
        let getFieldsForAssembly (assembly: Assembly) =
            assembly.DefinedTypes
            |> Seq.collect (fun m -> m.DeclaredProperties |> (filterValidProperties seqFilter))

        seqCollect getFieldsForAssembly assemblies

    /// Base function implementation, accepts functions for working with seq or pseq
    let inline private findAllInjectedFunctionsByAttribute' seqCollect seqFilter seqMap seqOfArray =
        (getAllAssemblies seqOfArray)
        >> (findInjectedFunctionsInAssembly seqCollect seqFilter)
        >> (createTypeValuePairs seqMap)

    /// Scan all loaded assemblies for wrapped function pointers for dependency injection sequentially
    let findAllInjectedFunctionsByAttribute =
        findAllInjectedFunctionsByAttribute' Seq.collect Seq.filter Seq.map Seq.ofArray

    /// Scan all loaded assemblies for wrapped function pointers for dependency injection in parallel
    let inline findAllInjectedFunctionsByAttributeParallel parallelism =
        match parallelism with
        | Some parallelism -> PSeq.ofArray >> PSeq.withDegreeOfParallelism parallelism
        | None -> PSeq.ofArray >> PSeq.withDegreeOfParallelism Environment.ProcessorCount
        |> findAllInjectedFunctionsByAttribute' PSeq.collect PSeq.filter PSeq.map
