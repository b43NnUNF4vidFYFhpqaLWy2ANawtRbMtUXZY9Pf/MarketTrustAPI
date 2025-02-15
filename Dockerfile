FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /App
EXPOSE 8080
EXPOSE 8081

RUN adduser -u 5678 --disabled-password --gecos "" appuser && \
    mkdir -p /home/appuser/.aspnet/DataProtection-Keys && \
    chown -R appuser:appuser /home/appuser/.aspnet
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App
COPY ["src/MarketTrustAPI.csproj", "src/"]
RUN dotnet restore "src/MarketTrustAPI.csproj"
COPY . .
WORKDIR "/App/src"
RUN dotnet build "MarketTrustAPI.csproj" -c Release -o /App/build

FROM build AS publish
RUN dotnet publish "MarketTrustAPI.csproj" -c Release -o /App/publish /p:useAppHost=false

FROM base AS final
WORKDIR /App
COPY --from=publish /App/publish .
ENTRYPOINT ["dotnet", "MarketTrustAPI.dll"]