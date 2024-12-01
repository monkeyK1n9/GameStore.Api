using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
    private readonly List<GameSummary> _games =
    [
        new() {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 29.99M,
            ReleaseDate = new DateOnly(1992, 6, 6)
        },
        new () {
            Id = 2,
            Name = "Final Fantasy XVII",
            Genre = "Roleplaying",
            Price = 59.99M,
            ReleaseDate = new DateOnly(2010, 9, 30)
        },
        new () {
            Id = 3,
            Name = "FIFA 20",
            Genre = "Sports",
            Price = 69.99M,
            ReleaseDate = new DateOnly(2019, 9, 27)
        }
    ];

    private readonly Genre[] _genres = new GenresClient().GetGenres();

    public GameSummary[] GetGames() => [.. _games];

    public void AddGame(GameDetails gameDetails)
    {
        Genre genre = GetGenreById(gameDetails.GenreId);

        var gameSummary = new GameSummary
        {
            Id = _games.Count + 1,
            Name = gameDetails.Name,
            Genre = genre.Name,
            Price = gameDetails.Price,
            ReleaseDate = gameDetails.ReleaseDate,
        };

        _games.Add(gameSummary);
    }

    public GameDetails GetGameDetailsById(int id)
    {
        GameSummary? game = GetGameSummaryById(id);
        var genre = _genres.Single
        (
            genre => string.Equals
                                (
                                    genre.Name,
                                    game.Genre,
                                    StringComparison.OrdinalIgnoreCase
                                )
        );

        return new GameDetails
        {
            Id = game.Id,
            Name = game.Name,
            GenreId = genre.Id.ToString(),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public void UpdateGame(GameDetails updatedGame)
    {
        var genre = GetGenreById(updatedGame.GenreId);
        GameSummary existingGame = GetGameSummaryById(updatedGame.Id);

        existingGame.Name = updatedGame.Name;
        existingGame.Genre = genre.Name;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
    }

    public void DeleteGame(int id)
    {
        var game = GetGameSummaryById(id);
        _games.Remove(game);
    }

    private GameSummary GetGameSummaryById(int id)
    {
        var game = _games.Find(game => game.Id == id);

        ArgumentNullException.ThrowIfNull(game);
        return game;
    }

    private Genre GetGenreById(string? id)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

        var genre = _genres.Single(genre => genre.Id == int.Parse(id));
        return genre;
    }
}
