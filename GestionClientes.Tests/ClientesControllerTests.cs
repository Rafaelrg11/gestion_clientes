using GestionClientes.Controllers;
using GestionClientes.Data;
using GestionClientes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit;

namespace GestionClientes.Tests;

public class ClientesControllerTests
{
    /// <summary>Proveedor de TempData falso, solo para pruebas.</summary>
    private class TempDataFalso : ITempDataProvider
    {
        public IDictionary<string, object> LoadTempData(HttpContext context) => new Dictionary<string, object>();
        public void SaveTempData(HttpContext context, IDictionary<string, object> values) { }
    }

    private static ClientesController CrearControlador(IClienteRepositorio repo)
    {
        var controlador = new ClientesController(repo);
        controlador.TempData = new TempDataDictionary(new DefaultHttpContext(), new TempDataFalso());
        return controlador;
    }

    [Fact]
    public void Index_Get_DevuelveVistaConLista()
    {
        var repo = new ClienteRepositorio();
        repo.Crear(new Cliente { Nombre = "Ana", Correo = "a@a.com", Telefono = "3001234567" });
        var resultado = CrearControlador(repo).Index(null);
        var vista = Assert.IsType<ViewResult>(resultado);
        Assert.Single((IEnumerable<Cliente>)vista.Model!);
    }

    [Fact]
    public void Crear_Post_Valido_RedirigeAlListado()
    {
        var repo = new ClienteRepositorio();
        var resultado = CrearControlador(repo).Crear(
            new Cliente { Nombre = "Ana Rojas", Correo = "ana@correo.com", Telefono = "3001234567" });
        Assert.IsType<RedirectToActionResult>(resultado);
        Assert.Single(repo.Listar(null));
    }

    [Fact]
    public void Crear_Post_CorreoDuplicado_VuelveAlFormulario()
    {
        var repo = new ClienteRepositorio();
        repo.Crear(new Cliente { Nombre = "Ana", Correo = "ana@correo.com", Telefono = "3001234567" });
        var controlador = CrearControlador(repo);
        var resultado = controlador.Crear(
            new Cliente { Nombre = "Otra Ana", Correo = "ana@correo.com", Telefono = "3001234567" });
        Assert.IsType<ViewResult>(resultado);
        Assert.False(controlador.ModelState.IsValid);
        Assert.Single(repo.Listar(null));
    }

    [Fact]
    public void Eliminar_Post_QuitaElRegistro()
    {
        var repo = new ClienteRepositorio();
        var c = repo.Crear(new Cliente { Nombre = "Ana", Correo = "a@a.com", Telefono = "3001234567" });
        CrearControlador(repo).Eliminar(c.Id);
        Assert.Empty(repo.Listar(null));
    }

    [Fact]
    public void Editar_Get_Inexistente_Redirige()
    {
        var resultado = CrearControlador(new ClienteRepositorio()).Editar(999);
        Assert.IsType<RedirectToActionResult>(resultado);
    }
}
