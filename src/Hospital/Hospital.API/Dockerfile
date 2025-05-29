FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Hospital/Hospital.API/Hospital.API.csproj", "Hospital/Hospital.API/"]
COPY ["BuildingBlocks/BuildingBlocks.RabbitMq/BuildingBlocks.RabbitMq.csproj", "BuildingBlocks/BuildingBlocks.RabbitMq/"]
RUN dotnet restore "Hospital/Hospital.API/Hospital.API.csproj"
COPY . .
WORKDIR "/src/Hospital/Hospital.API"
RUN dotnet build "Hospital.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Hospital.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hospital.API.dll"]
