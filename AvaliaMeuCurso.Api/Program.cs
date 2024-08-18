using AvaliaMeuCurso.Domain.Interfaces;
using AvaliaMeuCurso.Application.Service;
using AvaliaMeuCurso.Application.Mappings;
using AvaliaMeuCurso.Infrastructure.Repositories;
using AvaliaMeuCurso.Application.Interfaces.Service;
using AvaliaMeuCurso.Application.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AvaliaMeuCurso.Api",
        Version = "v1",
        Description = "Sistema voltado para que o aluno possa avaliar os cursos que participar",
    });
    c.EnableAnnotations();
});

// Registrar repositórios
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<IEstudanteRepository, EstudanteRepository>();

// Registrar serviços
builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IEstudanteService, EstudanteService>();

// Registrar helper
builder.Services.AddScoped<IValidadorErro, ValidadorErro>();

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AvaliaMeuCurso API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
