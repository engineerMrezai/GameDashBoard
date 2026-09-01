using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOs.GenreDTOs;

public record CreateGenreDto
{
    [property: Required] public required string Name { get; set; }
}