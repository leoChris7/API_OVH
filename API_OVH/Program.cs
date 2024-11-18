using API_OVH;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Manager;
using GestionProduit_API.Models.Repository;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, builder =>
    {
        builder.WithOrigins("https://localhost:7245") // Allow this origin
               .AllowAnyMethod()                     // Allow any HTTP methods
               .AllowAnyHeader();                    // Allow any headers
    });
});

// Connexion à la base de données
builder.Services.AddDbContext<SAE5_BD_OVH_DbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionLocale")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Auto Mapping
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Ajouter les services Scoped pour les managers
builder.Services.AddScoped<BatimentManager>();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
