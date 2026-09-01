using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOs;

public record CreateGameDto(
    [property: Required] string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);