using Microsoft.EntityFrameworkCore;
using Api_Wave.Models;
using Api_Wave.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ChatwaveContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"))
    );
builder.Services.AddScoped<IGeneralService, GeneralService>();
builder.Services.AddScoped<IMensajeService, MensajeService>();
builder.Services.AddScoped<IContactoService, ContactoService>();
builder.Services.AddScoped<IPersonaService, PersonaService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
