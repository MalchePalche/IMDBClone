using IMDBClone.Models;
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
        public DbSet<WatchlistItem> watchlistItems { get; set; }

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

            modelBuilder.Entity<WatchlistItem>().HasKey(w => w.Id);

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
                new Movie
                {
                    Id = 1,
                    Title = "Inception",
                    Description = "A mind-bending thriller",
                    ReleaseYear = 2010,
                    Director = "Christopher Nolan",
                    GenreId = 5,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Leonardo DiCaprio, Joseph Gordon-Levitt, Elliot Page",
                    TrailerUrl = "https://www.youtube.com/watch?v=YoHD9XEInc0"
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Godfather",
                    Description = "A mafia classic",
                    ReleaseYear = 1972,
                    Director = "Francis Ford Coppola",
                    GenreId = 2,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/1/1c/Godfather_ver1.jpg",
                    Actors = "Marlon Brando, Al Pacino, James Caan",
                    TrailerUrl = "https://www.youtube.com/watch?v=sY1S34973zA"
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Dark Knight",
                    Description = "Batman battles Joker in Gotham",
                    ReleaseYear = 2008,
                    Director = "Christopher Nolan",
                    GenreId = 1,
                    PosterUrl = "https://musicart.xboxlive.com/7/abb02f00-0000-0000-0000-000000000002/504/image.jpg",
                    Actors = "Christian Bale, Heath Ledger, Aaron Eckhart",
                    TrailerUrl = "https://www.youtube.com/watch?v=EXeTwQWrcwY"
                },
                new Movie
                {
                    Id = 4,
                    Title = "Forrest Gump",
                    Description = "Life story of a simple man",
                    ReleaseYear = 1994,
                    Director = "Robert Zemeckis",
                    GenreId = 2,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/6/67/Forrest_Gump_poster.jpg",
                    Actors = "Tom Hanks, Robin Wright, Gary Sinise",
                    TrailerUrl = "https://www.youtube.com/watch?v=bLvqoHBptjg"
                },
                new Movie
                {
                    Id = 5,
                    Title = "The Matrix",
                    Description = "Reality is not what it seems",
                    ReleaseYear = 1999,
                    Director = "The Wachowskis",
                    GenreId = 5,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg",
                    Actors = "Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss",
                    TrailerUrl = "https://www.youtube.com/watch?v=vKQi3bBA1y8"
                },
                new Movie
                {
                    Id = 6,
                    Title = "Parasite",
                    Description = "A dark satire of class inequality",
                    ReleaseYear = 2019,
                    Director = "Bong Joon-ho",
                    GenreId = 2,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/5/53/Parasite_%282019_film%29.png",
                    Actors = "Song Kang-ho, Lee Sun-kyun, Jo Yeo-jeong",
                    TrailerUrl = "https://www.youtube.com/watch?v=5xH0HfJHsaY"
                },
                new Movie
                {
                    Id = 7,
                    Title = "Gladiator",
                    Description = "A Roman general seeks revenge",
                    ReleaseYear = 2000,
                    Director = "Ridley Scott",
                    GenreId = 1,
                    PosterUrl = "https://tr.web.img3.acsta.net/pictures/bzp/01/24944.jpg",
                    Actors = "Russell Crowe, Joaquin Phoenix, Connie Nielsen",
                    TrailerUrl = "https://www.youtube.com/watch?v=P5ieIbInFpg"
                },
                new Movie
                {
                    Id = 8,
                    Title = "The Hangover",
                    Description = "A wild bachelor party in Vegas",
                    ReleaseYear = 2009,
                    Director = "Todd Phillips",
                    GenreId = 3,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/b/b9/Hangoverposter09.jpg",
                    Actors = "Bradley Cooper, Ed Helms, Zach Galifianakis",
                    TrailerUrl = "https://www.youtube.com/watch?v=tlize92ffnY"
                },
                new Movie
                {
                    Id = 9,
                    Title = "Interstellar",
                    Description = "A space-time journey to save humanity",
                    ReleaseYear = 2014,
                    Director = "Christopher Nolan",
                    GenreId = 5,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/b/bc/Interstellar_film_poster.jpg",
                    Actors = "Matthew McConaughey, Anne Hathaway, Jessica Chastain",
                    TrailerUrl = "https://www.youtube.com/watch?v=zSWdZVtXT7E"
                },
                new Movie
                {
                    Id = 10,
                    Title = "Titanic",
                    Description = "A romance aboard a doomed ship",
                    ReleaseYear = 1997,
                    Director = "James Cameron",
                    GenreId = 2,
                    PosterUrl = "https://i.ebayimg.com/images/g/94QAAOSwxp9W4HQM/s-l1200.jpg",
                    Actors = "Leonardo DiCaprio, Kate Winslet, Billy Zane",
                    TrailerUrl = "https://www.youtube.com/watch?v=kVrqfYjkTdQ"
                },
                new Movie
                {
                    Id = 11,
                    Title = "The Conjuring",
                    Description = "Paranormal investigators face a haunted house",
                    ReleaseYear = 2013,
                    Director = "James Wan",
                    GenreId = 4,
                    PosterUrl = "https://musicart.xboxlive.com/7/8ac41100-0000-0000-0000-000000000002/504/image.jpg",
                    Actors = "Vera Farmiga, Patrick Wilson, Lili Taylor",
                    TrailerUrl = "https://www.youtube.com/watch?v=k10ETZ41q5o"
                },
                new Movie
                {
                    Id = 12,
                    Title = "Get Out",
                    Description = "A psychological horror with social commentary",
                    ReleaseYear = 2017,
                    Director = "Jordan Peele",
                    GenreId = 4,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/a/a3/Get_Out_poster.png",
                    Actors = "Daniel Kaluuya, Allison Williams, Catherine Keener",
                    TrailerUrl = "https://www.youtube.com/watch?v=DzfpyUB60YY"
                },
                new Movie
                {
                    Id = 13,
                    Title = "The Avengers",
                    Description = "Marvel heroes unite to save the world",
                    ReleaseYear = 2012,
                    Director = "Joss Whedon",
                    GenreId = 1,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNGE0YTVjNzUtNzJjOS00NGNlLTgxMzctZTY4YTE1Y2Y1ZTU4XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Robert Downey Jr., Chris Evans, Mark Ruffalo",
                    TrailerUrl = "https://www.youtube.com/watch?v=eOrNdBpGMv8"
                },
                new Movie
                {
                    Id = 14,
                    Title = "Back to the Future",
                    Description = "Teen travels through time in a DeLorean",
                    ReleaseYear = 1985,
                    Director = "Robert Zemeckis",
                    GenreId = 5,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/d/d2/Back_to_the_Future.jpg",
                    Actors = "Michael J. Fox, Christopher Lloyd, Crispin Glover",
                    TrailerUrl = "https://www.youtube.com/watch?v=qvsgGtivCgs"
                },
                new Movie
                {
                    Id = 15,
                    Title = "Joker",
                    Description = "Origin story of the Joker",
                    ReleaseYear = 2019,
                    Director = "Todd Phillips",
                    GenreId = 2,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/e/e1/Joker_%282019_film%29_poster.jpg",
                    Actors = "Joaquin Phoenix, Robert De Niro, Zazie Beetz",
                    TrailerUrl = "https://www.youtube.com/watch?v=zAGVQLHvwOY"
                },
                new Movie
                {
                    Id = 16,
                    Title = "Shrek",
                    Description = "An ogre goes on a quest",
                    ReleaseYear = 2001,
                    Director = "Andrew Adamson",
                    GenreId = 3,
                    PosterUrl = "https://mediaproxy.tvtropes.org/width/1200/https://static.tvtropes.org/pmwiki/pub/images/shrek_cover.png",
                    Actors = "Mike Myers, Eddie Murphy, Cameron Diaz",
                    TrailerUrl = "https://www.youtube.com/watch?v=CwXOrWvPBPk"
                },
                new Movie
                {
                    Id = 17,
                    Title = "The Silence of the Lambs",
                    Description = "FBI agent consults a cannibal killer",
                    ReleaseYear = 1991,
                    Director = "Jonathan Demme",
                    GenreId = 4,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/8/86/The_Silence_of_the_Lambs_poster.jpg",
                    Actors = "Jodie Foster, Anthony Hopkins, Scott Glenn",
                    TrailerUrl = "https://www.youtube.com/watch?v=6iB21hsprAQ"
                },
                new Movie
                {
                    Id = 18,
                    Title = "Toy Story",
                    Description = "Toys come to life",
                    ReleaseYear = 1995,
                    Director = "John Lasseter",
                    GenreId = 3,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/1/13/Toy_Story.jpg",
                    Actors = "Tom Hanks, Tim Allen, Don Rickles",
                    TrailerUrl = "https://www.youtube.com/watch?v=v-PjgYDrg70"
                },
                new Movie 
                { 
                    Id = 19, 
                    Title = "Blade Runner 2049", 
                    Description = "A replicant uncovers buried secrets", 
                    ReleaseYear = 2017, 
                    Director = "Denis Villeneuve", 
                    GenreId = 5, 
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNzA1Njg4NzYxOV5BMl5BanBnXkFtZTgwODk5NjU3MzI@._V1_.jpg",
                    Actors = "Ryan Gosling, Harrison Ford, Ana de Armas",
                    TrailerUrl = "https://www.youtube.com/watch?v=gCcx85zbxz4"
                },
                new Movie 
                { 
                    Id = 20, 
                    Title = "The Shawshank Redemption", 
                    Description = "A banker is wrongfully imprisoned", 
                    ReleaseYear = 1994, 
                    Director = "Frank Darabont", 
                    GenreId = 2, 
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMDAyY2FhYjctNDc5OS00MDNlLThiMGUtY2UxYWVkNGY2ZjljXkEyXkFqcGc@._V1_.jpg",
                    Actors = "Tim Robbins, Morgan Freeman, Bob Gunton",
                    TrailerUrl = "https://www.youtube.com/watch?v=PLl99DlL6b4"
                },
                new Movie 
                { 
                    Id = 21, 
                    Title = "Mad Max: Fury Road", 
                    Description = "Post-apocalyptic action ride",
                    ReleaseYear = 2015, 
                    Director = "George Miller", 
                    GenreId = 1, 
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BZDRkODJhOTgtOTc1OC00NTgzLTk4NjItNDgxZDY4YjlmNDY2XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Tom Hardy, Charlize Theron, Nicholas Hoult",
                    TrailerUrl = "https://www.youtube.com/watch?v=hEJnMQG9ev8"
                },
                new Movie 
                { 
                    Id = 22, 
                    Title = "The Sixth Sense", 
                    Description = "A boy sees dead people", 
                    ReleaseYear = 1999,
                    Director = "M. Night Shyamalan", 
                    GenreId = 4, 
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BZWQ2OTY0M2UtMTQxNC00MmIzLTllNDQtNDQ0MTQyYzI2M2ZiXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Bruce Willis, Haley Joel Osment, Toni Collette",
                    TrailerUrl = "https://www.youtube.com/watch?v=3-ZP95NF_Wk"
                },
                new Movie
                {
                    Id = 23,
                    Title = "John Wick 3",
                    Description = "John Wick is excommunicated and hunted by assassins.",
                    ReleaseYear = 2019,
                    Director = "David Fincher",
                    GenreId = 1,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BYjdlNWFlZjEtM2U0NS00ZWU5LTk1M2EtZmQxNWFiZjk0MGM5XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Keanu Reeves, Halle Berry, Ian McShane",
                    TrailerUrl = "https://www.youtube.com/watch?v=M7XM597XO94"
                },
                new Movie
                {
                    Id = 24,
                    Title = "Star Wars: A New Hope",
                    Description = "Luke Skywalker joins a rebellion to fight the Empire.",
                    ReleaseYear = 1977,
                    Director = "George Lucas",
                    GenreId = 5,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/8/87/StarWarsMoviePoster1977.jpg",
                    Actors = "Mark Hamill, Harrison Ford, Carrie Fisher",
                    TrailerUrl = "https://www.youtube.com/watch?v=vZ734NWnAHA"
                },
                new Movie
                {
                    Id = 25,
                    Title = "Avengers Endgame",
                    Description = "Avengers try to reverse Thanos' snap in Endgame.",
                    ReleaseYear = 2019,
                    Director = "Anthony Russo, Joe Russo",
                    GenreId = 1,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/0/0d/Avengers_Endgame_poster.jpg",
                    Actors = "Robert Downey Jr., Chris Evans, Mark Ruffalo",
                    TrailerUrl = "https://www.youtube.com/watch?v=TcMBFSGVi1c"
                },
                new Movie
                {
                    Id = 26,
                    Title = "Fight Club",
                    Description = "An insomniac and a soap maker form a secret fight club.",
                    ReleaseYear = 1999,
                    Director = "David Fincher",
                    GenreId = 1,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/f/fc/Fight_Club_poster.jpg",
                    Actors = "Brad Pitt, Edward Norton, Helena Bonham Carter",
                    TrailerUrl = "https://www.youtube.com/watch?v=qtRKdVHc-cE"
                }
            );
        }
    }
}
