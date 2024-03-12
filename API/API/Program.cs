using CaptchaConfigurations.ExtensionMethod;
using Domain.Entities;
using Driver.API.Common;
using Driver.API.Common.Middlewares;
using Hangfire;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Web.IOC;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
static void AutoRegisterServices(IServiceCollection services, string environmentName)
{
    var executingAssm = Assembly.GetExecutingAssembly();
    var dir = Path.GetDirectoryName(executingAssm.Location);
    Assembly.LoadFrom(Path.Combine(dir, "Driver.Domain.dll"));
    Assembly.LoadFrom(Path.Combine(dir, "Driver.Service.dll"));
    Assembly.LoadFrom(Path.Combine(dir, "Driver.Infrastructure.dll"));
    //Assembly.LoadFrom(Path.Combine(dir, "Moneyon.PowerBi.External.Service.dll"));
    Assembly.LoadFrom(Path.Combine(dir, "Driver.API.dll"));

    new ServiceRegistrar(services, environmentName, "Driver").Register();
}


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JsonWebTokenKeys"));

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("AppConnectionString");

    options.UseSqlServer(connectionString, p =>
    {
        p.CommandTimeout(100000);
        p.EnableRetryOnFailure(3);
    });
});

AutoRegisterServices(builder.Services, builder.Environment.EnvironmentName);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
}).AddJsonOptions(options =>
{
    var enumConverter = new JsonStringEnumConverter();
    options.JsonSerializerOptions.Converters.Add(enumConverter);
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
}).ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var modelState = actionContext.ModelState.Values;

        StringBuilder sb = new StringBuilder();
        if (!actionContext.ModelState.IsValid)
        {
            foreach (var item in actionContext.ModelState)
            {
                foreach (var err in item.Value.Errors)
                {
                    sb.AppendLine(err.ErrorMessage);
                }
            }
        }

        object response;
        response = new
        {
            code = -1,
            message = sb.ToString()
        };

        return new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    };
}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "توکن الزامی است"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

#region mapper
builder.Services.AddAutoMapper(typeof(UserMappingProfile));

#endregion

#region Captcha

builder.Services.UseCaptcha(new CaptchaConfigurations.CaptchaOptionsDTO.CaptchaOptions()
{
    CaptchaType = CaptchaConfigurations.CaptchaOptionsDTO.CaptchaType.Numbers,
    CaptchaCharacter = 5,
    FontStyle = SixLabors.Fonts.FontStyle.Regular,
    CaptchaValueSendType = CaptchaConfigurations.CaptchaOptionsDTO.CaptchaValueSendType.InBody,
    Height = 40,
    Width = 100,
    NoiseRate = 200,
    DrawLines = 3,
    FontFamilies = new string[] { "Arail", "Verdana", "Hack" }

});

#endregion

#region Auth Configs
builder.Services.AddTransient<JwtTokenService>();
#endregion

builder.Services.AddCors();

//builder.Services.AddHostedService<ReverseAddressBackgroundService>();

builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("AppConnectionString")));
builder.Services.AddHangfireServer();

//builder.Services.AddScoped<ReverseAddressBackgroundService>();

var app = builder.Build();

app.UseMiddleware<JwtMiddleware>();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(c =>
{
    c.AllowAnyOrigin()
     .AllowAnyHeader()
     .AllowAnyMethod();
});

//app.UseHsts();
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.UseDefaultFiles("/index.html");
app.MapFallbackToFile("index.html");

app.UseHangfireDashboard();

app.Run();
