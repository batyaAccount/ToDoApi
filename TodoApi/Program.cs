using Microsoft.EntityFrameworkCore;
using TodoApi;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddDbContext<ToDoDbContext>();
builder.Services.AddControllers();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowAllOrigins");

app.UseAuthorization();
app.MapControllers();


app.MapGet("/items", async (ToDoDbContext dbContext) =>
{
    var results = await dbContext.Items.ToListAsync();
    return Results.Ok(results);
});
app.MapPost("/items", async (Item item, ToDoDbContext dbContext) =>
{
    if (item == null)
        return Results.BadRequest();
    await dbContext.Items.AddAsync(item);
    await dbContext.SaveChangesAsync();
    return Results.Ok(item);
});
app.MapPut("/items/{id}", async (int id, Item item, ToDoDbContext dbContext) =>
{
    if (id < 0)
        return Results.BadRequest();
    var tm = await dbContext.Items.FindAsync(id);
    if (tm == null)
        return Results.NotFound();
    tm.IsComplete = item.IsComplete;
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/items/{id}", async (int id, ToDoDbContext dbContext) =>
{
    if (id < 0)
        return Results.BadRequest();
    var tm = await dbContext.Items.FindAsync(id);
    if (tm == null)
        return Results.BadRequest();
    dbContext.Items.Remove(tm);
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();