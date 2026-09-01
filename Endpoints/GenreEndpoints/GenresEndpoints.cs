using GameStore.Api.Data;
using GameStore.Api.DTOs.GenreDTOs;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints.GenreEndpoints;

public static class GenresEndpoints
{
        
    private const string GetGenre = "GetGenre";    
    
    public static void MapGenresEndpoints(this WebApplication app)
    {

        
        var group = app.MapGroup("/genres");

        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Genres
                .Select(genre => new GenreDto(genre.Id, genre.Name))
                .ToListAsync()
        );

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var genre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
            return genre is null ? Results.NotFound() : Results.Ok(new GenreDto(genre.Id, genre.Name));
        }).WithName(GetGenre);

        group.MapPost("/", async (CreateGenreDto newGenre, GameStoreContext dbContext) =>
        {
            Genre genre = new()
            {
                Name = newGenre.Name
            };
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetGenre,new {id=genre.Id}, new GenreDto(genre.Id,genre.Name));
        });

        group.MapPut("/{id}", async (int id, UpdateGenre updateGenre, GameStoreContext dbContext) =>
        {
            var existing = await dbContext.Genres.FindAsync(id);
            if (existing is null) return Results.NotFound();
            existing.Name = updateGenre.Name ?? existing.Name;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                await dbContext.Genres.Where(g => g.Id == id).ExecuteDeleteAsync();
                return Results.NoContent();
            }
        );
    }
}