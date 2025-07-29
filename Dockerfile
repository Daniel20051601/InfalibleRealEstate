FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["InfalibleRealEstate.csproj", "./"]
RUN dotnet restore "InfalibleRealEstate.csproj"

COPY . .
RUN dotnet publish "InfalibleRealEstate.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "InfalibleRealEstate.dll"]