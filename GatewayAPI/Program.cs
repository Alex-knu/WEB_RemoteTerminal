using System.Reflection;
using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Services;
using GatewayAPI.Extentions.Extentions;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.AddGlobalErrorHandler();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
