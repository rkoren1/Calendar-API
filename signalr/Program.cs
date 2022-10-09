
using Microsoft.EntityFrameworkCore;
using signalr.Data;
using signalr.HubConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
var connectionString = builder.Configuration.GetConnectionString("CalendarDbConnectionString");
builder.Services.AddDbContext<CalendarDbContext>(options =>
{
    options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", corsBuilder =>
        corsBuilder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();
app.MapHub<CalendarEventHub>("/calendarevent");


app.Run();
