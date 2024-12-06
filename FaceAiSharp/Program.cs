using FaceAiSharpApi.Extentions;
using FaceAiSharpApi.Repositorys;
using FaceAiSharpApi.Repositorys.IRepositorys;
using FaceAiSharpApi.Services;
using FaceAiSharpApi.Services.IServices;
using MathNet.Numerics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFaceAiRepository, FaceAiRepository>();
builder.Services.AddScoped<IFaceAiService, FaceAiService>();

builder.Services.AddSingleton(new ConfHelper() { ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
