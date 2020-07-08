#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./
COPY ["TimeRedistribution/TimeRedistribution.csproj", "TimeRedistribution/"]
COPY ["MedAppData/MedAppData.csproj", "MedAppData/"]
COPY ["MedAppCore/MedAppCore.csproj", "MedAppCore/"]
COPY ["MedAppServices/MedAppServices.csproj", "MedAppServices/"]
RUN dotnet restore
COPY . .
WORKDIR "/src/TimeRedistribution"
RUN dotnet build -c $Configuration -o /app

FROM build AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TimeRedistribution.dll"]