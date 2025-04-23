FROM mcr.microsoft.com/dotnet/sdk:9.0

COPY . benchmark
RUN dotnet build benchmark/AnyText.PerformanceBenchmark.sln --configuration Release

WORKDIR /benchmark

CMD [ "dotnet", "bin/Release/net8.0/AnyText.PerformanceBenchmark.dll" ]