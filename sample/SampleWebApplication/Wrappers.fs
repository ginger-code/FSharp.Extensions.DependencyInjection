namespace SampleWebApplication

open FSharp.Extensions.DependencyInjection

/// Wrapper for AddOneFunc
[<Struct>]
type AddOne = AddOne of AddOneFunc

/// Wrapper for SubOneFunc
[<Struct>]
type SubOne = SubOne of SubOneFunc

module Wrappers =
    /// Injected function in wrapper
    [<InjectedFunction>]
    let addOne = Domain.addOne |> AddOne

    /// Injected function in wrapper
    [<InjectedFunction>]
    let subOne = Domain.subOne |> SubOne
