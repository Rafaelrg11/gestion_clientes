using System.Collections.Concurrent;
using GestionClientes.Models;

namespace GestionClientes.Data;

/// <summary>
/// Repositorio en memoria para que el proyecto corra sin base de datos.
/// Para usar SQL Server/MySQL se crea otra clase que implemente
/// IClienteRepositorio (por ejemplo con Entity Framework Core) sin tocar
/// el controlador ni las vistas.
/// </summary>
public class ClienteRepositorio : IClienteRepositorio
{
    private readonly ConcurrentDictionary<int, Cliente> _datos = new();
    private int _secuencia;

    public Cliente Crear(Cliente cliente)
    {
        cliente.Id = Interlocked.Increment(ref _secuencia);
        _datos[cliente.Id] = cliente;
        return cliente;
    }

    public bool Actualizar(Cliente cliente)
    {
        if (!_datos.ContainsKey(cliente.Id)) return false;
        _datos[cliente.Id] = cliente;
        return true;
    }

    public bool Eliminar(int id) => _datos.TryRemove(id, out _);

    public Cliente? BuscarPorId(int id) => _datos.TryGetValue(id, out var c) ? c : null;

    public IReadOnlyList<Cliente> Listar(string? filtro)
    {
        var f = (filtro ?? string.Empty).Trim();
        return _datos.Values
            .Where(c => f.Length == 0
                     || c.Nombre.Contains(f, StringComparison.OrdinalIgnoreCase)
                     || c.Correo.Contains(f, StringComparison.OrdinalIgnoreCase))
            .OrderBy(c => c.Id)
            .ToList();
    }

    public bool ExisteCorreo(string correo, int idExcluido) =>
        _datos.Values.Any(c => c.Id != idExcluido &&
                               string.Equals(c.Correo, correo, StringComparison.OrdinalIgnoreCase));
}
