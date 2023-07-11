namespace FSharp.Extensions.DependencyInjection

open System
open System.Reflection


module Probing =

    let findAllInjectedFunctionsByAttribute =
        let inline getAllAssemblies () = AppDomain.CurrentDomain.GetAssemblies()

        let inline findInjectedFunctionsInAssembly assemblies =
            let inline filterByAttribute (props: PropertyInfo seq) =
                props
                |> Seq.filter (fun prop ->
                    prop.CustomAttributes
                    |> Seq.exists (fun attr -> attr.AttributeType = typeof<InjectedFunctionAttribute>))

            let getFieldsForAssembly (assembly: Assembly) =
                assembly.DefinedTypes
                |> Seq.collect (fun m -> m.DeclaredProperties |> filterByAttribute)

            Seq.collect getFieldsForAssembly assemblies

        getAllAssemblies >> findInjectedFunctionsInAssembly
