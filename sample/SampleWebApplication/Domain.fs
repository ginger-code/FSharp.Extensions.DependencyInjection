namespace SampleWebApplication

type AddOneFunc = int -> int

type SubOneFunc = int -> int

module Domain =

    /// Adds one
    let addOne: AddOneFunc = fun x -> x + 1

    /// Subtracts one
    let subOne: SubOneFunc = fun x -> x - 1