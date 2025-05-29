FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PetShelter/PetShelter.API/PetShelter.API.csproj", "PetShelter/PetShelter.API/"]
COPY ["PetShelter/PetShelter.Domain/PetShelter.Domain.csproj", "PetShelter/PetShelter.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.Common/BuildingBlocks.Common.csproj", "BuildingBlocks/BuildingBlocks.Common/"]
COPY ["PetShelter/PetShelter.Infrastructure/PetShelter.Infrastructure.csproj", "PetShelter/PetShelter.Infrastructure/"]
COPY ["BuildingBlocks/BuildingBlocks.RabbitMq/BuildingBlocks.RabbitMq.csproj", "BuildingBlocks/BuildingBlocks.RabbitMq/"]
COPY ["PetShelter/PetShelter.Application/PetShelter.Application.csproj", "PetShelter/PetShelter.Application/"]
RUN dotnet restore "PetShelter/PetShelter.API/PetShelter.API.csproj"
COPY . .
WORKDIR "/src/PetShelter/PetShelter.API"
RUN dotnet build "PetShelter.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetShelter.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetShelter.API.dll"]
