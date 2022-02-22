var builder = WebApplication.CreateBuilder(args);

// Abhängigkeiten registrieren
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services.AddSingleton<IGreeter, Greeter>();

var app = builder.Build();

// Beispiel mit einfachen GET-Requests
app.MapGet("/", () => "Hello World!");
app.MapGet("/{name}", (string name) => $"Hello {name}!");

// Beispiel mit Authorization.
// Authentication und Authorization muss dann auf builder.Services hinzugefügt werden.
//app.MapGet("/{name}", [Authorize] (string name) => $"Hello {name}!");

// Beispiel mit POST-Requests
app.MapPost("/", (InputModel model) => $"Hello {model.Name}!");

// Beispiel mit Dependency Injection
app.MapGet("/greet/{name}", (string name, IGreeter greeter) => greeter.Greet(name));
app.MapGet("/static-greeter/{name}", Handler.Index);

// Beispiel mit Statuscodes.
app.MapGet("/TT", () => Results.Redirect("https://www.thinktecture.com/"));
app.MapGet("/Teapot", () => Results.StatusCode(StatusCodes.Status418ImATeapot));

app.UseSwagger();
app.UseSwaggerUI();
app.Run();

internal record InputModel(string Name);

internal interface IGreeter
{
    string Greet(string name);
}

internal class Greeter : IGreeter
{
    public string Greet(string name) => $"Hello {name}!";
}

internal static class Handler
{
    public static string Index(string name, IGreeter greeter) => greeter.Greet(name);
}