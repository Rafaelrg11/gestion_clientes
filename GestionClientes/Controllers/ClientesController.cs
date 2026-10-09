using GestionClientes.Data;
using GestionClientes.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionClientes.Controllers;

/// <summary>
/// Controlador del modulo (patron MVC).
///  - GET  : consultar (listar, buscar, formulario nuevo, formulario editar).
///  - POST : modificar datos (crear, editar, eliminar).
/// </summary>
public class ClientesController : Controller
{
    private readonly IClienteRepositorio _repo;

    public ClientesController(IClienteRepositorio repo) => _repo = repo;

    // ---------------------------- GET ----------------------------

    [HttpGet]
    public IActionResult Index(string? q)
    {
        ViewBag.Q = q ?? string.Empty;
        return View(_repo.Listar(q));
    }

    [HttpGet]
    public IActionResult Crear()
    {
        ViewData["Titulo"] = "Registrar cliente";
        return View("Formulario", new Cliente());
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        var cliente = _repo.BuscarPorId(id);
        if (cliente is null)
        {
            TempData["Error"] = "El cliente no existe.";
            return RedirectToAction(nameof(Index));
        }
        ViewData["Titulo"] = "Editar cliente";
        return View("Formulario", cliente);
    }

    // ---------------------------- POST ----------------------------

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Crear(Cliente cliente)
    {
        ValidarCorreoUnico(cliente);
        if (!ModelState.IsValid)
        {
            ViewData["Titulo"] = "Registrar cliente";
            return View("Formulario", cliente);
        }

        cliente.Nombre = cliente.Nombre.Trim();
        _repo.Crear(cliente);
        TempData["Ok"] = "Cliente registrado correctamente.";
        return RedirectToAction(nameof(Index)); // Post-Redirect-Get
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Editar(Cliente cliente)
    {
        ValidarCorreoUnico(cliente);
        if (!ModelState.IsValid)
        {
            ViewData["Titulo"] = "Editar cliente";
            return View("Formulario", cliente);
        }

        cliente.Nombre = cliente.Nombre.Trim();
        if (_repo.Actualizar(cliente))
            TempData["Ok"] = "Cliente actualizado correctamente.";
        else
            TempData["Error"] = "El cliente no existe.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id)
    {
        if (_repo.Eliminar(id))
            TempData["Ok"] = "Cliente eliminado correctamente.";
        else
            TempData["Error"] = "El cliente no existe.";
        return RedirectToAction(nameof(Index));
    }

    // ------------------------ Utilidades ------------------------

    private void ValidarCorreoUnico(Cliente cliente)
    {
        if (!string.IsNullOrWhiteSpace(cliente.Correo) &&
            _repo.ExisteCorreo(cliente.Correo, cliente.Id))
        {
            ModelState.AddModelError(nameof(Cliente.Correo), "Ese correo ya está registrado.");
        }
    }
}
