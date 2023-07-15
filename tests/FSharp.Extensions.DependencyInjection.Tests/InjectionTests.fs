module FSharp.Extensions.DependencyInjection.Tests.InjectionTests

open Expecto
open FSharp.Extensions.DependencyInjection
open Microsoft.Extensions.DependencyInjection


[<Struct; InjectableFunction>]
type IntegerAdditionFunction = IntegerAdditionFunction of func: (int -> int -> int)

[<Struct; InjectableFunction>]
type IntegerMultiplicationFunction = IntegerMultiplicationFunction of func: (int -> int -> int)

[<Struct>]
type FloatAdditionFunction = FloatAdditionFunction of func: (float -> float -> float)

[<Tests>]
let functionInjectionTests =
    testList
        "Function Injection"
        [ testCase "Can be injected into service collection if marked with InjectableFunctionAttribute"
          <| fun _ ->
              let serviceCollection = ServiceCollection()
              let functionDefinition = IntegerAdditionFunction (+)
              let _ = serviceCollection.AddFunction(functionDefinition)
              ()

          testCase "Cannot be injected into service collection if not marked with InjectableFunctionAttribute"
          <| fun _ ->
              let serviceCollection = ServiceCollection()
              let functionDefinition = FloatAdditionFunction (+)

              Expect.throws
                  (fun _ -> serviceCollection.AddFunction(functionDefinition) |> ignore)
                  "Function wrapper without InjectableFunctionAttribute was injected"

              ()

          testCase "Can be retrieved from built service provider"
          <| fun _ ->
              let serviceCollection = ServiceCollection()
              let functionDefinition = IntegerAdditionFunction (+)

              let serviceProvider =
                  serviceCollection
                      .AddFunction<IntegerAdditionFunction>(functionDefinition)
                      .BuildServiceProvider()

              let (IntegerAdditionFunction func) =
                  serviceProvider.GetService<IntegerAdditionFunction>()

              let result = func 1 1
              Expect.equal result 2 "Integer addition function was not retrieved correctly from the provider"

          testCase "Multiple can be retrieved from built service provider"
          <| fun _ ->
              let serviceCollection = ServiceCollection()
              let additionFunction = IntegerAdditionFunction (+)
              let multiplicationFunction = IntegerMultiplicationFunction (*)

              let serviceProvider =
                  serviceCollection
                      .AddFunctions([ additionFunction; multiplicationFunction ])
                      .BuildServiceProvider()

              let (IntegerAdditionFunction add) =
                  serviceProvider.GetService<IntegerAdditionFunction>()

              let (IntegerMultiplicationFunction mul) =
                  serviceProvider.GetService<IntegerMultiplicationFunction>()

              let result = mul 5 5 |> add 2
              Expect.equal result 27 "Integer addition function was not retrieved correctly from the provider"

          ]
