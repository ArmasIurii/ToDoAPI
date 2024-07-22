using TodoAPI.AppDataContext;
using TodoAPI.Middleware;
using TodoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // Add this line

builder.Services.AddProblemDetails();  // Add this line

// Adding of login 
builder.Services.AddLogging();  //  Add this line


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  // Add this line


 // Add  This to in the Program.cs file
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings")); // Add this line
builder.Services.AddSingleton<TodoDbContext>(); // Add this line


builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); 

builder.Services.AddProblemDetails(); 
builder.Services.AddLogging();



builder.Services.AddScoped<TodoAPI.Interface.ITodoServices, TodoAPI.Services.TodoServices>();

// Add this line
var app = builder.Build();

{
    using var scope = app.Services.CreateScope(); // Add this line
    var context = scope.ServiceProvider; // Add this line
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Add this line

app.UseExceptionHandler();
app.UseAuthorization();

app.MapControllers();

app.Run();