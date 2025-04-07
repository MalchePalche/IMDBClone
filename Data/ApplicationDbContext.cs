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

            // ✅ Fix Identity token key lengths (prevents migration error)
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(m => m.LoginProvider).HasMaxLength(450);
                entity.Property(m => m.ProviderKey).HasMaxLength(450);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(450);
                entity.Property(m => m.RoleId).HasMaxLength(450);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(450);
                entity.Property(m => m.LoginProvider).HasMaxLength(450);
                entity.Property(m => m.Name).HasMaxLength(450);
            });

            // Seed Roles FIRST
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );

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
                new Movie { Id = 2, Title = "The Godfather", Description = "A mafia classic", ReleaseYear = 1972, Director = "Francis Ford Coppola", GenreId = 2 },
                new Movie { Id = 3, Title = "The Dark Knight", Description = "Batman battles Joker in Gotham", ReleaseYear = 2008, Director = "Christopher Nolan", GenreId = 1 },
                new Movie { Id = 4, Title = "Forrest Gump", Description = "Life story of a simple man", ReleaseYear = 1994, Director = "Robert Zemeckis", GenreId = 2 },
                new Movie { Id = 5, Title = "The Matrix", Description = "Reality is not what it seems", ReleaseYear = 1999, Director = "The Wachowskis", GenreId = 5 },
                new Movie { Id = 6, Title = "Parasite", Description = "A dark satire of class inequality", ReleaseYear = 2019, Director = "Bong Joon-ho", GenreId = 2 },
                new Movie { Id = 7, Title = "Gladiator", Description = "A Roman general seeks revenge", ReleaseYear = 2000, Director = "Ridley Scott", GenreId = 1 },
                new Movie { Id = 8, Title = "The Hangover", Description = "A wild bachelor party in Vegas", ReleaseYear = 2009, Director = "Todd Phillips", GenreId = 3 },
                new Movie { Id = 9, Title = "Interstellar", Description = "A space-time journey to save humanity", ReleaseYear = 2014, Director = "Christopher Nolan", GenreId = 5 },
                new Movie { Id = 10, Title = "Titanic", Description = "A romance aboard a doomed ship", ReleaseYear = 1997, Director = "James Cameron", GenreId = 2 },
                new Movie { Id = 11, Title = "The Conjuring", Description = "Paranormal investigators face a haunted house", ReleaseYear = 2013, Director = "James Wan", GenreId = 4 },
                new Movie { Id = 12, Title = "Get Out", Description = "A psychological horror with social commentary", ReleaseYear = 2017, Director = "Jordan Peele", GenreId = 4 },
                new Movie { Id = 13, Title = "The Avengers", Description = "Marvel heroes unite to save the world", ReleaseYear = 2012, Director = "Joss Whedon", GenreId = 1 },
                new Movie { Id = 14, Title = "Back to the Future", Description = "Teen travels through time in a DeLorean", ReleaseYear = 1985, Director = "Robert Zemeckis", GenreId = 5 },
                new Movie { Id = 15, Title = "Joker", Description = "Origin story of the Joker", ReleaseYear = 2019, Director = "Todd Phillips", GenreId = 2 },
                new Movie { Id = 16, Title = "Shrek", Description = "An ogre goes on a quest", ReleaseYear = 2001, Director = "Andrew Adamson", GenreId = 3 },
                new Movie { Id = 17, Title = "The Silence of the Lambs", Description = "FBI agent consults a cannibal killer", ReleaseYear = 1991, Director = "Jonathan Demme", GenreId = 4 },
                new Movie { Id = 18, Title = "Toy Story", Description = "Toys come to life", ReleaseYear = 1995, Director = "John Lasseter", GenreId = 3 },
                new Movie { Id = 19, Title = "Blade Runner 2049", Description = "A replicant uncovers buried secrets", ReleaseYear = 2017, Director = "Denis Villeneuve", GenreId = 5 },
                new Movie { Id = 20, Title = "The Shawshank Redemption", Description = "A banker is wrongfully imprisoned", ReleaseYear = 1994, Director = "Frank Darabont", GenreId = 2 },
                new Movie { Id = 21, Title = "Mad Max: Fury Road", Description = "Post-apocalyptic action ride", ReleaseYear = 2015, Director = "George Miller", GenreId = 1 },
                new Movie { Id = 22, Title = "The Sixth Sense", Description = "A boy sees dead people", ReleaseYear = 1999, Director = "M. Night Shyamalan", GenreId = 4 }


            );
        }
    }
}
