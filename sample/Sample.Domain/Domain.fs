namespace Sample.Domain

type AddOneFunc = int -> int

type SubOneFunc = int -> int

type MulOneFunc = int -> int

module Domain =

    /// Adds one
    let addOne: AddOneFunc = fun x -> x + 1

    /// Subtracts one
    let subOne: SubOneFunc = fun x -> x - 1

    /// Multiplies by one
    let mulOne: MulOneFunc = id
