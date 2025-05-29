using Hospital.API.Data;
using Hospital.API.Data.EmailServices;
using Hospital.API.DataSeeders;
using Hospital.API.EventHandlers.ConsumingEvents;
using Hospital.API.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.AddConsumer<PetTransferredToHospitalConsumer>();
    
    config.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), host =>
        {
            host.Username(builder.Configuration["MessageBroker:UserName"]);
            host.Password(builder.Configuration["MessageBroker:Password"]);
        });
        configurator.ConfigureEndpoints(context);
    });
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddDbContext<HospitalDbContext>(z=>
    z.UseSqlServer(builder.Configuration.GetConnectionString("HospitalDB")));

builder.Services.AddScoped<IDataSeeder, DataSeeder>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
await dbContext.Database.MigrateAsync();
var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
await dataSeeder.Seed();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();