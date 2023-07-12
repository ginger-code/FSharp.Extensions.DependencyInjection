## .NET Core 7.0.9 (CoreCLR 7.0.923.32018, CoreFX 7.0.923.32018), X64 RyuJIT
```assembly
; FSharp.Extensions.DependencyInjection.Benchmarks.Benchmarks.RawFunctionPointer()
       7FF9EAE281C0 mov       eax,3A98
       7FF9EAE281C5 ret
; Total bytes of code 6
```

## .NET Core 7.0.9 (CoreCLR 7.0.923.32018, CoreFX 7.0.923.32018), X64 RyuJIT
```assembly
; FSharp.Extensions.DependencyInjection.Benchmarks.Benchmarks.RawLambdaCall()
       7FF9EAE181C0 mov       eax,3A9C
       7FF9EAE181C5 ret
; Total bytes of code 6
```

## .NET Core 7.0.9 (CoreCLR 7.0.923.32018, CoreFX 7.0.923.32018), X64 RyuJIT
```assembly
; FSharp.Extensions.DependencyInjection.Benchmarks.Benchmarks.RawCurriedClosure()
       7FF9EAE081C0 mov       eax,12D
       7FF9EAE081C5 ret
; Total bytes of code 6
```

## .NET Core 7.0.9 (CoreCLR 7.0.923.32018, CoreFX 7.0.923.32018), X64 RyuJIT
```assembly
; FSharp.Extensions.DependencyInjection.Benchmarks.Benchmarks.WrappedFunctionPointerFP()
       7FF9EAE06250 sub       rsp,28
       7FF9EAE06254 call      qword ptr [7FF9EAE07C90]
       7FF9EAE0625A mov       rcx,rax
       7FF9EAE0625D mov       edx,1388
       7FF9EAE06262 mov       r8d,2710
       7FF9EAE06268 add       rsp,28
       7FF9EAE0626C jmp       qword ptr [7FF9EAE0F048]
; Total bytes of code 34
```
```assembly
; FSharp.Extensions.DependencyInjection.Benchmarks.Wrappers.get_add()
       7FF9EAE08310 mov       rax,1FD8AC062E8
       7FF9EAE0831A mov       rax,[rax]
       7FF9EAE0831D mov       rax,[rax+8]
       7FF9EAE08321 ret
; Total bytes of code 18
```
```assembly
; Microsoft.FSharp.Core.FSharpFunc`2[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]].InvokeFast[[System.Int32, System.Private.CoreLib]](Microsoft.FSharp.Core.FSharpFunc`2<Int32,Microsoft.FSharp.Core.FSharpFunc`2<Int32,Int32>>, Int32, Int32)
       7FF9EAE06290 push      rdi
       7FF9EAE06291 push      rsi
       7FF9EAE06292 push      rbx
       7FF9EAE06293 sub       rsp,20
       7FF9EAE06297 mov       rsi,rcx
       7FF9EAE0629A mov       edi,edx
       7FF9EAE0629C mov       ebx,r8d
       7FF9EAE0629F mov       rdx,rsi
       7FF9EAE062A2 mov       rcx,offset MT_Microsoft.FSharp.Core.OptimizedClosures+FSharpFunc`3[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]]
       7FF9EAE062AC call      qword ptr [7FF9EAE0B828]
       7FF9EAE062B2 test      rax,rax
       7FF9EAE062B5 je        short M02_L00
       7FF9EAE062B7 mov       rcx,rax
       7FF9EAE062BA mov       edx,edi
       7FF9EAE062BC mov       r8d,ebx
       7FF9EAE062BF mov       rax,[rax]
       7FF9EAE062C2 mov       rax,[rax+40]
       7FF9EAE062C6 add       rsp,20
       7FF9EAE062CA pop       rbx
       7FF9EAE062CB pop       rsi
       7FF9EAE062CC pop       rdi
       7FF9EAE062CD jmp       qword ptr [rax+28]
M02_L00:
       7FF9EAE062D1 mov       rcx,rsi
       7FF9EAE062D4 mov       edx,edi
       7FF9EAE062D6 mov       rax,[rsi]
       7FF9EAE062D9 mov       rax,[rax+40]
       7FF9EAE062DD call      qword ptr [rax+20]
       7FF9EAE062E0 mov       rcx,rax
       7FF9EAE062E3 mov       edx,ebx
       7FF9EAE062E5 mov       rax,[rax]
       7FF9EAE062E8 mov       rax,[rax+40]
       7FF9EAE062EC add       rsp,20
       7FF9EAE062F0 pop       rbx
       7FF9EAE062F1 pop       rsi
       7FF9EAE062F2 pop       rdi
       7FF9EAE062F3 jmp       qword ptr [rax+20]
; Total bytes of code 103
```

## .NET Core 7.0.9 (CoreCLR 7.0.923.32018, CoreFX 7.0.923.32018), X64 RyuJIT
```assembly
; FSharp.Extensions.DependencyInjection.Benchmarks.Benchmarks.WrappedFunctionPointerLambda()
       7FF9EAE16250 sub       rsp,28
       7FF9EAE16254 call      qword ptr [7FF9EAE17CA8]
       7FF9EAE1625A mov       rcx,rax
       7FF9EAE1625D mov       edx,1388
       7FF9EAE16262 mov       r8d,2710
       7FF9EAE16268 mov       r9d,4
       7FF9EAE1626E add       rsp,28
       7FF9EAE16272 jmp       qword ptr [7FF9EAE1F048]
; Total bytes of code 40
```
```assembly
; FSharp.Extensions.DependencyInjection.Benchmarks.Wrappers.get_add3()
       7FF9EAE18320 mov       rax,2093E8062F0
       7FF9EAE1832A mov       rax,[rax]
       7FF9EAE1832D mov       rax,[rax+8]
       7FF9EAE18331 ret
; Total bytes of code 18
```
```assembly
; Microsoft.FSharp.Core.FSharpFunc`2[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]].InvokeFast[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]](Microsoft.FSharp.Core.FSharpFunc`2<Int32,Microsoft.FSharp.Core.FSharpFunc`2<Int32,Microsoft.FSharp.Core.FSharpFunc`2<Int32,Int32>>>, Int32, Int32, Int32)
       7FF9EAE16290 push      rdi
       7FF9EAE16291 push      rsi
       7FF9EAE16292 push      rbp
       7FF9EAE16293 push      rbx
       7FF9EAE16294 sub       rsp,28
       7FF9EAE16298 mov       rsi,rcx
       7FF9EAE1629B mov       edi,edx
       7FF9EAE1629D mov       ebx,r8d
       7FF9EAE162A0 mov       ebp,r9d
       7FF9EAE162A3 mov       rdx,rsi
       7FF9EAE162A6 mov       rcx,offset MT_Microsoft.FSharp.Core.OptimizedClosures+FSharpFunc`4[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]]
       7FF9EAE162B0 call      qword ptr [7FF9EAE1B828]
       7FF9EAE162B6 test      rax,rax
       7FF9EAE162B9 jne       short M02_L00
       7FF9EAE162BB mov       rdx,rsi
       7FF9EAE162BE mov       rcx,offset MT_Microsoft.FSharp.Core.OptimizedClosures+FSharpFunc`3[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib],[Microsoft.FSharp.Core.FSharpFunc`2[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]], FSharp.Core]]
       7FF9EAE162C8 call      qword ptr [7FF9EAE1B828]
       7FF9EAE162CE test      rax,rax
       7FF9EAE162D1 je        short M02_L04
       7FF9EAE162D3 jmp       short M02_L02
M02_L00:
       7FF9EAE162D5 mov       rcx,rsi
       7FF9EAE162D8 mov       rdx,offset MT_Microsoft.FSharp.Core.OptimizedClosures+FSharpFunc`4[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]]
       7FF9EAE162E2 cmp       [rcx],rdx
       7FF9EAE162E5 je        short M02_L01
       7FF9EAE162E7 mov       rcx,rsi
M02_L01:
       7FF9EAE162EA mov       edx,edi
       7FF9EAE162EC mov       r8d,ebx
       7FF9EAE162EF mov       r9d,ebp
       7FF9EAE162F2 mov       rax,[rcx]
       7FF9EAE162F5 mov       rax,[rax+40]
       7FF9EAE162F9 add       rsp,28
       7FF9EAE162FD pop       rbx
       7FF9EAE162FE pop       rbp
       7FF9EAE162FF pop       rsi
       7FF9EAE16300 pop       rdi
       7FF9EAE16301 jmp       qword ptr [rax+28]
M02_L02:
       7FF9EAE16305 mov       rcx,rsi
       7FF9EAE16308 mov       rdx,offset MT_Microsoft.FSharp.Core.OptimizedClosures+FSharpFunc`3[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib],[Microsoft.FSharp.Core.FSharpFunc`2[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]], FSharp.Core]]
       7FF9EAE16312 cmp       [rcx],rdx
       7FF9EAE16315 je        short M02_L03
       7FF9EAE16317 mov       rcx,rsi
M02_L03:
       7FF9EAE1631A mov       edx,edi
       7FF9EAE1631C mov       r8d,ebx
       7FF9EAE1631F mov       rax,[rcx]
       7FF9EAE16322 mov       rax,[rax+40]
       7FF9EAE16326 call      qword ptr [rax+28]
       7FF9EAE16329 mov       rcx,rax
       7FF9EAE1632C mov       edx,ebp
       7FF9EAE1632E mov       rax,[rax]
       7FF9EAE16331 mov       rax,[rax+40]
       7FF9EAE16335 add       rsp,28
       7FF9EAE16339 pop       rbx
       7FF9EAE1633A pop       rbp
       7FF9EAE1633B pop       rsi
       7FF9EAE1633C pop       rdi
       7FF9EAE1633D jmp       qword ptr [rax+20]
M02_L04:
       7FF9EAE16341 mov       rcx,rsi
       7FF9EAE16344 mov       edx,edi
       7FF9EAE16346 mov       rax,[rsi]
       7FF9EAE16349 mov       rax,[rax+40]
       7FF9EAE1634D call      qword ptr [rax+20]
       7FF9EAE16350 mov       rcx,rax
       7FF9EAE16353 mov       edx,ebx
       7FF9EAE16355 mov       r8d,ebp
       7FF9EAE16358 add       rsp,28
       7FF9EAE1635C pop       rbx
       7FF9EAE1635D pop       rbp
       7FF9EAE1635E pop       rsi
       7FF9EAE1635F pop       rdi
       7FF9EAE16360 jmp       qword ptr [7FF9EAE1F480]
; Total bytes of code 214
```

## .NET Core 7.0.9 (CoreCLR 7.0.923.32018, CoreFX 7.0.923.32018), X64 RyuJIT
```assembly
; FSharp.Extensions.DependencyInjection.Benchmarks.Benchmarks.WrappedFunctionPointerClosure()
       7FF9EADF6250 sub       rsp,28
       7FF9EADF6254 call      qword ptr [7FF9EADF7CC0]
       7FF9EADF625A mov       rcx,rax
       7FF9EADF625D mov       edx,12C
       7FF9EADF6262 mov       rax,[rax]
       7FF9EADF6265 mov       rax,[rax+40]
       7FF9EADF6269 add       rsp,28
       7FF9EADF626D jmp       qword ptr [rax+20]
; Total bytes of code 33
```
```assembly
; FSharp.Extensions.DependencyInjection.Benchmarks.Wrappers.get_increment()
       7FF9EADF8200 mov       rax,2371A8062F8
       7FF9EADF820A mov       rax,[rax]
       7FF9EADF820D mov       rax,[rax+8]
       7FF9EADF8211 ret
; Total bytes of code 18
```

## .NET Core 7.0.9 (CoreCLR 7.0.923.32018, CoreFX 7.0.923.32018), X64 RyuJIT
```assembly
; FSharp.Extensions.DependencyInjection.Benchmarks.Benchmarks.PreFetchedInjectedWrappedFunctionPointer()
       7FF9EAE06250 mov       rcx,[rcx+10]
       7FF9EAE06254 mov       edx,1388
       7FF9EAE06259 mov       r8d,2710
       7FF9EAE0625F jmp       qword ptr [7FF9EAE0F048]
; Total bytes of code 21
```
```assembly
; Microsoft.FSharp.Core.FSharpFunc`2[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]].InvokeFast[[System.Int32, System.Private.CoreLib]](Microsoft.FSharp.Core.FSharpFunc`2<Int32,Microsoft.FSharp.Core.FSharpFunc`2<Int32,Int32>>, Int32, Int32)
       7FF9EAE06280 push      rdi
       7FF9EAE06281 push      rsi
       7FF9EAE06282 push      rbx
       7FF9EAE06283 sub       rsp,20
       7FF9EAE06287 mov       rsi,rcx
       7FF9EAE0628A mov       edi,edx
       7FF9EAE0628C mov       ebx,r8d
       7FF9EAE0628F mov       rdx,rsi
       7FF9EAE06292 mov       rcx,offset MT_Microsoft.FSharp.Core.OptimizedClosures+FSharpFunc`3[[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib],[System.Int32, System.Private.CoreLib]]
       7FF9EAE0629C call      qword ptr [7FF9EAE0B828]
       7FF9EAE062A2 test      rax,rax
       7FF9EAE062A5 je        short M01_L00
       7FF9EAE062A7 mov       rcx,rax
       7FF9EAE062AA mov       edx,edi
       7FF9EAE062AC mov       r8d,ebx
       7FF9EAE062AF mov       rax,[rax]
       7FF9EAE062B2 mov       rax,[rax+40]
       7FF9EAE062B6 add       rsp,20
       7FF9EAE062BA pop       rbx
       7FF9EAE062BB pop       rsi
       7FF9EAE062BC pop       rdi
       7FF9EAE062BD jmp       qword ptr [rax+28]
M01_L00:
       7FF9EAE062C1 mov       rcx,rsi
       7FF9EAE062C4 mov       edx,edi
       7FF9EAE062C6 mov       rax,[rsi]
       7FF9EAE062C9 mov       rax,[rax+40]
       7FF9EAE062CD call      qword ptr [rax+20]
       7FF9EAE062D0 mov       rcx,rax
       7FF9EAE062D3 mov       edx,ebx
       7FF9EAE062D5 mov       rax,[rax]
       7FF9EAE062D8 mov       rax,[rax+40]
       7FF9EAE062DC add       rsp,20
       7FF9EAE062E0 pop       rbx
       7FF9EAE062E1 pop       rsi
       7FF9EAE062E2 pop       rdi
       7FF9EAE062E3 jmp       qword ptr [rax+20]
; Total bytes of code 103
```

