namespace GameStore.Api.DTOs.GameDTOs;

public record GameDto(
    int Id,
    string Name,
    int GenreId,
    string GenreName,
    decimal Price,
    DateOnly ReleaseDate);