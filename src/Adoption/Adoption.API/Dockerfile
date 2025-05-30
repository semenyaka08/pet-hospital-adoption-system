FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Adoption/Adoption.API/Adoption.API.csproj", "Adoption/Adoption.API/"]
COPY ["BuildingBlocks/BuildingBlocks.RabbitMq/BuildingBlocks.RabbitMq.csproj", "BuildingBlocks/BuildingBlocks.RabbitMq/"]
RUN dotnet restore "Adoption/Adoption.API/Adoption.API.csproj"
COPY . .
WORKDIR "/src/Adoption/Adoption.API"
RUN dotnet build "Adoption.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Adoption.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adoption.API.dll"]
