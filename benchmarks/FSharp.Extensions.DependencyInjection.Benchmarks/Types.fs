namespace FSharp.Extensions.DependencyInjection.Benchmarks

open FSharp.Extensions.DependencyInjection

[<Struct>]
type AddFunction = AddFunction of (int -> int -> int)

[<Struct>]
type Add3Function = Add3Function of (int -> int -> int -> int)

[<Struct>]
type Increment = Increment of (int -> int)

module Domain =

    /// Generic curried add function
    let inline add x y = x + y

module Wrappers =
    [<InjectedFunction>]
    let add = Domain.add |> AddFunction

    [<InjectedFunction>]
    let add3 = (fun x y z -> x + Domain.add y z) |> Add3Function

    [<InjectedFunction>]
    let increment = Domain.add 1 |> Increment
