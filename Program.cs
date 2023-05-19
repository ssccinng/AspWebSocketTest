using AspWebSocketTest;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();
app.UseWebSockets();
app.MapControllers();
var sampleTodos = TodoGenerator.GenerateTodos().ToArray();

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();

