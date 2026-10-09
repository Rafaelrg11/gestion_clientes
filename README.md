# Módulo Gestión de Clientes (C#) — GA7-220501096-AA2-EV02

Módulo web en **C# con ASP.NET Core MVC (.NET 8)**: controladores, formularios HTML y vistas Razor. Implementa un CRUD de clientes.

## Equivalencia con los conceptos de Java (servlets / JSP)
| Java | C# / ASP.NET Core |
|------|-------------------|
| Servlet (`doGet` / `doPost`) | Controlador con `[HttpGet]` / `[HttpPost]` |
| JSP | Vista Razor (`.cshtml`) |
| JSTL + EL | Sintaxis Razor (`@foreach`, `@if`, `@Model`) y Tag Helpers (`asp-action`, `asp-for`) |
| `include` de encabezado y pie | `_Layout.cshtml` |
| `web.xml` | `Program.cs` |
| Maven / JUnit | `dotnet` CLI / xUnit |

## Estructura (MVC)
| Capa | Archivo |
|------|---------|
| Modelo | `Models/Cliente.cs` (con validaciones DataAnnotations) |
| Acceso a datos | `Data/IClienteRepositorio.cs`, `Data/ClienteRepositorio.cs` |
| Controlador | `Controllers/ClientesController.cs` |
| Vistas | `Views/Clientes/Index.cshtml`, `Formulario.cshtml`, `Views/Shared/_Layout.cshtml` |

## Métodos HTTP
- **GET**: listar y buscar (`/Clientes?q=`), formulario nuevo (`/Clientes/Crear`), formulario editar (`/Clientes/Editar/1`).
- **POST**: crear, editar y eliminar, con token antifalsificación y patrón Post-Redirect-Get.

## Ejecución
1. Instalar el [SDK de .NET 8](https://dotnet.microsoft.com/download).
2. En la carpeta del proyecto:
   ```bash
   dotnet run --project GestionClientes
   ```
3. Abrir la URL que muestre la consola (por ejemplo `http://localhost:5000`).

También puede abrirse `RAFAELRODRIGUEZ_AA2_EV02.sln` en Visual Studio 2022 y presionar F5.

## Pruebas
- Unitarias (xUnit): `dotnet test`
- Funcionales: ver `docs/PLAN_DE_PRUEBAS.md`
