namespace SampleWebApplication

type AddOneFunc = int -> int

type SubOneFunc = int -> int

module Domain =

    /// Adds one
    let addOne: AddOneFunc = (+) 1

    /// Subtracts one
    let subOne: SubOneFunc = (-) 1
