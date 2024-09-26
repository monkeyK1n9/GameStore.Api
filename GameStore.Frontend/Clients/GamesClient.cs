using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
    private readonly List<GameSummary> games =
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

    public GameSummary[] GetGames() => [.. games];
}
