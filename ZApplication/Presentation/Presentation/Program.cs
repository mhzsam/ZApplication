using Application.Helper;
using Application.SetUp;
using Infrastructure.SetUp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    
    options.Filters.Add(typeof(DataAnotationException));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddApplicationDBContext(connectionString);
builder.Services.AddJWTConfig(builder.Configuration);
builder.Services.AddJWT();
builder.Services.AddAllApplicationServices();
builder.Services.AddInfrastructureService();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
