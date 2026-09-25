# Backend - Sistema de Incidencias

Backend desarrollado en **C# con ASP.NET Core** para gestionar incidencias, usuarios, categorías y comentarios mediante una API REST.

## Tecnologías utilizadas

- C#
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Docker
- Swagger / OpenAPI
- JWT (JSON Web Token)
- API REST

## Funcionalidades

### Incidencias
- Crear incidencias
- Consultar incidencias
- Modificar incidencias
- Eliminar incidencias

### Usuarios
- Crear usuarios
- Consultar usuarios
- Eliminar usuarios
- Autenticación mediante JWT
- Autorización mediante roles

### Categorías
- Crear categorías
- Consultar categorías
- Modificar categorías
- Eliminar categorías

### Comentarios
- Crear comentarios
- Consultar comentarios
- Modificar comentarios
- Eliminar comentarios

## Casos de uso

El sistema permite gestionar los distintos recursos de la aplicación mediante operaciones CRUD.

### Gestión de incidencias
El usuario puede:
- Registrar una incidencia.
- Consultar las incidencias existentes.
- Modificar una incidencia.
- Eliminar una incidencia.

### Gestión de usuarios
El sistema permite:
- Registrar usuarios.
- Consultar usuarios.
- Eliminar usuarios.
- Autenticar usuarios mediante JWT.
- Controlar el acceso según los roles asignados.

### Gestión de categorías
El usuario puede:
- Crear categorías.
- Consultar categorías.
- Modificar categorías.
- Eliminar categorías.

### Gestión de comentarios
El usuario puede:
- Crear comentarios.
- Consultar comentarios.
- Modificar comentarios.
- Eliminar comentarios.

## API

Se desarrolló una **API REST utilizando ASP.NET Core**.

Los endpoints pueden ser consultados y probados mediante **Swagger / OpenAPI**.

La API cuenta con autenticación mediante **JWT** y autorización basada en roles.

## Base de datos

El proyecto utiliza **PostgreSQL** como sistema de gestión de base de datos.

La comunicación entre la aplicación y la base de datos se realiza mediante **Entity Framework Core**.

El proyecto utiliza **migraciones** para gestionar la estructura de la base de datos.

## Docker

El proyecto está preparado para ejecutarse mediante **Docker**.

Se utilizan:

- `Dockerfile` para construir la imagen del backend.
- `docker-compose.yml` para levantar el backend y los servicios necesarios.

## Ejecución

Para ejecutar el proyecto:

1. Tener Docker instalado y funcionando.
2. Clonar el repositorio.
3. Abrir una terminal en la carpeta del proyecto.
4. Levantar los contenedores mediante Docker Compose.
5. Verificar que el backend y la base de datos estén funcionando.
6. Acceder a Swagger para consultar y probar los endpoints.

## Estructura del proyecto


Backend/
├── Controllers/
├── Data/
├── Migrations/
├── Modelos/
├── Properties/
├── Dockerfile
├── docker-compose.yml
├── Program.cs
├── appsettings.json
└── Backend.csproj





