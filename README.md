```

BenchmarkDotNet v0.13.12, Arch Linux WSL
AMD Ryzen 7 7840H w/ Radeon 780M Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.101
  [Host]   : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  ShortRun : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=ShortRun  Runtime=.NET 8.0  IterationCount=3  
LaunchCount=1  WarmupCount=3  

```
| Type        | Method                    | N    | Mean            | Error          | StdDev        |
|------------ |-------------------------- |----- |----------------:|---------------:|--------------:|
| **MatMulBench** | **MatMulEager**               | **10**   |       **208.77 ns** |      **73.801 ns** |      **4.045 ns** |
| MatMulBench | MatVecET                  | 10   |       185.62 ns |      47.826 ns |      2.622 ns |
| MatMulBench | MatVecETInplace           | 10   |       103.34 ns |      12.473 ns |      0.684 ns |
| MatMulBench | MatVecETAfterEager        | 10   |       143.67 ns |      84.895 ns |      4.653 ns |
| MatMulBench | MatVecETInplaceAfterEager | 10   |        94.81 ns |      32.370 ns |      1.774 ns |
| MatMulBench | MatMulETInterface         | 10   |       655.85 ns |     417.903 ns |     22.907 ns |
| MatMulBench | MatMulETInterfaceInplace  | 10   |       539.01 ns |     184.195 ns |     10.096 ns |
| VecBench    | VecAddEager               | 10   |        35.10 ns |      13.324 ns |      0.730 ns |
| VecBench    | VecAddET                  | 10   |        35.44 ns |      17.447 ns |      0.956 ns |
| VecBench    | VecAddETInplace           | 10   |        12.56 ns |       4.566 ns |      0.250 ns |
| VecBench    | VecAddETInterface         | 10   |        78.50 ns |      51.625 ns |      2.830 ns |
| VecBench    | VecAddETInterfaceInplace  | 10   |        61.85 ns |      32.318 ns |      1.771 ns |
| **MatMulBench** | **MatMulEager**               | **1000** | **2,645,064.92 ns** | **420,641.593 ns** | **23,056.785 ns** |
| MatMulBench | MatVecET                  | 1000 | 1,599,019.75 ns | 147,490.294 ns |  8,084.441 ns |
| MatMulBench | MatVecETInplace           | 1000 | 1,208,840.62 ns | 295,830.655 ns | 16,215.476 ns |
| MatMulBench | MatVecETAfterEager        | 1000 |   727,587.04 ns | 174,778.298 ns |  9,580.188 ns |
| MatMulBench | MatVecETInplaceAfterEager | 1000 |   665,255.99 ns | 173,158.107 ns |  9,491.380 ns |
| MatMulBench | MatMulETInterface         | 1000 | 6,100,101.38 ns | 445,850.916 ns | 24,438.593 ns |
| MatMulBench | MatMulETInterfaceInplace  | 1000 | 4,683,724.48 ns | 661,078.865 ns | 36,235.963 ns |
| VecBench    | VecAddEager               | 1000 |     2,420.12 ns |   1,155.901 ns |     63.359 ns |
| VecBench    | VecAddET                  | 1000 |     2,443.81 ns |   2,337.026 ns |    128.100 ns |
| VecBench    | VecAddETInplace           | 1000 |       939.88 ns |     327.963 ns |     17.977 ns |
| VecBench    | VecAddETInterface         | 1000 |     6,152.53 ns |   5,787.656 ns |    317.241 ns |
| VecBench    | VecAddETInterfaceInplace  | 1000 |     5,427.66 ns |   1,245.613 ns |     68.276 ns |
