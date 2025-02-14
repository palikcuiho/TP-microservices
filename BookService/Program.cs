using BookService.Data;
using BookService.Kafka;
using BookService.Repositories;
using BookService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration d'Entity Framework
builder.Services.AddDbContext<BookContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));


// Injection des dependances
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddSingleton<KafkaProducer>();

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

