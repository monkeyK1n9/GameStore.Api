using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints.GamesEndpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetName";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        // we can use grouping to replace the repeating "games" route in each Method
        var group = app.MapGroup("games")
                        .WithParameterValidation(); // to apply validation with the nuget package MinimalApis.Extensions

        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.Games
            .Include(item => item.Genre) // this queries from the Genre table to include it into the final result in Select extension method, since Genre is part of the GameSummaryDto object
            .Select(item => item.ToGameSummaryDto())
            .AsNoTracking() // removes tracking, and improves performance
            .ToListAsync());

        // GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            // we use extension method to from Dto to Game object
            Game game = newGame.ToEntity();

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            // we map back to Game from Dto with extension method to return to client
            GameDetailsDto returnGame = game.ToGameDetailsDto();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, returnGame);
        });

        // PUT /games/1
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is not null)
            {
                dbContext.Entry(existingGame)
                         .CurrentValues
                         .SetValues(updatedGame.ToEntity(id));

                await dbContext.SaveChangesAsync();
            }

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                    .Where(game => game.Id == id)
                    .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
