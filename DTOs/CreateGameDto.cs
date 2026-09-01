using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOs;

public record CreateGameDto(
    [property: Required] string Name,
    [property: Required] int GenreId,
    decimal Price,
    DateOnly ReleaseDate);