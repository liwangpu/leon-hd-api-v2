FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["app-morejee/App.MoreJee.API/App.MoreJee.API.csproj", "app-morejee/App.MoreJee.API/"]
COPY ["app-base/App.Base.Domain/App.Base.Domain.csproj", "app-base/App.Base.Domain/"]
COPY ["app-base/App.Base.Infrastructure/App.Base.Infrastructure.csproj", "app-base/App.Base.Infrastructure/"]
COPY ["app-base/App.Base.API/App.Base.API.csproj", "app-base/App.Base.API/"]
COPY ["app-morejee/App.MoreJee.Domain/App.MoreJee.Domain.csproj", "app-morejee/App.MoreJee.Domain/"]
COPY ["app-morejee/App.MoreJee.Infrastructure/App.MoreJee.Infrastructure.csproj", "app-morejee/App.MoreJee.Infrastructure/"]
RUN dotnet restore "app-morejee/App.MoreJee.API/App.MoreJee.API.csproj"
COPY . .
# WORKDIR "/src/app-morejee/App.MoreJee.API"
# RUN dotnet build "App.MoreJee.API.csproj" -c Release -o /app

FROM build AS publish
WORKDIR /src/app-morejee/App.MoreJee.API
RUN dotnet publish "App.MoreJee.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "App.MoreJee.API.dll"]