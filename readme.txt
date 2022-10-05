Herramientas utilizadas:
Microsoft Visual Studio Community 2019
Versión 16.11.19
Microsoft Sql Server v18.8
Microsoft Sql Server 2019 Express Edition

Version SDK de .NET:
SDK de .NET (5.0.412)

El archivo de la solución se encuentra en la ruta: TravelTest\WebSolucionTravelTest\WebSolucionTravelTest.sln

Paquetes Nuget instalados y utilizados con respecto a EntityFramework:
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools

Proyecto de Inicio: WebTravelTest

CadenaConexion BD en archivo: TravelTest\WebSolucionTravelTest\WebTravelTest\appsettings.json
Etiquetas: ConnectionStrings-->DefaultConnection

Se sube en repositorio backup BD:
Backup TravelTest.bak

Nombre BD SQL SERVER: TravelTest

Arquitectura Implementada - 5 Capas:
Capa Expuesta SITIO WEB MVC Front y Controllers - WebTravelTest
Capa de Negocio - WebTravelTest.Core
Capa de Modelo o Entidades - WebTravelTest.Entities
Capa de Repositorio o Acceso a Datos - WebTravelTest.Repositories
Capa pruebas unitarias nUnit - WebTravelTest.UnitTest

Pruebas unitarias se hacen realizando simulación BD en memoria con Sqlite - La BD in-memory solo existe cuando la conexión está abierta.
Para correr pruebas unitarias no se realiza apuntamiento a BD SQL SERVER, se hace en memoria inyectando contexto BD SQL LITE a repositorios.

Inyeccion de Dependencias en Metodo ConfigureServices archivo :
TravelTest\WebSolucionTravelTest\WebTravelTest\Startup.cs

Archivo EVIDENCIAS SOLUCION WebSolucionTravelTest:
EVIDENCIAS SOLUCION WebSolucionTravelTest.docx