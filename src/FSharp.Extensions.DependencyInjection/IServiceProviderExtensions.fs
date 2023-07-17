namespace FSharp.Extensions.DependencyInjection

open System
open Microsoft.Extensions.DependencyInjection
open FSharp.Extensions.DependencyInjection

[<AutoOpen>]
module IServiceProviderExtensions =
    type IServiceProvider with

        member inline this.GetFunction<[<InjectableFunction>] 'wrapper when 'wrapper: struct>() =
            this.GetService<'wrapper>()

        static member inline getFunction<[<InjectableFunction>] 'wrapper when 'wrapper: struct>
            (provider: IServiceProvider)
            =
            provider.GetFunction<'wrapper>()
