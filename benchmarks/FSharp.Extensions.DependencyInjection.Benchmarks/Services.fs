namespace FSharp.Extensions.DependencyInjection.Benchmarks

open Microsoft.Extensions.DependencyInjection
open FSharp.Extensions.DependencyInjection

module Services =
    let makeServiceProvider =
        let inline create () = ServiceCollection()
        let inline inject (services: #IServiceCollection) = services.AddAllInjectedFunctions()
        let inline build (services: #IServiceCollection) = services.BuildServiceProvider()

        create >> inject >> build
