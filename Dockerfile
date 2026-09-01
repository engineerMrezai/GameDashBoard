# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["GameStore.Api.csproj", "."]
RUN dotnet restore "GameStore.Api.csproj"

COPY . .
RUN dotnet publish "GameStore.Api.csproj" \
    --configuration Release \
    --output /app/publish \
    --no-restore \
    /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080
ENV ConnectionStrings__GameStore="Data Source=/app/data/GameStore.db"
EXPOSE 8080

COPY --from=build /app/publish .

# The SQLite database is mounted here by docker compose.
RUN mkdir -p /app/data && chown -R $APP_UID:$APP_UID /app
USER $APP_UID

ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
