using Escola.Context;
using Escola.Repository;
using Escola.Repository.Interface;
using Escola.Services;
using Escola.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EscolaDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<IBoletimRepository, BoletimRepository>();
builder.Services.AddScoped<INotasMateriaRepository, NotasMateriaRepository>();

builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IBoletimService, BoletimService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Escola.API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Escola.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
