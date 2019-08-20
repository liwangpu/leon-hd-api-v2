FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["app-basic/App.Basic.API/App.Basic.API.csproj", "app-basic/App.Basic.API/"]
COPY ["app-basic/App.Basic.Domain/App.Basic.Domain.csproj", "app-basic/App.Basic.Domain/"]
COPY ["app-basic/App.Basic.Infrastructure/App.Basic.Infrastructure.csproj", "app-basic/App.Basic.Infrastructure/"]
RUN dotnet restore "app-basic/App.Basic.API/App.Basic.API.csproj"
COPY . .
#WORKDIR "/src/app-basic/App.Basic.API"
#RUN dotnet build "App.Basic.API.csproj" -c Release -o /app

FROM build AS publish
WORKDIR /src/app-basic/App.Basic.API
RUN dotnet publish "App.Basic.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "App.Basic.API.dll"]