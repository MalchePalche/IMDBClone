﻿using IMDBClone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IMDBClone.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<FavoriteMovie> FavoriteMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensures Identity tables are created

            // Seed Roles FIRST
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );

            // ❌ Removed admin user from `HasData()`, moved to `Program.cs`

            // Seed Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Drama" },
                new Genre { Id = 3, Name = "Comedy" },
                new Genre { Id = 4, Name = "Horror" },
                new Genre { Id = 5, Name = "Sci-Fi" }
            );

            // Seed Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Inception", Description = "A mind-bending thriller", ReleaseYear = 2010, Director = "Christopher Nolan", GenreId = 5 },
                new Movie { Id = 2, Title = "The Godfather", Description = "A mafia classic", ReleaseYear = 1972, Director = "Francis Ford Coppola", GenreId = 2 }
            );
        }
    }
}
