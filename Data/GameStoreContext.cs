using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Fighting" },
            new Genre { Id = 2, Name = "Roleplaying" },
            new Genre { Id = 3, Name = "Platformer" },
            new Genre { Id = 4, Name = "Racing" },
            new Genre { Id = 5, Name = "Sports" },
            new Genre { Id = 6, Name = "Adventure" },
            new Genre { Id = 7, Name = "Sandbox" },
            new Genre { Id = 8, Name = "Action" },
            new Genre { Id = 9, Name = "Action Roguelike" },
            new Genre { Id = 10, Name = "Simulation" },
            new Genre { Id = 11, Name = "Action RPG" },
            new Genre { Id = 12, Name = "Puzzle" });
        modelBuilder.Entity<Game>().HasData(
            new Game
            {
                Id = 1, Name = "Street Fighter II", GenreId = 1, Price = 100.00m, ReleaseDate = new DateOnly(2001, 1, 1)
            },
            new Game
            {
                Id = 2, Name = "The Legend of Zelda: Breath of the Wild", GenreId = 6, Price = 59.99m,
                ReleaseDate = new DateOnly(2017, 3, 3)
            },
            new Game
            {
                Id = 3, Name = "Minecraft", GenreId = 7, Price = 29.99m, ReleaseDate = new DateOnly(2011, 11, 18)
            },
            new Game
            {
                Id = 4, Name = "The Witcher 3: Wild Hunt", GenreId = 2, Price = 39.99m,
                ReleaseDate = new DateOnly(2015, 5, 19)
            },
            new Game
            {
                Id = 5, Name = "Grand Theft Auto V", GenreId = 8, Price = 29.99m,
                ReleaseDate = new DateOnly(2013, 9, 17)
            },
            new Game
            {
                Id = 6, Name = "Hades", GenreId = 9, Price = 24.99m, ReleaseDate = new DateOnly(2020, 9, 17)
            },
            new Game
            {
                Id = 7, Name = "Stardew Valley", GenreId = 10, Price = 14.99m, ReleaseDate = new DateOnly(2016, 2, 26)
            },
            new Game
            {
                Id = 8, Name = "Elden Ring", GenreId = 11, Price = 59.99m, ReleaseDate = new DateOnly(2022, 2, 25)
            },
            new Game
            {
                Id = 9, Name = "Portal 2", GenreId = 12, Price = 9.99m, ReleaseDate = new DateOnly(2011, 4, 18)
            },
            new Game
            {
                Id = 10, Name = "Celeste", GenreId = 3, Price = 19.99m, ReleaseDate = new DateOnly(2018, 1, 25)
            }
        );
    }
}