using DataManagerAPI.Core.Interfaces;
using DataManagerAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataManagerApiDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IMachineRepository, MachineRepository>();
builder.Services.AddTransient<IMachineUserRepository, MachineUserRepository>();
builder.Services.AddTransient<ICommandHistoryRepository, CommandHistoryRepository>();
builder.Services.AddTransient<ISystemUserToMachineUserRepository, SystemUserToMachineUserRepository>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
