FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["app-oss/App.OSS.API/App.OSS.API.csproj", "app-oss/App.OSS.API/"]
COPY ["app-base/App.Base.Domain/App.Base.Domain.csproj", "app-base/App.Base.Domain/"]
COPY ["app-oss/App.OSS.Domain/App.OSS.Domain.csproj", "app-oss/App.OSS.Domain/"]
COPY ["app-base/App.Base.API/App.Base.API.csproj", "app-base/App.Base.API/"]
COPY ["app-oss/App.OSS.Infrastructure/App.OSS.Infrastructure.csproj", "app-oss/App.OSS.Infrastructure/"]
COPY ["app-base/App.Base.Infrastructure/App.Base.Infrastructure.csproj", "app-base/App.Base.Infrastructure/"]
RUN dotnet restore "app-oss/App.OSS.API/App.OSS.API.csproj"
COPY . .
#WORKDIR "/src/app-oss/App.OSS.API"
#RUN dotnet build "App.OSS.API.csproj" -c Release -o /app

FROM build AS publish
WORKDIR /src/app-oss/App.OSS.API
RUN dotnet publish "App.OSS.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "App.OSS.API.dll"]