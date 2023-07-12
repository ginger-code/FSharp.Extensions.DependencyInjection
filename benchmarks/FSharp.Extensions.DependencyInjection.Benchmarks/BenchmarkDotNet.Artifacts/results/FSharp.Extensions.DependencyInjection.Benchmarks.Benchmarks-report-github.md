``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22621
13th Gen Intel Core i9-13900KF, 1 CPU, 32 logical and 24 physical cores
.NET Core SDK=7.0.306
  [Host]     : .NET Core 7.0.9 (CoreCLR 7.0.923.32018, CoreFX 7.0.923.32018), X64 RyuJIT DEBUG
  Throughput : .NET Core 7.0.9 (CoreCLR 7.0.923.32018, CoreFX 7.0.923.32018), X64 RyuJIT

Job=Throughput  RunStrategy=Throughput  

```
|                                         Method |      Mean |     Error |    StdDev |    Median | Gen 0 | Gen 1 | Gen 2 | Allocated | Code Size |
|----------------------------------------------- |----------:|----------:|----------:|----------:|------:|------:|------:|----------:|----------:|
|                            &#39;Raw Function Call&#39; | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     - |     - |     - |         - |       6 B |
|                              &#39;Raw Lambda Call&#39; | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     - |     - |     - |         - |       6 B |
|                             &#39;Raw Curried Call&#39; | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |     - |     - |     - |         - |       6 B |
|        &#39;Wrapped Function Call (Direct access)&#39; | 0.0029 ns | 0.0000 ns | 0.0000 ns | 0.0029 ns |     - |     - |     - |         - |     155 B |
|          &#39;Wrapped Lambda Call (Direct access)&#39; | 0.0029 ns | 0.0000 ns | 0.0000 ns | 0.0029 ns |     - |     - |     - |         - |     272 B |
|         &#39;Wrapped Curried Call (Direct access)&#39; | 0.0012 ns | 0.0000 ns | 0.0000 ns | 0.0012 ns |     - |     - |     - |         - |      51 B |
| &#39;Injected Wrapped Function Call (Pre-Fetched)&#39; | 0.0023 ns | 0.0000 ns | 0.0000 ns | 0.0023 ns |     - |     - |     - |         - |     124 B |
