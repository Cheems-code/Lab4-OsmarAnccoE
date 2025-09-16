using Lab4_OsmarAnccoE.Models;
using Lab4_OsmarAnccoE.Repository;
using Lab4_OsmarAnccoE.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// A. Configuración de Entity Framework Core con PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AdbContextn>(options =>
    options.UseNpgsql(connectionString));

// B. Registrar el Unit of Work y los Repositorios para la Inyección de Dependencias.
//    Se usa AddScoped para que la instancia de UnitOfWork (y su DbContext) dure
//    lo mismo que una solicitud HTTP completa. ¡Esto es crucial!
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// C. Habilitar el uso de Controladores para la API
builder.Services.AddControllers();

// D. Configuración de Swagger (Swashbuckle) para documentar la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- Construir la aplicación ---
var app = builder.Build();

// --- 2. CONFIGURACIÓN DEL PIPELINE DE SOLICITUDES HTTP (MIDDLEWARE) ---

// A. Habilitar Swagger solo en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// B. Middleware estándar
app.UseHttpsRedirection();

app.UseAuthorization();

// C. Indicarle a la aplicación que use las rutas definidas en los Controladores
app.MapControllers();

// --- Ejecutar la aplicación ---
app.Run();