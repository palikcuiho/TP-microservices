using BorrowingService.Clients;
using BorrowingService.Data;
using BorrowingService.Kafka;
using BorrowingService.Mappings;
using BorrowingService.Repositories;
using BorrowingService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration d'Entity Framework
builder.Services.AddDbContext<BorrowingContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));


// Injection des dependances
builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddScoped<IBorrowingsService, BorrowingsService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSingleton<KafkaProducer>();

builder.Services.AddHttpClient<IBookServiceClient, BookServiceClient>();
builder.Services.AddHttpClient<IUserServiceClient, UserServiceClient>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();

