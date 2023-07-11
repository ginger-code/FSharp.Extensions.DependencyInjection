open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open FSharp.Extensions.DependencyInjection
open Giraffe

open Sample.WebApplication


let private webApp =
    choose
        [ routef "/increment/%i" Handlers.addOne
          routef "/decrement/%i" Handlers.subOne ]

type private Startup() =
    member _.ConfigureServices(services: IServiceCollection) =
        services.AddGiraffe().AddAllInjectedFunctions()

    member _.Configure (app: IApplicationBuilder) (_: IHostEnvironment) (_: ILoggerFactory) = app.UseGiraffe webApp

let run () =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun webHostBuilder -> webHostBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()
