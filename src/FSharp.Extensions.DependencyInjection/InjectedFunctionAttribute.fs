namespace FSharp.Extensions.DependencyInjection

open System


/// Marks a struct-wrapped function-pointer for addition to a dependency injection container
[<AttributeUsage(AttributeTargets.Property)>]
type InjectedFunctionAttribute() =
    inherit Attribute()


//TODO: Currently, this does not work with DUs with a generic type parameter. Figure out if that's possible and how.