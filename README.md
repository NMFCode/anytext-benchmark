# Anytext Benchmark

This is a benchmark for NMF AnyText, used for the paper *AnyText: Incremental, left-recursive Parsing and Pretty-Printing from a single Grammar Definition with first-class LSP support* by Georg Hinkel, Alexander Hert, Niklas Hettler and Kevin Weinert, 
accepted for publication at the SLE 2025 conference.

## Running the Benchmark as a Docker Container

The simplest way to run the benchmark is to execute it as a Docker container. You can start the container as follows:

```bash
docker run georghinkel/anytext.performance_benchmark
```

The benchmark will print its results as ASCII-art in the console. Alternatively, you can find the results under `/benchmark/BenchmarkDotNet.Artifacts/results`. Note that it will contain the R scripts necessary to produce the diagrams, but not the resulting diagrams because the Docker container does not have R installed.

## Install prerequisites

To run the benchmark, you need to have the .NET SDK installed. To install it, follow the [instructions](https://github.com/dotnet/core/blob/main/release-notes/9.0/install.md) provided by Microsoft. 

To render the plots visible in the paper, you need to have R installed.

## Compile and Run the Benchmark

The benchmark can be compiled using the dotnet CLI:

```bash
dotnet build --configuration Release AnyText.PerformanceBenchmark.sln
```

Afterwards, you can execute the benchmark by starting

```bash
dotnet bin/Release/net8.0/AnyText.PerformanceBenchmark.dll
```

The benchmark takes about 60 to 90 minutes to complete and produces a number of results, including plain values, HTML summaries, CSV files with the raw measurement results and (if R is installed), plots.
We included the results which we used for the paper in the repository, so you can also review this data. We manually changed the plot scripts but did not change the data.

## Inspect Benchmark Results

After the Benchmark completes, results can be found in `BenchmarkDotNet.Artifacts/results`.

## Inspect Benchmark Sources

The solution consists of two benchmarks, one that measures the update performance against the reinitialization. The other benchmark measures the performance of selected editor services. 

### Reinitialization Benchmark

The benchmark to measure re-initialization against update is found in `AnyMetaInitializeUpdateBenchmark.cs`. This is the benchmark that is referred to in the paper.

For each of the AnyMeta files that we use for benchmarking, we created an additional file with randomly selected tokens, in order to improve the reproducibility of the benchmark results. These are CSV files that consist of the token that is removed and its position (line and column number).
The actual benchmark is implemented in three public methods, executed by the benchmark framework. In order to reduce the noise in the benchmark, we omitted the LSP protocol implementation and call the parser methods directly.

### Editor-features Benchmark

The repository also includes a second benchmark to measure the performance of semantic tokens, folding ranges and formatting. The benchmark sources can be found in `EditorFeaturesBenchmark.cs`.