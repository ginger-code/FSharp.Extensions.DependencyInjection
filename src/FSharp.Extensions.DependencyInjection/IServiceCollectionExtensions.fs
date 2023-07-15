namespace FSharp.Extensions.DependencyInjection

open System
open Microsoft.Extensions.DependencyInjection
open FSharp.Extensions.DependencyInjection
open Microsoft.FSharp.Reflection

type FunctionProvider = IServiceProvider
type FunctionCollection = IServiceCollection

[<AutoOpen>]
module IServiceCollectionExtensions =
    let inline private addInjectedFunctions
        (services: #IServiceCollection)
        (injectedFunctions: InjectedFunctionTypeAndFactory seq)
        =
        injectedFunctions
        |> Seq.iter (fun (struct (type', factory)) -> services.AddSingleton(type', factory) |> ignore)

    let inline private addAllInjectedFunctions func (services: #IServiceCollection) =
        func () |> addInjectedFunctions services

    let inline private assertTrue errorMsg pred =
        if not pred then
            failwith errorMsg

    let inline private addFunction (wrapperType: Type) (func: obj) (services: IServiceCollection) =
        (wrapperType.CustomAttributes
         |> Seq.exists (fun attr -> attr.AttributeType = typeof<InjectableFunctionAttribute>))
        |> assertTrue $"The type '{wrapperType.Name}' is not marked with the InjectableFunction attribute"

        (FSharpType.IsUnion wrapperType)
        |> assertTrue $"The type '{wrapperType.Name}' is not a union."

        wrapperType.IsValueType
        |> assertTrue $"The type '{wrapperType.Name}' is not marked with the Struct attribute."

        let unionCases = FSharpType.GetUnionCases wrapperType

        (unionCases.Length = 1)
        |> assertTrue $"The union type '{wrapperType.Name}' must have exactly one case."

        let unionCaseFields = unionCases.[0].GetFields()

        (unionCaseFields.Length = 1)
        |> assertTrue
            $"The case '{unionCases.[0].Name}' for the union type '{wrapperType.Name}' must have exactly one field."

        let unionCaseField = unionCaseFields.[0]

        (FSharpType.IsFunction unionCaseField.PropertyType)
        |> assertTrue
            $"The case '{unionCases.[0].Name}' for the union type '{wrapperType.Name}' must contain a function type."

        services.Add(ServiceDescriptor(wrapperType, func))
        services

    type IServiceCollection with

        /// Add all wrapped functions marked with the InjectedFunction attribute to the service collection
        member this.AddAllInjectedFunctions() =
            addAllInjectedFunctions Probing.findAllInjectedFunctionsByAttribute this
            this

        /// Add all wrapped functions marked with the InjectedFunction attribute to the service collection using the specified degree of parallelism (max parallelism if none provided)
        member this.AddAllInjectedFunctionsParallel(?parallelism) =
            addAllInjectedFunctions (Probing.findAllInjectedFunctionsByAttributeParallel parallelism) this
            this

        member inline this.AddFunction(wrapperType: Type, func) = addFunction wrapperType func this

        member inline this.AddFunctions(functions: obj seq) =
            functions |> Seq.fold (fun sc x -> addFunction (x.GetType()) x sc) this

        /// Adds a non-generic wrapped function to the collection
        member inline this.AddFunction<[<InjectableFunction>] 'wrapper when 'wrapper: struct>(func: 'wrapper) =
            let wrapperType = typeof<'wrapper>
            this.AddFunction(wrapperType, func)

        member inline this.AddFunction<[<InjectableFunction>] 'wrapper
            when 'wrapper: struct and 'wrapper: (static member Implementation: 'wrapper)>
            ()
            =
            this.AddFunction<'wrapper> 'wrapper.Implementation

        static member inline addFunction<[<InjectableFunction>] 'wrapper when 'wrapper: struct>(func: 'wrapper) =
            fun (builder: FunctionCollection) -> builder.AddFunction<'wrapper> func

        static member inline addFunction<[<InjectableFunction>] 'wrapper
            when 'wrapper: struct and 'wrapper: (static member Implementation: 'wrapper)>
            (builder: FunctionCollection)
            =
            builder.AddFunction<'wrapper>()
