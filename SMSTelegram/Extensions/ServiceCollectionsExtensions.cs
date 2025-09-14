using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SMSTelegram.Application.Abstractions;
using SMSTelegram.Application.Options;
using SMSTelegram.Application.Services;
using SMSTelegram.Domain;
using Telegram.Bot;

namespace SMSTelegram.Extensions;

public static class ServiceCollectionsExtensions
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SmsTelegram API", 
                Version = "v1"
            });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return builder;
    }
    
    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<SmsDbContext>(opt => 
            opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
        
        return builder;
    }
    
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        return builder;
    }
    
    public static WebApplicationBuilder AddIntegrationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddHttpClient<ISmsService,SmsService>();
        builder.Services.AddScoped<ILoggerService, LoggerService>();
        builder.Services.AddScoped<ISendSmsToTelegramNumService, SendSmsToTelegramNumService>();
        builder.Services.AddSingleton<ITelegramBotClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<TelegramBotOptions>>().Value;
            return new TelegramBotClient(options.Token);
        });
        return builder;
    }
    
    public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JasminOptions>(
            builder.Configuration.GetSection("Jasmin"));
        builder.Services.Configure<TelegramBotOptions>(
            builder.Configuration.GetSection("TelegramBot"));
        return builder;
    }
    
    
    public static WebApplicationBuilder AddHostedServices(this WebApplicationBuilder builder)
    {
        return builder;
    }
}