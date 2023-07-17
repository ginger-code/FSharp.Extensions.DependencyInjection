namespace FSharp.Extensions.DependencyInjection

open System
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.DependencyInjection.Extensions
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

    let inline private assertTrue errorMsg pred =
        if not pred then
            failwith errorMsg

    //todo: Potentially use this snippet as a base for compatible types for generic type parameters for automatic generalization of open generics
    // let allTypesInAssemblies =
    //     AppDomain.CurrentDomain.GetAssemblies()
    //     |> Seq.collect (fun assm -> assm.GetTypes())
    //     |> Seq.filter (fun t ->
    //         // t.cons
    //         (not t.IsAbstract)
    //         && (not t.IsGenericType)
    //         && (not t.IsInterface)
    //         // && t.IsValueType
    //         && t.IsPrimitive
    //         || t.IsClass
    //         || FSharpType.IsRecord t
    //         || FSharpType.IsUnion t
    //
    //     //
    //     )
    //     |> Seq.distinctBy (fun t -> t.FullName)

    /// Implementation for injecting a function wrapper implementation for a type marked with the InjectableFunctionAttribute
    let addFunction (wrapperType: Type) (func: _) (services: IServiceCollection) =
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

        if wrapperType.IsGenericType && not wrapperType.IsConstructedGenericType then
            //todo: Figure out how, if possible, to make constructed instances of the type for all valid types in the assembly, etc.
            failwith "Generic functions must be injected with concrete argument types."
        else
            services.AddScoped(wrapperType, (fun _ -> func))



    type IServiceCollection with

        /// Add all wrapped functions marked with the InjectedFunction attribute to the service collection
        member this.AddAllReflectedInjectedFunctions() =
            Probing.findAllInjectedFunctionsByAttribute () |> addInjectedFunctions this
            this

        /// Add all wrapped functions marked with the InjectedFunction attribute to the service collection using the specified degree of parallelism (max parallelism if none provided)
        member this.AddAllReflectedInjectedFunctionsParallel(?parallelism) =
            (Probing.findAllInjectedFunctionsByAttributeParallel parallelism)
            |> addInjectedFunctions this

            this

        /// Adds a non-generic wrapped function to the collection
        member this.AddFunction(wrapperType: Type, func) = addFunction wrapperType func this

        /// Adds a collection of non-generic wrapped functions to the collection
        member this.AddFunctions(functions: obj seq) =
            functions |> Seq.fold (fun sc x -> addFunction (x.GetType()) x sc) this

        /// Adds a non-generic wrapped function to the collection
        member this.AddFunction<[<InjectableFunction>] 'wrapper when 'wrapper: struct>(func: 'wrapper) =
            addFunction typeof<'wrapper> func this

        /// Adds a non-generic wrapped function to the collection
        member inline this.AddFunction<[<InjectableFunction>] 'wrapper
            when 'wrapper: struct and 'wrapper: (static member Implementation: 'wrapper)>
            ()
            =
            addFunction typeof<'wrapper> 'wrapper.Implementation this

        /// Adds a non-generic wrapped function to the collection
        static member addFunction<[<InjectableFunction>] 'wrapper when 'wrapper: struct>
            (func: 'wrapper)
            (collection: FunctionCollection)
            =
            addFunction typeof<'wrapper> func collection

        /// Adds a non-generic wrapped function to the collection
        static member inline addFunction<[<InjectableFunction>] 'wrapper
            when 'wrapper: struct and 'wrapper: (static member Implementation: 'wrapper)>
            (collection: FunctionCollection)
            =
            addFunction typeof<'wrapper> 'wrapper.Implementation collection
