using GestionClientes.Models;

namespace GestionClientes.Data;

public interface IClienteRepositorio
{
    Cliente Crear(Cliente cliente);
    bool Actualizar(Cliente cliente);
    bool Eliminar(int id);
    Cliente? BuscarPorId(int id);
    IReadOnlyList<Cliente> Listar(string? filtro);
    bool ExisteCorreo(string correo, int idExcluido);
}
