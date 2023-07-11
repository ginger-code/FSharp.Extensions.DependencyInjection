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

    let inline getAllAssemblies seqOfArray =
        AppDomain.CurrentDomain.GetAssemblies >> seqOfArray

    let inline findInjectedFunctionsInAssembly seqCollect seqFilter assemblies =
        let getFieldsForAssembly (assembly: Assembly) =
            assembly.DefinedTypes
            |> Seq.collect (fun m -> m.DeclaredProperties |> (filterPropertiesByAttribute seqFilter))

        seqCollect getFieldsForAssembly assemblies

    let inline private findAllInjectedFunctionsByAttribute' seqCollect seqFilter seqMap seqOfArray =
        (getAllAssemblies seqOfArray)
        >> (findInjectedFunctionsInAssembly seqCollect seqFilter)
        >> (createTypeValuePairs seqMap)

    let findAllInjectedFunctionsByAttribute =
        findAllInjectedFunctionsByAttribute' Seq.collect Seq.filter Seq.map Seq.ofArray

    let findAllInjectedFunctionsByAttributeParallel parallelism =
        match parallelism with
        | Some parallelism -> PSeq.ofArray >> PSeq.withDegreeOfParallelism parallelism
        | None -> PSeq.ofArray
        |> findAllInjectedFunctionsByAttribute' PSeq.collect PSeq.filter PSeq.map
