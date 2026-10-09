using GestionClientes.Data;
using GestionClientes.Models;

var builder = WebApplication.CreateBuilder(args);

// Servicios: MVC (controladores + vistas Razor) y el repositorio en memoria
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IClienteRepositorio, ClienteRepositorio>();

var app = builder.Build();

// Datos de ejemplo para la primera ejecucion
var repo = app.Services.GetRequiredService<IClienteRepositorio>();
if (!repo.Listar(null).Any())
{
    repo.Crear(new Cliente { Nombre = "Ana María Rojas", Correo = "ana.rojas@correo.com", Telefono = "3001234567" });
    repo.Crear(new Cliente { Nombre = "Carlos Pérez", Correo = "carlos.perez@correo.com", Telefono = "3109876543" });
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Clientes/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clientes}/{action=Index}/{id?}");

app.Run();
