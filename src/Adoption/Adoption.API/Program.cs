using Adoption.API.Repositories;
using Adoption.API.Services;
using PetShelter.Grpc;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IAdoptionService, AdoptionService>();
builder.Services.AddControllers();

builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(configuration!);
});

builder.Services.AddGrpcClient<PetService.PetServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:PetsUrl"]!);
});

var app = builder.Build();

app.MapControllers();

app.Run();