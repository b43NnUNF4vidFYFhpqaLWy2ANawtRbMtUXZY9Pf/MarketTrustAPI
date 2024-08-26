FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /App
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MarketTrustAPI.csproj", "."]
RUN dotnet restore "./MarketTrustAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MarketTrustAPI.csproj" -c Release -o /App/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MarketTrustAPI.csproj" -c Release -o /App/publish /p:useAppHost=false

FROM base AS final
WORKDIR /App
COPY --from=publish /App/publish .
ENTRYPOINT ["dotnet", "MarketTrustAPI.dll"]