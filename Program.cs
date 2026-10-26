using BirdJournal.Data;
using BirdJournal.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("AzureSqlDatabase");
builder.Services.AddDbContext<BirdJournalContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapJournalEntryEndpoints();

app.MigrateDb();

app.Run();
