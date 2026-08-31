using GameStore.Api.Dto;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    private static readonly List<GameDto> games =
    [
        new(1,
            "Street Fighter II",
            "Fighting",
            100.00m,
            DateOnly.FromDateTime(new DateTime(2001, 1, 1))),
        new(2,
            "The Legend of Zelda: Breath of the Wild",
            "Adventure",
            59.99m,
            DateOnly.FromDateTime(new DateTime(2017, 3, 3))),
        new(3,
            "Minecraft",
            "Sandbox",
            29.99m,
            DateOnly.FromDateTime(new DateTime(2011, 11, 18))),
        new(4,
            "The Witcher 3: Wild Hunt",
            "RPG",
            39.99m,
            DateOnly.FromDateTime(new DateTime(2015, 5, 19))),
        new(5,
            "Grand Theft Auto V",
            "Action",
            29.99m,
            DateOnly.FromDateTime(new DateTime(2013, 9, 17))),
        new(6,
            "Hades",
            "Action Roguelike",
            24.99m,
            DateOnly.FromDateTime(new DateTime(2020, 9, 17))),
        new(7,
            "Stardew Valley",
            "Simulation",
            14.99m,
            DateOnly.FromDateTime(new DateTime(2016, 2, 26))),
        new(8,
            "Elden Ring",
            "Action RPG",
            59.99m,
            DateOnly.FromDateTime(new DateTime(2022, 2, 25))),
        new(9,
            "Portal 2",
            "Puzzle",
            9.99m,
            DateOnly.FromDateTime(new DateTime(2011, 4, 18))),
        new(10,
            "Celeste",
            "Platformer",
            19.99m,
            DateOnly.FromDateTime(new DateTime(2018, 1, 25)))
    ];


    private const string getGame = "GetGame";


    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", () => games);


        group.MapGet("/{id}", (int id) =>
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        }).WithName(getGame);

        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);
            games.Add(game);
            return Results.CreatedAtRoute(getGame, new { id = game.Id }, game);
        }).WithParameterValidation();

        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1) return Results.NotFound();
            games[index] = new GameDto(
                id,
                updateGame.Name ?? games[index].Name,
                updateGame.Genre ?? games[index].Genre,
                updateGame.Price ?? games[index].Price,
                updateGame.ReleaseDate ?? games[index].ReleaseDate
            );

            return Results.NoContent();
        });

        group.MapDelete("games/{id}", (int id) => games.RemoveAll(game => game.Id == id));
    }
}