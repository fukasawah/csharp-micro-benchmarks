``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19042.1288 (20H2/October2020Update)
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.414
  [Host]     : .NET Core 3.1.20 (CoreCLR 4.700.21.47003, CoreFX 4.700.21.47101), X64 RyuJIT
  DefaultJob : .NET Core 3.1.20 (CoreCLR 4.700.21.47003, CoreFX 4.700.21.47101), X64 RyuJIT


```
|                           Method |        TestData |                 Mean |             Error |            StdDev |               Median |
|--------------------------------- |---------------- |---------------------:|------------------:|------------------:|---------------------:|
| **StructuralEqualityComparerEquals** |      **Byte[1024]** |        **103,824.48 ns** |      **2,036.869 ns** |      **2,000.478 ns** |        **103,089.92 ns** |
|                    SequenceEqual |      Byte[1024] |          6,256.96 ns |         62.058 ns |         55.013 ns |          6,266.41 ns |
|                    SimpleCompare |      Byte[1024] |            600.54 ns |          9.548 ns |          8.931 ns |            603.72 ns |
|                SpanSequenceEqual |      Byte[1024] |             97.52 ns |          1.552 ns |          2.225 ns |             97.21 ns |
|                 MultiTaskCompare |      Byte[1024] |         13,443.64 ns |        171.417 ns |        160.344 ns |         13,489.51 ns |
|                   ArrayCompare32 |      Byte[1024] |            200.76 ns |          4.041 ns |          9.605 ns |            196.69 ns |
|                   ArrayCompare64 |      Byte[1024] |            138.57 ns |          1.160 ns |          0.969 ns |            138.60 ns |
|                           MemCmp |      Byte[1024] |            119.48 ns |          2.384 ns |          2.839 ns |            119.46 ns |
| **StructuralEqualityComparerEquals** |   **Byte[1048575]** |    **108,380,618.67 ns** |  **1,150,894.783 ns** |  **1,076,547.694 ns** |    **108,394,760.00 ns** |
|                    SequenceEqual |   Byte[1048575] |      6,363,528.35 ns |     77,338.832 ns |     68,558.860 ns |      6,364,174.61 ns |
|                    SimpleCompare |   Byte[1048575] |        679,806.70 ns |      8,475.727 ns |      7,928.201 ns |        681,782.91 ns |
|                SpanSequenceEqual |   Byte[1048575] |        174,003.90 ns |      3,457.742 ns |      6,744.060 ns |        173,218.51 ns |
|                 MultiTaskCompare |   Byte[1048575] |        204,064.99 ns |      3,100.256 ns |      2,588.854 ns |        203,645.78 ns |
|                   ArrayCompare32 |   Byte[1048575] |        247,386.57 ns |      4,673.774 ns |      5,194.888 ns |        248,315.62 ns |
|                   ArrayCompare64 |   Byte[1048575] |        197,532.89 ns |      3,934.935 ns |      5,643.366 ns |        197,665.50 ns |
|                           MemCmp |   Byte[1048575] |        200,833.15 ns |     10,170.339 ns |     28,181.962 ns |        190,308.74 ns |
| **StructuralEqualityComparerEquals** |   **Byte[1048576]** |    **108,498,673.33 ns** |    **979,775.388 ns** |    **916,482.506 ns** |    **108,489,980.00 ns** |
|                    SequenceEqual |   Byte[1048576] |      6,407,282.99 ns |     52,752.418 ns |     44,050.658 ns |      6,388,628.12 ns |
|                    SimpleCompare |   Byte[1048576] |        687,527.76 ns |      8,050.289 ns |      7,530.245 ns |        687,771.68 ns |
|                SpanSequenceEqual |   Byte[1048576] |        176,101.52 ns |      3,480.392 ns |      5,209.286 ns |        176,307.90 ns |
|                 MultiTaskCompare |   Byte[1048576] |        206,742.06 ns |      2,938.385 ns |      2,748.567 ns |        206,631.57 ns |
|                   ArrayCompare32 |   Byte[1048576] |        250,372.59 ns |      4,787.340 ns |      4,478.081 ns |        251,726.32 ns |
|                   ArrayCompare64 |   Byte[1048576] |        195,164.65 ns |      3,759.878 ns |      5,511.180 ns |        195,841.38 ns |
|                           MemCmp |   Byte[1048576] |        193,794.15 ns |      3,815.852 ns |      6,269.554 ns |        192,987.73 ns |
| **StructuralEqualityComparerEquals** | **Byte[134217728]** | **14,625,289,784.62 ns** | **75,694,667.582 ns** | **63,208,475.660 ns** | **14,606,151,200.00 ns** |
|                    SequenceEqual | Byte[134217728] |    835,892,753.33 ns |  7,784,221.304 ns |  7,281,365.437 ns |    833,311,000.00 ns |
|                    SimpleCompare | Byte[134217728] |    120,854,436.00 ns |  1,921,895.955 ns |  1,797,742.669 ns |    120,424,780.00 ns |
|                SpanSequenceEqual | Byte[134217728] |     63,132,158.33 ns |    698,865.243 ns |    653,718.982 ns |     63,080,225.00 ns |
|                 MultiTaskCompare | Byte[134217728] |     64,996,656.25 ns |  1,237,082.460 ns |  1,214,980.388 ns |     64,942,061.11 ns |
|                   ArrayCompare32 | Byte[134217728] |     67,619,060.94 ns |  1,308,127.802 ns |  1,284,756.413 ns |     67,364,518.75 ns |
|                   ArrayCompare64 | Byte[134217728] |     64,572,005.77 ns |    815,411.465 ns |    680,905.504 ns |     64,569,587.50 ns |
|                           MemCmp | Byte[134217728] |     64,282,324.76 ns |  1,063,560.254 ns |    994,854.922 ns |     64,372,885.71 ns |
| **StructuralEqualityComparerEquals** |        **Byte[16]** |          **1,766.25 ns** |         **19.334 ns** |         **18.085 ns** |          **1,761.45 ns** |
|                    SequenceEqual |        Byte[16] |            174.44 ns |          2.369 ns |          4.086 ns |            173.02 ns |
|                    SimpleCompare |        Byte[16] |             31.24 ns |          0.654 ns |          0.671 ns |             31.18 ns |
|                SpanSequenceEqual |        Byte[16] |             24.86 ns |          0.277 ns |          0.259 ns |             24.85 ns |
|                 MultiTaskCompare |        Byte[16] |         13,493.23 ns |        197.955 ns |        175.482 ns |         13,530.73 ns |
|                   ArrayCompare32 |        Byte[16] |             25.43 ns |          0.184 ns |          0.172 ns |             25.46 ns |
|                   ArrayCompare64 |        Byte[16] |             24.75 ns |          0.285 ns |          0.266 ns |             24.79 ns |
|                           MemCmp |        Byte[16] |             30.99 ns |          0.469 ns |          0.392 ns |             30.92 ns |
