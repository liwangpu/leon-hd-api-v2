FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["app-oms/App.OMS.API/App.OMS.API.csproj", "app-oms/App.OMS.API/"]
RUN dotnet restore "app-oms/App.OMS.API/App.OMS.API.csproj"
COPY . .
#WORKDIR "/src/app-oms/App.OMS.API"
#RUN dotnet build "App.OMS.API.csproj" -c Release -o /app

FROM build AS publish
WORKDIR /src/app-oms/App.OMS.API
RUN dotnet publish "App.OMS.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "App.OMS.API.dll"]