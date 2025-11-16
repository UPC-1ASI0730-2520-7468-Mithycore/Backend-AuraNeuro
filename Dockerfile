# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar solución
COPY Backend-AuraNeuro.sln ./

# Copiar proyecto API (esta es tu única carpeta)
COPY Backend-AuraNeuro.API/*.csproj Backend-AuraNeuro.API/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código
COPY . .

# Compilar
WORKDIR /src/Backend-AuraNeuro.API
RUN dotnet publish -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Backend-AuraNeuro.API.dll"]
