open System.Reflection
open FSharp.Extensions.DependencyInjection
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

let webApp = choose [ route "/ping" >=> text "pong" ]

type Startup() =
    member _.ConfigureServices(services: IServiceCollection) = services.AddGiraffe() |> ignore

    member _.Configure (app: IApplicationBuilder) (env: IHostEnvironment) (loggerFactory: ILoggerFactory) =
        app.UseGiraffe webApp

let run () =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun webHostBuilder -> webHostBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()

let test () =
    Probing.findAllInjectedFunctionsByAttribute ()
    |> Seq.iter (printfn "%O")

test ()
// let assm = Assembly.GetEntryAssembly()
// assm.DefinedTypes
// |> Seq.filter (fun t -> t.Name.Contains("Wrappers"))
// |> Seq.collect (fun t -> t.DeclaredProperties)
// |> Seq.iter (printfn "%O")
