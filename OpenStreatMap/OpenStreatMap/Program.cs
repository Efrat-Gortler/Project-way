using OpenStreatMap.Dal;
using OpenStreatMap.Manager;
//using OpenStreetMap.Manager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<OpenStreetMapService>();
builder.Services.AddScoped<MongoDBContext>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<VehicleManager>();
//builder.Services.AddScoped<DijkstraFunction>();
//builder.AddHttpContextAccessor();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
