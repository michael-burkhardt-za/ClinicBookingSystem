using Application;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;
using FluentValidation;
using Application.Validators;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connStr = config.GetConnectionString("DefaultConnection");
    return new SqlConnection(connStr); // System.Data.SqlClient or Microsoft.Data.SqlClient
});

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
builder.Services.AddScoped<IClinicService, ClinicService>();

builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<AppointmentBookingValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClinicValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PatientValidator>();
 


//Only for demo purposes to allow the client to connect during development.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.WithOrigins("https://localhost:7027")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//"Global exception handling is implemented using ASP.NET Core’s UseExceptionHandler middleware.
//Validation exceptions are mapped to 400 responses, while unexpected errors return standardized ProblemDetails responses."

app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext context, ILogger<Program> logger) =>
{
    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

    if (exception != null)
    {
        logger.LogError(exception, "Unhandled exception");
    }

    if (exception is ValidationException validationException)
    {
        return Results.BadRequest(validationException.Errors
            .Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage
            }));
    }
    if (exception is KeyNotFoundException ex)
    {
        return Results.NotFound(new ProblemDetails
        {
            Title = "Resource Not Found",
            Detail = ex.Message,
            Status = 404
        });
    }

    return Results.Problem(
        title: "An unexpected error occurred.",
        statusCode: 500);
});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseCors("AllowClient");
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
