open Microsoft.Extensions.DependencyInjection
open FSharp.Extensions.DependencyInjection

open Sample.Domain.Wrappers


printfn "Running demo"
printfn "Creating service collection"
let serviceCollection = ServiceCollection()
printfn "Asserting service collection is empty"
assert (serviceCollection.Count = 0)

if serviceCollection.Count <> 0 then
    failwith "Service collection should have been empty"

printfn "Adding all injected functions to service collection"
serviceCollection.AddAllInjectedFunctionsParallel()
printfn "Asserting service collection has 2 functions injected"
assert (serviceCollection.Count = 2)

if serviceCollection.Count <> 2 then
    failwith "Service collection should have 2 functions injected"

printfn "Building service provider"
let provider = serviceCollection.BuildServiceProvider()
printfn "Retrieving AddOne function from service provider"
let (AddOne addOne) = provider.GetService()
printfn "Calling AddOne with 1"
let two = addOne 1
printfn "Asserting that the result of AddOne on 1 is 2"
assert (two = 2)

if two <> 2 then
    failwith $"AddOne 1 should have been 2, but was {two}"

printfn "Successfully completed demo!"
