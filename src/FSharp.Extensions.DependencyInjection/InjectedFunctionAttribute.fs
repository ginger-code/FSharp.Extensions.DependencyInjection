namespace FSharp.Extensions.DependencyInjection

open System


[<AttributeUsage(AttributeTargets.Property)>]
type InjectedFunctionAttribute() =
    inherit Attribute()
