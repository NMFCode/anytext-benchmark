
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3194)
AMD Ryzen 7 PRO 6850H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.200
  [Host]     : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2


 Method         | Model  | Mean            | Error          | StdDev         | Median          |
--------------- |------- |----------------:|---------------:|---------------:|----------------:|
 **SemanticTokens** | **61850**  | **1,730,143.44 ns** |  **71,302.798 ns** | **204,581.107 ns** | **1,655,342.87 ns** |
 FoldingRanges  | 61850  |   371,904.72 ns |  13,437.238 ns |  39,620.002 ns |   372,980.08 ns |
 Formatting     | 61850  |        12.86 ns |       0.268 ns |       0.287 ns |        12.76 ns |
 **SemanticTokens** | **COSEM**  | **1,018,592.29 ns** |  **20,025.112 ns** |  **25,325.357 ns** | **1,018,508.50 ns** |
 FoldingRanges  | COSEM  |   296,925.31 ns |   8,183.839 ns |  24,130.235 ns |   296,481.32 ns |
 Formatting     | COSEM  |        12.87 ns |       0.286 ns |       0.544 ns |        12.65 ns |
 **SemanticTokens** | **KDM**    | **1,027,257.52 ns** |  **17,246.589 ns** |  **15,288.652 ns** | **1,020,453.22 ns** |
 FoldingRanges  | KDM    |   144,276.46 ns |   2,872.327 ns |   5,394.926 ns |   141,001.33 ns |
 Formatting     | KDM    |        12.65 ns |       0.130 ns |       0.109 ns |        12.67 ns |
 **SemanticTokens** | **NMeta**  |    **58,833.60 ns** |     **313.043 ns** |     **244.403 ns** |    **58,839.14 ns** |
 FoldingRanges  | NMeta  |    23,477.04 ns |     276.160 ns |     230.606 ns |    23,402.69 ns |
 Formatting     | NMeta  |        12.77 ns |       0.206 ns |       0.172 ns |        12.76 ns |
 **SemanticTokens** | **schema** | **9,185,037.20 ns** | **183,252.480 ns** | **378,448.178 ns** | **9,042,355.47 ns** |
 FoldingRanges  | schema | 4,655,268.83 ns | 101,283.823 ns | 297,047.941 ns | 4,658,119.92 ns |
 Formatting     | schema |        15.38 ns |       0.215 ns |       0.202 ns |        15.29 ns |
