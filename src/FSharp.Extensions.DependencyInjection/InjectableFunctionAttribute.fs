namespace FSharp.Extensions.DependencyInjection

open System

/// Marks a struct-wrapped function-pointer as valid for addition to a dependency injection container
[<AttributeUsage(AttributeTargets.Struct ||| AttributeTargets.GenericParameter)>]
type InjectableFunctionAttribute() =
    inherit Attribute()
