namespace FSharp.Extensions.DependencyInjection

open System
open System.Reflection
open FSharp.Collections.ParallelSeq

module Probing =
    /// Ensure that the discovered properties have the InjectedFunction flag
    let inline private filterPropertiesByAttribute seqFilter props =
        /// Ensure that the discovered property has the InjectedFunction flag
        let inline hasInjectedFunctionAttribute (prop: PropertyInfo) =
            prop.CustomAttributes
            |> Seq.exists (fun attr -> attr.AttributeType = typeof<InjectedFunctionAttribute>)

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
            |> Seq.collect (fun m -> m.DeclaredProperties |> (filterPropertiesByAttribute seqFilter))

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
        | None -> PSeq.ofArray
        |> findAllInjectedFunctionsByAttribute' PSeq.collect PSeq.filter PSeq.map
