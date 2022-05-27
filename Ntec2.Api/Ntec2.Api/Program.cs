using Microsoft.EntityFrameworkCore;
using Ntec2.Api.Database;
using Ntec2.Api.Globals;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Genero la cadena de conexion cuando NO ESTOY USANDO ENTITYFRAMEWORK
GlobalsDb.ConnectionString = builder.Configuration.GetConnectionString("SqliteConnection");

//Genero la cadena de conexion cuando SI ESTOY USANDO ENTITYFRAMEWORK
builder.Services.AddDbContext<Contexto>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
}));


var app = builder.Build();

app.UseCors();

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
