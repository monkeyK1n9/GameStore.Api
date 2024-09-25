using System;

namespace GameStore.Frontend.Models;

public class GameSummary
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
