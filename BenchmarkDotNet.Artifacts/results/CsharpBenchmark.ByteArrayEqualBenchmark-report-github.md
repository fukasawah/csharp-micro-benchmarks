``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19042.1288 (20H2/October2020Update)
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.414
  [Host]     : .NET Core 3.1.20 (CoreCLR 4.700.21.47003, CoreFX 4.700.21.47101), X64 RyuJIT
  DefaultJob : .NET Core 3.1.20 (CoreCLR 4.700.21.47003, CoreFX 4.700.21.47101), X64 RyuJIT


```
|                           Method |      TestData |              Mean |            Error |           StdDev |
|--------------------------------- |-------------- |------------------:|-----------------:|-----------------:|
| **StructuralEqualityComparerEquals** |    **Byte[1024]** |      **99,732.48 ns** |     **1,481.524 ns** |     **1,385.818 ns** |
|                    SequenceEqual |    Byte[1024] |       6,091.01 ns |        92.471 ns |        86.497 ns |
|                    SimpleCompare |    Byte[1024] |         586.48 ns |         9.569 ns |         8.951 ns |
|                SpanSequenceEqual |    Byte[1024] |          96.05 ns |         0.835 ns |         0.781 ns |
|                 MultiTaskCompare |    Byte[1024] |       6,438.75 ns |        64.357 ns |        60.199 ns |
|                   ArrayCompare32 |    Byte[1024] |         185.96 ns |         2.171 ns |         1.813 ns |
|                   ArrayCompare64 |    Byte[1024] |         135.90 ns |         1.798 ns |         1.765 ns |
|                           MemCmp |    Byte[1024] |         119.52 ns |         1.311 ns |         1.162 ns |
| **StructuralEqualityComparerEquals** | **Byte[1048575]** | **112,932,348.57 ns** |   **954,909.489 ns** |   **846,502.389 ns** |
|                    SequenceEqual | Byte[1048575] |   6,366,387.78 ns |   117,536.055 ns |   104,192.652 ns |
|                    SimpleCompare | Byte[1048575] |     677,000.18 ns |     8,469.303 ns |     7,922.191 ns |
|                SpanSequenceEqual | Byte[1048575] |     175,290.30 ns |     3,449.946 ns |     6,809.857 ns |
|                 MultiTaskCompare | Byte[1048575] |     204,758.80 ns |     3,269.159 ns |     4,992.351 ns |
|                   ArrayCompare32 | Byte[1048575] |     256,591.36 ns |     4,400.901 ns |     4,519.401 ns |
|                   ArrayCompare64 | Byte[1048575] |     201,876.98 ns |     3,223.374 ns |     3,015.146 ns |
|                           MemCmp | Byte[1048575] |     191,929.50 ns |     3,808.798 ns |     5,213.526 ns |
| **StructuralEqualityComparerEquals** | **Byte[1048576]** | **114,841,664.62 ns** | **1,522,188.919 ns** | **1,271,096.688 ns** |
|                    SequenceEqual | Byte[1048576] |   6,623,190.35 ns |    47,652.643 ns |    42,242.827 ns |
|                    SimpleCompare | Byte[1048576] |     700,016.22 ns |     5,987.405 ns |     4,999.754 ns |
|                SpanSequenceEqual | Byte[1048576] |     178,076.45 ns |     3,475.050 ns |     3,862.509 ns |
|                 MultiTaskCompare | Byte[1048576] |     206,830.56 ns |     4,086.425 ns |     4,864.598 ns |
|                   ArrayCompare32 | Byte[1048576] |     258,189.56 ns |     4,724.524 ns |     5,802.140 ns |
|                   ArrayCompare64 | Byte[1048576] |     202,681.13 ns |     4,040.556 ns |     4,491.067 ns |
|                           MemCmp | Byte[1048576] |     195,249.87 ns |     3,060.142 ns |     2,862.459 ns |
| **StructuralEqualityComparerEquals** |      **Byte[16]** |       **1,858.33 ns** |        **34.321 ns** |        **61.887 ns** |
|                    SequenceEqual |      Byte[16] |         180.63 ns |         1.020 ns |         0.852 ns |
|                    SimpleCompare |      Byte[16] |          32.19 ns |         0.390 ns |         0.365 ns |
|                SpanSequenceEqual |      Byte[16] |          25.51 ns |         0.159 ns |         0.141 ns |
|                 MultiTaskCompare |      Byte[16] |       7,147.20 ns |       111.951 ns |       104.719 ns |
|                   ArrayCompare32 |      Byte[16] |          26.22 ns |         0.174 ns |         0.163 ns |
|                   ArrayCompare64 |      Byte[16] |          25.24 ns |         0.185 ns |         0.173 ns |
|                           MemCmp |      Byte[16] |          30.49 ns |         0.239 ns |         0.224 ns |
