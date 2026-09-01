using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOs.GameDTOs;

public record CreateGameDto(
    [property: Required] string Name,
    [property: Required] int GenreId,
    decimal Price,
    DateOnly ReleaseDate);