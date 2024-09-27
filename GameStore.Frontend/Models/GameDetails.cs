using System;

namespace GameStore.Frontend.Models;

public class GameDetails
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string GenreId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
