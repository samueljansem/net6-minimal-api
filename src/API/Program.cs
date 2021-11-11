using Microsoft.EntityFrameworkCore;
using Minimal.API.Data;
using Minimal.API.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Defining services
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Building our app
var app = builder.Build();

// Activate Swagger
app.UseSwagger();

// Mapping routes
app.MapGet("/persons", async (AppDbContext _context) =>
{
    var persons = await _context.Persons.ToListAsync();

    return Results.Ok(persons);
});

app.MapGet("/persons/{id}", async (AppDbContext _context, Guid id) =>
{
    var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

    return Results.Ok(person);
});

app.MapPost("/persons", async (AppDbContext _context, PersonViewModel model) =>
{
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    var person = model.MapTo();

    await _context.Persons.AddAsync(person);
    await _context.SaveChangesAsync();

    return Results.Created($"/persons/{person.Id}", person);
});

app.MapPut("/persons/{id}", async (AppDbContext _context, Guid id, PersonViewModel model) =>
{
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    var person = model.MapTo(id);

    _context.Persons.Update(person);
    await _context.SaveChangesAsync();

    return Results.Ok(person);
});

app.MapDelete("/persons/{id}", async (AppDbContext _context, Guid id) =>
{
    var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

    if (person != null)
    {
        _context.Persons.Remove(person);
        return Results.Ok(person);
    }

    return Results.BadRequest();
});

// Activate Swagger UI
app.UseSwaggerUI();

// Running our app
app.Run();