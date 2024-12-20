using AutoMapper;
using Policlinic.Domain;
using Policlinic.Domain.Repositories;
using Policlinic.Server;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure AutoMapper
var mapperConfig = new MapperConfiguration(config => config.AddProfile(new Mapping()));
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Register repositories
builder.Services.AddScoped<IRepository<Patient, int>, PatientRepository>();
builder.Services.AddScoped<IRepository<Doctor, int>, DoctorRepository>();
builder.Services.AddScoped<IRepository<Reception, int>, ReceptionRepository>();

// Configure DbContext
builder.Services.AddDbContext<PoliclinicDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySql")!, ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySql")!)));

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:8080"));

app.UseAuthorization();

app.MapControllers();

app.Run();
