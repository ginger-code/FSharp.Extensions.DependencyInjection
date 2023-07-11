module SampleWebApplication.Program

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open FSharp.Extensions.DependencyInjection
open Giraffe

let webApp =
    choose
        [ routef "/increment/%i" Handlers.addOne
          routef "/decrement/%i" Handlers.subOne ]

type Startup() =
    member _.ConfigureServices(services: IServiceCollection) =
        services.AddGiraffe().AddAllInjectedFunctionsParallel()

    member _.Configure (app: IApplicationBuilder) (_: IHostEnvironment) (_: ILoggerFactory) = app.UseGiraffe webApp

let run () =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun webHostBuilder -> webHostBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()

let test () =
    let serviceCollection = ServiceCollection()
    assert (serviceCollection.Count = 0)
    serviceCollection.AddAllInjectedFunctionsParallel()
    assert (serviceCollection.Count = 2)
    ()

test ()
