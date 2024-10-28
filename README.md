# Creado en .net

.env deberia tener 2 parametros
MONGO_URI= direccion de la base de datos
MONGO_DB_NAME = nombre de la base de datos

El proyecto tiene un dockerfile por lo que para ejecutarlo hay que utilizar estos comandos

buildear imagen
docker build -t taller_1 .

Ejecutar el contenedor y exponer el puerto 5012 del contenedor al puerto 5012 de tu máquina host:
docker run -d -p 5012:5012 --env-file .env --name taller_1_container taller_1

Si por alguna razon esto no funciona tambien puede ser ejecutado teniendo
.net 8.0
que puede ser descargado en el siguiente enlace
<https://dotnet.microsoft.com/en-us/download>

Una vez instalado ejecutar dentro de la carpeta raiz del proyecto
dotnet run
