# PartnecolRamales

## 1. Descripción del proyecto

PartnecolRamales es una aplicación de consola desarrollada en C# y .NET 6 como apoyo a la consulta de información relacionada con la fabricación de ramales eléctricos para motocicletas en PARTNECOL S.A.S.

El sistema permite consultar referencias de ramales, equivalencias de colores de cables y la función asociada a la marcación numérica de conectores.

## 2. Funcionalidades implementadas

- **HU01:** Consulta de referencias de ramales.
- **HU02:** Consulta de equivalencias de colores de cables.
- **HU03:** Consulta de marcación de conectores.

## 3. Tecnologías utilizadas

- C#
- .NET 6
- SQL Server Express
- NUnit
- Moq
- Git

## 4. Estructura del proyecto

- `Partnecol.App`: aplicación de consola e interacción con el usuario.
- `Partnecol.Core`: lógica del sistema, modelos, interfaces y acceso a datos.
- `Partnecol.Tests`: pruebas automatizadas.

## 5. Requisitos previos

- SDK de .NET 6.
- SQL Server Express.
- Una instancia de SQL Server configurada y accesible.
- Base de datos `PartnecolRamales` creada y preparada con las tablas y datos necesarios.

## 6. Configuración y ejecución

1. Clonar o descargar el repositorio.
2. Configurar el acceso a SQL Server según la instancia disponible.
3. Crear la base de datos y sus tablas, e ingresar los datos requeridos.
4. Abrir `PartnecolRamales.sln` en Visual Studio.
5. Configurar `Partnecol.App` como proyecto de inicio.
6. Ejecutar la aplicación.

La cadena de conexión debe adaptarse al entorno de cada desarrollador. No se deben publicar contraseñas ni otros datos sensibles en el repositorio.

## 7. Pruebas automatizadas

Desde la carpeta raíz de la solución, ejecutar:

```powershell
dotnet test PartnecolRamales.sln
```

Este comando ejecuta las pruebas automatizadas de la solución. Las pruebas de integración con SQL Server requieren que la base de datos y la conexión estén configuradas correctamente.

## 8. Estado del proyecto

El proyecto cuenta con las funcionalidades de consulta implementadas y pruebas automatizadas. Su alcance corresponde a un avance académico; la información de marcación debe validarse con el catálogo real de producción antes de utilizarse como referencia operativa.