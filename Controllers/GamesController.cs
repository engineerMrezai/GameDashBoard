using GameStore.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/games")]
public class GamesController : ControllerBase
{
    // Must remain static so the data isn't wiped out when the controller is re-created on each request.
    private static readonly List<GameDto> games =
    [
        new(1, "Street Fighter II", "Fighting", 100.00m, new DateOnly(2001, 1, 1)),
        new(2, "The Legend of Zelda: Breath of the Wild", "Adventure", 59.99m, new DateOnly(2017, 3, 3)),
        new(3, "Minecraft", "Sandbox", 29.99m, new DateOnly(2011, 11, 18)),
        new(4, "The Witcher 3: Wild Hunt", "RPG", 39.99m, new DateOnly(2015, 5, 19)),
        new(5, "Grand Theft Auto V", "Action", 29.99m, new DateOnly(2013, 9, 17)),
        new(6, "Hades", "Action Roguelike", 24.99m, new DateOnly(2020, 9, 17)),
        new(7, "Stardew Valley", "Simulation", 14.99m, new DateOnly(2016, 2, 26)),
        new(8, "Elden Ring", "Action RPG", 59.99m, new DateOnly(2022, 2, 25)),
        new(9, "Portal 2", "Puzzle", 9.99m, new DateOnly(2011, 4, 18)),
        new(10, "Celeste", "Platformer", 19.99m, new DateOnly(2018, 1, 25))
    ];

    // SemaphoreSlim rather than lock: you cannot await inside a lock block,
    // because a monitor is thread-affine and a continuation may resume on another thread.
    private static readonly SemaphoreSlim gate = new(1, 1);

    // GET: api/games
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetAll(CancellationToken cancellationToken)
    {
        await gate.WaitAsync(cancellationToken);
        try
        {
            return Ok(games.ToList());
        }
        finally
        {
            gate.Release();
        }
    }

    // GET: api/games/{id}
    [HttpGet("{id:int}", Name = "GetGame")]
    public async Task<ActionResult<GameDto>> GetById(int id, CancellationToken cancellationToken)
    {
        await gate.WaitAsync(cancellationToken);
        try
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            return game is not null ? Ok(game) : NotFound();
        }
        finally
        {
            gate.Release();
        }
    }

    // POST: api/games
    // [ApiController] returns 400 ValidationProblemDetails automatically
    // when the DataAnnotations on CreateGameDto fail, before this body runs.
    [HttpPost]
    public async Task<ActionResult<GameDto>> Create(
        CreateGameDto newGame,
        CancellationToken cancellationToken)
    {
        GameDto game;

        await gate.WaitAsync(cancellationToken);
        try
        {
            // Max + 1 rather than Count + 1: after a delete, Count + 1 collides with an existing id.
            var nextId = games.Count == 0 ? 1 : games.Max(g => g.Id) + 1;

            game = new GameDto(
                nextId,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);

            games.Add(game);
        }
        finally
        {
            gate.Release();
        }

        return CreatedAtRoute("GetGame", new { id = game.Id }, game);
    }

    // PUT: api/games/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateGameDto updateGame,
        CancellationToken cancellationToken)
    {
        await gate.WaitAsync(cancellationToken);
        try
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1) return NotFound();

            var existing = games[index];

            games[index] = new GameDto(
                id,
                updateGame.Name ?? existing.Name,
                updateGame.Genre ?? existing.Genre,
                updateGame.Price ?? existing.Price,
                updateGame.ReleaseDate ?? existing.ReleaseDate);
        }
        finally
        {
            gate.Release();
        }

        return NoContent();
    }

    // DELETE: api/games/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await gate.WaitAsync(cancellationToken);
        try
        {
            if (games.RemoveAll(game => game.Id == id) == 0) return NotFound();
        }
        finally
        {
            gate.Release();
        }

        return NoContent();
    }
}