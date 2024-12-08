
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

