using GestionClientes.Data;
using GestionClientes.Models;
using Xunit;

namespace GestionClientes.Tests;

public class ClienteRepositorioTests
{
    private static Cliente Nuevo(string nombre = "Ana Rojas", string correo = "ana@correo.com") =>
        new() { Nombre = nombre, Correo = correo, Telefono = "3001234567" };

    [Fact]
    public void Crear_AsignaId()
    {
        var repo = new ClienteRepositorio();
        var c = repo.Crear(Nuevo());
        Assert.Equal(1, c.Id);
        Assert.NotNull(repo.BuscarPorId(1));
    }

    [Fact]
    public void Actualizar_CambiaDatos()
    {
        var repo = new ClienteRepositorio();
        var c = repo.Crear(Nuevo());
        c.Nombre = "Ana María Rojas";
        Assert.True(repo.Actualizar(c));
        Assert.Equal("Ana María Rojas", repo.BuscarPorId(c.Id)!.Nombre);
    }

    [Fact]
    public void Actualizar_Inexistente_DevuelveFalse()
    {
        var repo = new ClienteRepositorio();
        Assert.False(repo.Actualizar(new Cliente { Id = 99 }));
    }

    [Fact]
    public void Eliminar_QuitaElCliente()
    {
        var repo = new ClienteRepositorio();
        var c = repo.Crear(Nuevo());
        Assert.True(repo.Eliminar(c.Id));
        Assert.Null(repo.BuscarPorId(c.Id));
        Assert.False(repo.Eliminar(c.Id));
    }

    [Fact]
    public void Listar_FiltraPorNombreOCorreo()
    {
        var repo = new ClienteRepositorio();
        repo.Crear(Nuevo());
        repo.Crear(Nuevo("Luis Gómez", "luis@correo.com"));
        Assert.Single(repo.Listar("luis"));
        Assert.Equal(2, repo.Listar("").Count);
    }

    [Fact]
    public void ExisteCorreo_IgnoraMayusculasYElPropioId()
    {
        var repo = new ClienteRepositorio();
        var c = repo.Crear(Nuevo());
        Assert.True(repo.ExisteCorreo("ANA@correo.com", 0));
        Assert.False(repo.ExisteCorreo("ana@correo.com", c.Id));
    }
}
