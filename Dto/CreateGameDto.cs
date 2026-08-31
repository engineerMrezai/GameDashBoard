using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dto;

public record CreateGameDto(
    [property: Required] string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);