using api.Data;
using api.Interfaces;
using api.Repositories;
using api.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardManagementService, CardManagementService>();
builder.Services.AddScoped<TranslationService>();

//add HangFire
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseHangfireDashboard();
app.UseHangfireServer();

// Ќастраиваем периодическую задачу, котора€ выполн€етс€ каждые 30 минут
RecurringJob.AddOrUpdate<CardManagementService>(
    "check-cards-status",
    service => service.CheckAllCardsStatus(),
    Cron.MinuteInterval(interval: 1)); // »нтервал проверки можно настроит

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
