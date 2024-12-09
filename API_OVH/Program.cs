using API_OVH;
using API_OVH.Models.DataManager;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Manager;
using API_OVH.Models.Repository;
using Microsoft.EntityFrameworkCore;
using static API_OVH.Models.Repository.IUniteRepository;

var MyAllowSpecificOrigins = "AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, builder =>
    {
        builder.WithOrigins("http://localhost:5258", "https://localhost:5258") // Allow this origin
               .AllowAnyMethod()                     // Allow any HTTP methods
               .AllowAnyHeader();                    // Allow any headers
    });
});

// Ajouter les services Scoped pour les managers
builder.Services.AddScoped<IBatimentRepository<Batiment, BatimentDTO, BatimentSansNavigationDTO>, BatimentManager>();
builder.Services.AddScoped<ICapteurRepository<Capteur, CapteurDTO, CapteurDetailDTO, CapteurSansNavigationDTO>, CapteurManager>();

builder.Services.AddScoped<IEquipementRepository<Equipement, EquipementDTO, EquipementDetailDTO, EquipementSansNavigationDTO>, EquipementManager>();
builder.Services.AddScoped<ISalleRepository<Salle, SalleSansNavigationDTO, SalleDTO, SalleDetailDTO>, SalleManager>();
builder.Services.AddScoped<ITypeEquipementRepository<TypeEquipement, TypeEquipementDTO>, TypeEquipementManager>();
builder.Services.AddScoped<ITypeEquipementRepository<TypeSalle, TypeSalleDTO>, TypeSalleManager>();
builder.Services.AddScoped<IUniteRepository<Unite, UniteDTO, UniteDetailDTO>, UniteManager>();
builder.Services.AddScoped<IMurRepository<Mur, MurDTO, MurSansNavigationDTO>, MurManager>();

// Managers ReadOnly
builder.Services.AddScoped<IDirectionRepository<Direction>, DirectionManager>();

// Auto Mapping
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connexion à la base de données
builder.Services.AddDbContext<SAE5_BD_OVH_DbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionLocale")));

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
