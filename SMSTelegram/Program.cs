using SMSTelegram.Application.Seeder;
using SMSTelegram.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "SMS Telegram API",
        Version = "v1"
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


builder
    .AddData()
    .AddOptions()
    .AddApplicationServices()
    .AddIntegrationServices()
    .AddHostedServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SMS API V1");
});

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    await CountrySeeder.SeedAsync(services);
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка при сеединге: {ex.Message}");
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.MapControllers();
app.UseHttpsRedirection();
app.Run();