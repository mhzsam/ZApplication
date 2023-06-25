using Application.Helper;
using Application.SetUp;
using Domain.Context;
using Infrastructure.SetUp;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddApplicationDBContext(connectionString);
builder.Services.AddJWTConfig(builder.Configuration);
builder.Services.AddJWT();
builder.Services.AddAllApplicationServices();
builder.Services.AddInfrastructureService();
builder.Services.AddDataAnnotationReturnData();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Presentation.SetUp.AuthorizationSeedData.AuthorizationControllerSeedData(
                    builder.Services.BuildServiceProvider().GetRequiredService<ApplicationDBContext>(),
                    Assembly.GetExecutingAssembly()
                    );
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
