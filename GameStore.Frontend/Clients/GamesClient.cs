using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient(HttpClient httpClient)
{
    public async Task<GameSummary[]> GetGamesAsync() => await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];

    public async Task AddGameAsync(GameDetails gameDetails) => await httpClient.PostAsJsonAsync("games", gameDetails);

    public async Task<GameDetails> GetGameDetailsByIdAsync(int id) => await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}") ??
    throw new Exception($"Game with id {id} not found");

    public async Task UpdateGameAsync(GameDetails updatedGame) => await httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);

    public async Task DeleteGameAsync(int id) => await httpClient.DeleteAsync($"games/{id}");
}
