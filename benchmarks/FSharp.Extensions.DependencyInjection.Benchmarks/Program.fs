open BenchmarkDotNet.Running

open FSharp.Extensions.DependencyInjection.Benchmarks

BenchmarkRunner.Run<Benchmarks>() |> ignore
