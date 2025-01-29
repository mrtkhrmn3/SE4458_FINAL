using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PrescriptionService.Schedulers;
using Quartz.Impl;
using Quartz;
using System.Text;
using PrescriptionService.Services;
using PrescriptionService.Repositories;
using PrescriptionService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MongoDB Setup
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddSingleton<IMongoDatabase>(s =>
    s.GetService<IMongoClient>().GetDatabase("prescription_doctor_system_db"));

// Quartz.NET Scheduler Ayarlarý
var schedulerFactory = new StdSchedulerFactory();
var scheduler = await schedulerFactory.GetScheduler();
await scheduler.Start();

// Zamanlanmýþ Görev (Job) Tanýmlamasý
var job = JobBuilder.Create<MedicineUpdateJob>()
    .WithIdentity("MedicineUpdateJob")
    .Build();

// Trigger (Çalýþma Zamaný) Ayarlarý
var trigger = TriggerBuilder.Create()
    .WithIdentity("MedicineUpdateTrigger")
    .WithCronSchedule("0 0 22 ? * SUN") // Pazar 22:00
    .Build();

// Görevi Zamanlayýcýya Ekle
await scheduler.ScheduleJob(job, trigger);

builder.Services.AddSingleton(scheduler);

// Add Services
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionsService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});



var app = builder.Build();

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
