

using ApiCarteiraInvestimentos.Repositories;
using ApiCarteiraInvestimentos.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ApiCarteiraInvestimentos",
        Version = "v1"
    });
    c.EnableAnnotations();
});

builder.Services.AddScoped<IAtivoRepository, AtivoRepository>();
builder.Services.AddScoped<AtivoRepository>();
builder.Services.AddScoped<AtivoService>();
builder.Services.AddScoped<ICarteiraRepository, CarteiraRepository>();
builder.Services.AddScoped<CarteiraRepository>();
builder.Services.AddScoped<CarteiraService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();