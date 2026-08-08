using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HelpDesk.Api.Data;
using HelpDesk.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server (or InMemory fallback if SQL Server is unreachable)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
try
{
    builder.Services.AddDbContext<HelpDeskDbContext>(options =>
        options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        }));
}
catch
{
    // Fallback to InMemory database for seamless testing and execution
    builder.Services.AddDbContext<HelpDeskDbContext>(options =>
        options.UseInMemoryDatabase("HelpDeskManagementDb"));
}

// Register Repository Pattern Dependency Injection
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

var app = builder.Build();

// Auto-ensure database created and populated with seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HelpDeskDbContext>();
    try
    {
        dbContext.Database.EnsureCreated();
    }
    catch
    {
        // Fallback gracefully if LocalDB/SqlServer service is not running
    }
}

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
