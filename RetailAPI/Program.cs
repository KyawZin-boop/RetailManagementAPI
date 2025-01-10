using BAL.Shared;
using Model.AppConfig;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Replace with your front-end URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration.GetSection("AppSettings").Bind(appSettings);
ServiceManager.SetServiceInfo(builder.Services, appSettings);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Enable the CORS policy globally
app.UseCors("AllowSpecificOrigin");

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
