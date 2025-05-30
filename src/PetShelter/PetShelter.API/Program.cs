using Microsoft.EntityFrameworkCore;
using PetShelter.API.Extensions;
using PetShelter.API.Services;
using PetShelter.Application.Extensions;
using PetShelter.Grpc;
using PetShelter.Infrastructure;
using PetShelter.Infrastructure.DataSeeders;
using PetShelter.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<PetDbContext>();
await dbContext.Database.MigrateAsync();
var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
await dataSeeder.SeedAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGrpcService<PetServiceGrpc>();

app.Run();