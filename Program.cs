using Task_Novin_Teck.Repository;
using Microsoft.Extensions.Caching.Memory;
using Amazon.S3;
using MediatR;
using Task_Novin_Teck.RabbitMQ;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSService<IAmazonS3>();



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHostedService<UserCreationConsumer>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Register other services
builder.Services.AddScoped<UserRepository>(provider =>
{
    var memoryCache = provider.GetRequiredService<IMemoryCache>();
    
    return new UserRepository(connectionString, memoryCache);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
