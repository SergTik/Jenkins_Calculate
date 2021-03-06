# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source
# copy csproj and restore as distinct layers
COPY *.sln .
COPY TestCalc/TestCalc.csproj ./TestCalc/
COPY TestCalc.Tests/TestCalc.Tests.csproj ./TestCalc.Tests/
RUN dotnet restore -r linux-x64

# copy everything else and build app
COPY TestCalc/. ./TestCalc/
WORKDIR /source/TestCalc
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false
# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["./TestCalc"]
