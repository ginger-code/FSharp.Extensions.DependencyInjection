namespace Sample.Domain.Wrappers

open FSharp.Extensions.DependencyInjection

open Sample.Domain

/// Wrapper for AddOneFunc
[<Struct>]
type AddOne = AddOne of AddOneFunc

/// Wrapper for SubOneFunc
[<Struct>]
type SubOne = SubOne of SubOneFunc

/// <summary>
/// Wrapper for AddOneFunc *or* AddOneFunc >> AddOneFunc. <br/>
/// NOTE: This DU has multiple cases, so it won't be injected.
/// </summary>
[<Struct>]
type Increment =
    | IncreaseOne of one: AddOneFunc
    | IncreaseTwo of two: AddOneFunc

/// <summary>
/// Wrapper for MulOneFunc <br/>
/// NOTE: This is not marked as a struct, so it won't be injected. <br/>
/// Without the struct attribute, the wrapped function will not be treated as a raw function pointer and will degrade performance.
/// </summary>
type MulOne = MulOne of MulOneFunc

///<summary>
/// A single-case struct DU. <br/>
/// NOTE: The DU case does not contain a function type, so it won't be injected.
/// </summary>
[<Struct>]
type SomeDU = SingleCase of int

[<Struct>]
type TwoFieldDU = TwoField of (int -> int) * (int -> int)

module Wrappers =
    /// Injected function in wrapper
    [<InjectedFunction>]
    let addOne = Domain.addOne |> AddOne

    /// Injected function in wrapper
    [<InjectedFunction>]
    let subOne = Domain.subOne |> SubOne

// Not a single-case DU- this will throw an error at startup!
//[<InjectedFunction>]
//let addSome = Domain.addOne >> Domain.addOne |> IncreaseTwo

// Not a struct DU- this will throw an error at startup!
// [<InjectedFunction>]
// let mulOne = Domain.mulOne |> MulOne

// Not a DU case with a single field- this will throw an error at startup!
// [<InjectedFunction>]
// let twoField = TwoField(id, id)

// Not a function DU- this will throw an error at startup!
// [<InjectedFunction>]
// let singleCase = 1 |> SingleCase
