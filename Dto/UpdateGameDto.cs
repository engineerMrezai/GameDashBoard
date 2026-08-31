namespace GameStore.Api.Dto;

public record UpdateGameDto(
    string? Name,
    string? Genre,
    decimal? Price,
    DateOnly? ReleaseDate
);