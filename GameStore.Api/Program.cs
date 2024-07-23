using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Endpoints.GamesEndpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStore");

// we register the services here
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

// with extension methods
app.MapGamesEndpoints();
app.MapGenresEndpoints();

// run immediate migrations before the app runs
await app.MigrateDbAsync();


app.Run();
