using System.Reflection;
using System.Text;
using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Services;
using GatewayAPI.Extentions.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
// Add services to the container.

var configuration = builder.Configuration.GetSection("GatewayServer");

builder.Services
    .AddHttpClient("RemoteServer", client =>
    {
        client.BaseAddress = new Uri(configuration.GetValue<string>("RemoteAPIRoute"));
    });
builder.Services
    .AddHttpClient("DataServer", client =>
    {
        client.BaseAddress = new Uri(configuration.GetValue<string>("DataAPIRoute"));
    });

// Add services to the container.

builder.Services.AddScoped(typeof(IRouteService), typeof(RouteService));
builder.Services.AddScoped(typeof(IRemoteService), typeof(RemoteService));
builder.Services.AddScoped(typeof(ICommandHistoryService), typeof(CommandHistoryService));
builder.Services.AddScoped(typeof(IMachineService), typeof(MachineService));
builder.Services.AddScoped(typeof(IMachineUserService), typeof(MachineUserService));
builder.Services.AddScoped(typeof(ISystemUserToMachineUserService), typeof(SystemUserToMachineUserService));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var tokenConfiguration = builder.Configuration.GetSection("JWT");
JwtUtils.SecretKey = tokenConfiguration.GetValue<string>("Secret");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = tokenConfiguration.GetValue<string>("ValidAudience"),
            ValidIssuer = tokenConfiguration.GetValue<string>("ValidIssuer"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.GetValue<string>("Secret")))
        };
    });

var app = builder.Build();

app.UseCors(
    builder => builder
        .WithOrigins(
        "http://localhost:1418",
        "https://localhost:5001",
        "http://localhost:5000",
        "http://web_app")
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.AddGlobalErrorHandler();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
