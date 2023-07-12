namespace FSharp.Extensions.DependencyInjection.Benchmarks

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Engines
open Microsoft.Extensions.DependencyInjection

module Helper =
    let inline testAdd ([<InlineIfLambda>] addFunc) : int = addFunc 5000 10000
    let inline testAdd3 ([<InlineIfLambda>] addFunc) : int = addFunc 5000 10000 4
    let inline testIncrement ([<InlineIfLambda>] incrementFunc) : int = incrementFunc 300

[<SimpleJob(RunStrategy.Throughput, id = "Throughput");
  MemoryDiagnoser;
  DisassemblyDiagnoser(printSource = true,
                       printInstructionAddresses = true,
                       exportGithubMarkdown = true,
                       exportCombinedDisassemblyReport = true)>]
type Benchmarks() =
    let mutable services = Services.makeServiceProvider ()
    let (AddFunction addPrefetched) = services.GetService()

    [<Benchmark(Description = "Raw Function Call", OperationsPerInvoke = 1000)>]
    member this.RawFunctionPointer() = Domain.add |> Helper.testAdd

    [<Benchmark(Description = "Raw Lambda Call", OperationsPerInvoke = 1000)>]
    member this.RawLambdaCall() =
        (fun x y z -> x + Domain.add y z) |> Helper.testAdd3

    [<Benchmark(Description = "Raw Curried Call", OperationsPerInvoke = 1000)>]
    member this.RawCurriedClosure() = Domain.add 1 |> Helper.testIncrement

    [<Benchmark(Description = "Wrapped Function Call (Direct access)", OperationsPerInvoke = 1000)>]
    member this.WrappedFunctionPointerFP() =
        let (AddFunction add) = Wrappers.add
        add |> Helper.testAdd

    [<Benchmark(Description = "Wrapped Lambda Call (Direct access)", OperationsPerInvoke = 1000)>]
    member this.WrappedFunctionPointerLambda() =
        let (Add3Function add) = Wrappers.add3
        add |> Helper.testAdd3

    [<Benchmark(Description = "Wrapped Curried Call (Direct access)", OperationsPerInvoke = 1000)>]
    member this.WrappedFunctionPointerClosure() =
        let (Increment inc) = Wrappers.increment
        inc |> Helper.testIncrement

    [<Benchmark(Description = "Injected Wrapped Function Call (Pre-Fetched)", OperationsPerInvoke = 1000)>]
    member this.PreFetchedInjectedWrappedFunctionPointer() = addPrefetched |> Helper.testAdd
