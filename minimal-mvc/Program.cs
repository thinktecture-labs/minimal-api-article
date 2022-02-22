using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public class HelloController : Controller
{
    [HttpGet("/hello/{name}")]
    public IActionResult Index(string name) => Ok($"Hello {name}!");
}