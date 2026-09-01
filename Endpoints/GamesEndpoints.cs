using GameStore.Api.Data;
using GameStore.Api.Models;
using GameStore.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    private const string getGame = "GetGame";


    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Games
                .Include(game=> game.Genre)
                .Select(game => new GameDto(game.Id, game.Name, game.GenreId,game.Genre!.Name, game.Price, game.ReleaseDate))
                .ToListAsync()
            );



        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.Include(game=>game.Genre).FirstOrDefaultAsync(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            var gameDto = new GameDto(
                Id: game.Id,
                Name: game.Name,
                GenreId: game.GenreId,
                Price: game.Price,
                ReleaseDate: game.ReleaseDate,
                GenreName: game.Genre!.Name
                );
            return Results.Ok(gameDto);
        }).WithName(getGame);

        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate,
            };
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
            var genre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == newGame.GenreId);
            if (genre is null)
            {
                return Results.BadRequest();
            }
            GameDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                genre.Name,
                game.Price,
                game.ReleaseDate
                );
            return Results.CreatedAtRoute(getGame, new { id = gameDto.Id }, gameDto);
        }).WithParameterValidation();

        group.MapPut("/{id}", async (int id, UpdateGameDto updateGame, GameStoreContext dbContext) =>
        {
            var existing = await dbContext.Games.FindAsync(id);
            if (existing is null) return Results.NotFound();
            if (!dbContext.Genres.Any(g => g.Id == updateGame.GenreId))
            {
                return Results.BadRequest();
            }
            existing.Name         = updateGame.Name        ?? existing.Name;
            existing.GenreId      = updateGame.GenreId     ?? existing.GenreId;
            existing.Price        = updateGame.Price       ?? existing.Price;
            existing.ReleaseDate  = updateGame.ReleaseDate ?? existing.ReleaseDate;

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("games/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(g => g.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
    }
}