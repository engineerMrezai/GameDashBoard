namespace GameStore.Api.DTOs;

public record UpdateGameDto(
    string? Name,
    int? GenreId,
    decimal? Price,
    DateOnly? ReleaseDate
);