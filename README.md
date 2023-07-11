# FSharp.Extensions.DependencyInjection

This project is a POC to demonstrate a method of creating typed wrappers for functions and injecting them automatically into an `IServiceCollection` using reflection and marker attributes _without incurring a performance penalty or using any interfaces_.

This works because functions wrapped in single-case struct unions are optimized to into raw function pointers by the compiler- this offers practically equivalent performance as simply calling a function directly! A breakdown of benchmarks supporting this claim are included, below.

Using this strategy, developers can achieve things like obtaining function definitions directly from the `HttpContext` within an `HttpHandler` when using `Giraffe`; these injected functions can be composed before injection, meaning it's completely compatible with functional dependency injection both before injection and after retrieval! 

## Getting started

Define the wrapper type and implementation in your library code:
```fsharp
open FSharp.Extensions.DependencyInjection

/// Define a wrapper struct DU with one case containing a function type
[<Struct>]
type AddOne = AddOne of (int -> int)

module Wrappers =
    /// Create an instance of the wrapper containing an implementation of the function
    [<InjectedFunction>]
    let addOne = (+) 1 |> AddOne

```

Automatically inject the functions from your startup code:

```fsharp
open FSharp.Extensions.DependencyInjection

type Startup() =
    member _.ConfigureServices(services: IServiceCollection) =
        services.AddAllInjectedFunctions() // or services.AddAllInjectedFunctionsParallel()

// ... etc.
```

Retrieve and reference the function in your `HttpHandler` code:

```fsharp
let addOne (i: int) : HttpHandler =
    fun next ctx ->
        let (AddOne addOne) = ctx.GetService()
        let result = addOne i
        text $"{i} + 1 = {result}" next ctx
```



## Performance Considerations

### General Function Call Overhead

| Method | Mean | Error | StdDev | Code Size | Gen0 | Allocated |
| --- | --- | --- | --- | --- | --- | --- |
| RawFunctionPointer | 4.046 ns | 0.0163 ns | 0.0136 ns | 1,094 B | - | - |
| StructDUWrappedFunctionPointer | 4.050 ns | 0.0310 ns | 0.0275 ns | 1,094 B | - | - |
| ClassDUWrappedFunctionPointer | 6.488 ns | 0.0669 ns | 0.0522 ns | 1,126 B | 0.0019 | 24 B |

### Generated Assembly for Function Calls

```assembly
; Benchmarks.FunctionPointerBenchmarks+FunctionPointerBenchmarks.RawFunctionPointer()
       7FF9CFDF4120 push      rsi
       7FF9CFDF4121 sub       rsp,20
       7FF9CFDF4125 mov       rcx,7FF9D01EDE50
       7FF9CFDF412F mov       edx,5
       7FF9CFDF4134 call      CORINFO_HELP_GETSHARED_NONGCSTATIC_BASE
       7FF9CFDF4139 mov       rcx,1D3B0406BA8
       7FF9CFDF4143 mov       rsi,[rcx]
       7FF9CFDF4146 mov       rcx,7FF9D00648B0
       7FF9CFDF4150 mov       edx,5
       7FF9CFDF4155 call      CORINFO_HELP_GETSHARED_NONGCSTATIC_BASE
       7FF9CFDF415A mov       edx,[7FF9D00648EC]
       7FF9CFDF4160 mov       r8d,[7FF9D00648E8]
       7FF9CFDF4167 mov       rcx,rsi
       7FF9CFDF416A add       rsp,20
       7FF9CFDF416E pop       rsi
       7FF9CFDF416F jmp       qword ptr [7FF9D0201678]; Microsoft.FSharp.Core.FSharpFunc`2[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]].InvokeFast[[System.Int32, System.Private.CoreLib]](Microsoft.FSharp.Core.FSharpFunc`2<Int32,Microsoft.FSharp.Core.FSharpFunc`2<Int32,Int32>>, Int32, Int32)
; Total bytes of code 85
```

```assembly
; Benchmarks.FunctionPointerBenchmarks+FunctionPointerBenchmarks.StructDUWrappedFunctionPointer()
       7FF9CFDF4120 push      rsi
       7FF9CFDF4121 sub       rsp,20
;         Add.instance |> fun (Add add) -> add a b
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF9CFDF4125 mov       rcx,7FF9D01EDE50
       7FF9CFDF412F mov       edx,5
       7FF9CFDF4134 call      CORINFO_HELP_GETSHARED_NONGCSTATIC_BASE
       7FF9CFDF4139 mov       rcx,276C3C06BA8
       7FF9CFDF4143 mov       rsi,[rcx]
       7FF9CFDF4146 mov       rcx,7FF9D00648B0
       7FF9CFDF4150 mov       edx,5
       7FF9CFDF4155 call      CORINFO_HELP_GETSHARED_NONGCSTATIC_BASE
       7FF9CFDF415A mov       edx,[7FF9D00648EC]
       7FF9CFDF4160 mov       r8d,[7FF9D00648E8]
       7FF9CFDF4167 mov       rcx,rsi
       7FF9CFDF416A add       rsp,20
       7FF9CFDF416E pop       rsi
       7FF9CFDF416F jmp       qword ptr [7FF9D0201738]; Microsoft.FSharp.Core.FSharpFunc`2[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]].InvokeFast[[System.Int32, System.Private.CoreLib]](Microsoft.FSharp.Core.FSharpFunc`2<Int32,Microsoft.FSharp.Core.FSharpFunc`2<Int32,Int32>>, Int32, Int32)
; Total bytes of code 85
```

```assembly
; Benchmarks.FunctionPointerBenchmarks+FunctionPointerBenchmarks.ClassDUWrappedFunctionPointer()
       7FF9CFDE4120 push      rdi
       7FF9CFDE4121 push      rsi
       7FF9CFDE4122 sub       rsp,28
;         Add2.instance |> fun (Add2 add) -> add a b
;         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF9CFDE4126 mov       rcx,7FF9D01DDE50
       7FF9CFDE4130 mov       edx,5
       7FF9CFDE4135 call      CORINFO_HELP_GETSHARED_NONGCSTATIC_BASE
       7FF9CFDE413A mov       rcx,23DD6406BA8
       7FF9CFDE4144 mov       rsi,[rcx]
       7FF9CFDE4147 mov       rdi,rsi
       7FF9CFDE414A mov       rcx,offset MT_Library.StructFunctions+Add2
       7FF9CFDE4154 call      CORINFO_HELP_NEWSFAST
       7FF9CFDE4159 lea       rcx,[rax+8]
       7FF9CFDE415D mov       rdx,rdi
       7FF9CFDE4160 call      CORINFO_HELP_ASSIGN_REF
       7FF9CFDE4165 mov       rcx,7FF9D00548B0
       7FF9CFDE416F mov       edx,5
       7FF9CFDE4174 call      CORINFO_HELP_GETSHARED_NONGCSTATIC_BASE
       7FF9CFDE4179 mov       edx,[7FF9D00548EC]
       7FF9CFDE417F mov       r8d,[7FF9D00548E8]
       7FF9CFDE4186 mov       rcx,rsi
       7FF9CFDE4189 add       rsp,28
       7FF9CFDE418D pop       rsi
       7FF9CFDE418E pop       rdi
       7FF9CFDE418F jmp       qword ptr [7FF9D01F1720]; Microsoft.FSharp.Core.FSharpFunc`2[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]].InvokeFast[[System.Int32, System.Private.CoreLib]](Microsoft.FSharp.Core.FSharpFunc`2<Int32,Microsoft.FSharp.Core.FSharpFunc`2<Int32,Int32>>, Int32, Int32)
; Total bytes of code 117
```
