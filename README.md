# .NET 6 Minimal API Sample

Project built with .NET 6 SDK with VS 2022 Empty Project Template using Minimal concept, creating an web api with minimal code:

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Mapping GET method
app.MapGet("/", () => "Hello world");

// Running our app
app.Run();
```

<hr />

### That project uses:

- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.Design
- Swashbuckle.AspNetCore (Swagger)

### Swagger

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add Swagger Gen
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.MapGet("/", () => "Hello world");

// Add Swagger UI
app.UseSwaggerUI();

app.Run();
```

### Entity Framework

```csharp
// Create our Model using new .NET6 feature (records)

public record Person(Guid Id, string FullName, DateTime DateOfBirth);
```

```csharp
// Create our DbContext

public class AppDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("DataSource=app.db;Cache=Shared");
}
```

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject our DbContext
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.UseSwagger();

app.MapGet("/", () => "Hello world");

app.UseSwaggerUI();

app.Run();
```

To add Migration and update database

```powershell
PS C:\ProjectRoot> dotnet ef migrations add InitialMigration

PS C:\ProjectRoot> dotnet ef database update
```
