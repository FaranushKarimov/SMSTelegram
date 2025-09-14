using SMSTelegram.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder
    .AddSwagger()
    .AddData()
    .AddOptions()
    .AddApplicationServices()
    .AddIntegrationServices()
    .AddHostedServices();
    
    
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.UseHttpsRedirection();




app.Run();