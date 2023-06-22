using FluentValidation;
using Prueba2.CQRS.Features.Personas.Commands.CreatePersona;
using Prueba2.CQRS.Features.Personas.Commands.DeletePersona;
using Prueba2.CQRS.Features.Personas.Commands.UpdatePersona;
using Prueba2.CQRS.Features.Personas.Queries.GePersonasByNombreApellido;
using Prueba2.CQRS.Features.Personas.Queries.GetPersonasById;
using Prueba2.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(config =>
{
    config.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddDbContext<EfdatabaseFirstContext>();

builder.Services.AddScoped<IValidator<DeletePersonaCommand>, DeletePersonaCommandValidator>();
builder.Services.AddScoped<IValidator<UpdatePersonaCommand>, UpdatePersonaCommandValidator>();
builder.Services.AddScoped<IValidator<CreatePersonaCommand>, CreatePersonaCommandValidator>();
builder.Services.AddScoped<IValidator<GetPersonasByIdQuery>, GetPersonaByIdQueryValidator>();
builder.Services.AddScoped<IValidator<GetPersonasByNombreApellidoQuery>, GetPersonasByNombreApellidoQueryValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
