# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.
# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
#Paso 1 
#Descarga de runtime de net core (compiladores)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio
# codigo auto generado para la descarga del SDK (librerias )
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Paso 2 
#ubicacion del codigo fuente del servicio 
COPY ["LaboratorioBack.csproj", "."]
RUN dotnet restore "./LaboratorioBack.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./LaboratorioBack.csproj" -c $BUILD_CONFIGURATION -o /app/build

#Paso 3
# Proceso de compilacion y publicacion al interior del container 
# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LaboratorioBack.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LaboratorioBack.dll"]