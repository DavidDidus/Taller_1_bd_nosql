# Usa la imagen oficial de .NET SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia el archivo de proyecto y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia el resto de los archivos de la aplicación y compila
COPY . ./
RUN dotnet publish -c Release -o out

# Usa la imagen oficial de .NET Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Definir variables de entorno
ENV MONGO_URI="${MONGO_URI}"
ENV MONGO_DB_NAME="${MONGO_DB_NAME}"
ENV REDIS_URI="${REDIS_URI}"
ENV REDIS_USER="${REDIS_USER}"
ENV REDIS_PASSWORD="${REDIS_PASSWORD}"

# Exponer el puerto en el que la aplicación escuchará
EXPOSE 5012

# Configura el punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Taller1.dll"]