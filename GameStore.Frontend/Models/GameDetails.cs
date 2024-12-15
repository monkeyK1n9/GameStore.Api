using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GameStore.Frontend.Converters;

namespace GameStore.Frontend.Models;

public class GameDetails
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Genre field is required.")]
    [JsonConverter(typeof(StringConverter))] // this will convert the string back to int and reverse. See the StringConverter class we created as converter
    public string GenreId { get; set; } = string.Empty;

    [Range(1, 100)]
    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
