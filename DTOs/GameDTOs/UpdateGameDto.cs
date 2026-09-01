namespace GameStore.Api.DTOs.GameDTOs;

public record UpdateGameDto(
    string? Name,
    int? GenreId,
    decimal? Price,
    DateOnly? ReleaseDate
);