#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AppoitmentRedistribution/AppoitmentRedistribution.csproj", "AppoitmentRedistribution/"]
COPY ["MedAppData/MedAppData.csproj", "MedAppData/"]
COPY ["MedAppCore/MedAppCore.csproj", "MedAppCore/"]
COPY ["MedAppServices/MedAppServices.csproj", "MedAppServices/"]
RUN dotnet restore "AppoitmentRedistribution/AppoitmentRedistribution.csproj"
COPY . .
WORKDIR "/src/AppoitmentRedistribution"
RUN dotnet build "AppoitmentRedistribution.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppoitmentRedistribution.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppoitmentRedistribution.dll"]