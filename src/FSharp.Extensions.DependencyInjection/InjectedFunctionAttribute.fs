namespace FSharp.Extensions.DependencyInjection

open System


/// Marks a struct-wrapped function-pointer for addition to a dependency injection container
[<AttributeUsage(AttributeTargets.Property)>]
type InjectedFunctionAttribute() =
    inherit Attribute()
