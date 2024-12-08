
# Taller 1

Proyecto creado en .NET 8.0 que utiliza MongoDB y Redis para la gestión de datos. Este proyecto incluye un Dockerfile para simplificar su ejecución en contenedores.

## Configuración del entorno

El archivo `.env` debe contener los siguientes parámetros:

```
MONGO_URI=<dirección de la base de datos>
MONGO_DB_NAME=<nombre de la base de datos>
REDIS_URI=<dirección del servidor Redis>
REDIS_USER=<usuario de Redis>
REDIS_PASSWORD=<contraseña de Redis>
```

## Ejecución del proyecto con Docker

Este proyecto incluye un `Dockerfile` que facilita su despliegue. Sigue estos pasos:

1. **Construir la imagen de Docker:**
   ```bash
   docker build -t taller_1 .
   ```

2. **Ejecutar el contenedor:**
   ```bash
   docker run -d -p 5012:5012 --env-file .env --name taller_1_container taller_1
   ```

   Esto expondrá el puerto `5012` del contenedor al puerto `5012` de tu máquina host.

### Problemas con Docker
Si no puedes ejecutar el proyecto en Docker, también puedes ejecutarlo directamente con .NET.

## Ejecución sin Docker

1. **Requisitos previos:**
   - Instala .NET 8.0 desde el siguiente enlace: [Descargar .NET 8.0](https://dotnet.microsoft.com/en-us/download)

2. **Ejecuta el proyecto:**
   En la raíz del proyecto, ejecuta:
   ```bash
   dotnet run
   ```

## Dependencias del proyecto

Este proyecto utiliza las siguientes bibliotecas. Asegúrate de instalarlas si ejecutas el proyecto fuera de Docker:

- **DotNetEnv**: Para cargar variables de entorno desde un archivo `.env`.  
  ```bash
  dotnet add package DotNetEnv --version 3.1.1
  ```
- **Microsoft.AspNetCore.OpenApi**: Para trabajar con OpenAPI/Swagger.  
  ```bash
  dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.10
  ```
- **MongoDB.Bson**: Para trabajar con BSON en MongoDB.  
  ```bash
  dotnet add package MongoDB.Bson --version 3.0.0
  ```
- **MongoDB.Driver**: Para interactuar con MongoDB.  
  ```bash
  dotnet add package MongoDB.Driver --version 3.0.0
  ```
- **StackExchange.Redis**: Para interactuar con Redis.  
  ```bash
  dotnet add package StackExchange.Redis
  ```
- **Microsoft.AspNetCore.Mvc**: Para crear controladores y manejar solicitudes HTTP.  
  ```bash
  dotnet add package Microsoft.AspNetCore.Mvc
  ```
- **Microsoft.Extensions.DependencyInjection**: Para la inyección de dependencias.  
  ```bash
  dotnet add package Microsoft.Extensions.DependencyInjection
  ```
- **Microsoft.Extensions.Configuration**: Para manejar la configuración de la aplicación.  
  ```bash
  dotnet add package Microsoft.Extensions.Configuration
  ```
- **System.Text.Json**: Para la serialización y deserialización de JSON.  
  ```bash
  dotnet add package System.Text.Json
  ```


