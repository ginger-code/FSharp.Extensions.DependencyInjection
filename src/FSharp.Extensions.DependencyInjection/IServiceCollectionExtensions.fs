namespace FSharp.Extensions.DependencyInjection

open System
open Microsoft.Extensions.DependencyInjection

[<AutoOpen>]
module IServiceCollectionExtensions =
    let inline private addInjectedFunctions
        (services: #IServiceCollection)
        (injectedFunctions: (struct (Type * obj)) seq)
        =
        injectedFunctions
        |> Seq.iter (fun (struct (type', instance)) -> services.AddSingleton(type', instance) |> ignore)

    let inline private addAllInjectedFunctions func (services: #IServiceCollection) =
        func () |> addInjectedFunctions services

    type IServiceCollection with

        /// Add all wrapped functions marked with the InjectedFunction attribute to the service collection
        member this.AddAllInjectedFunctions() =
            addAllInjectedFunctions Probing.findAllInjectedFunctionsByAttribute this

        /// Add all wrapped functions marked with the InjectedFunction attribute to the service collection using the specified degree of parallelism (max parallelism if none provided)
        member this.AddAllInjectedFunctionsParallel(?parallelism) =
            addAllInjectedFunctions (Probing.findAllInjectedFunctionsByAttributeParallel parallelism) this