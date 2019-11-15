FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["SuiviCompresseur.ApiGateways/SuiviCompresseur.ApiGateways.csproj", "SuiviCompresseur.ApiGateways/"]
RUN dotnet restore "SuiviCompresseur.ApiGateways/SuiviCompresseur.ApiGateways.csproj"
COPY . .
WORKDIR "/src/SuiviCompresseur.ApiGateways"
RUN dotnet build "SuiviCompresseur.ApiGateways.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SuiviCompresseur.ApiGateways.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SuiviCompresseur.ApiGateways.dll"]
