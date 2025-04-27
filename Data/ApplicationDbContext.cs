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
                },
                new Movie
                {
                    Id = 27,
                    Title = "Whiplash",
                    Description = "A young drummer enrolls in a cut-throat music conservatory where his dreams of greatness are mentored by an abusive instructor.",
                    ReleaseYear = 2014,
                    Director = "Damien Chazelle",
                    GenreId = 2,
                    PosterUrl = "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p10488558_p_v12_ai.jpg",
                    Actors = "Miles Teller, J.K. Simmons, Paul Reiser",
                    TrailerUrl = "https://www.youtube.com/watch?v=7d_jQycdQGo"
                },
                new Movie
                {
                    Id = 28,
                    Title = "The Prestige",
                    Description = "Two stage magicians engage in a bitter rivalry to create the ultimate illusion.",
                    ReleaseYear = 2006,
                    Director = "Christopher Nolan",
                    GenreId = 5,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjA4NDI0MTIxNF5BMl5BanBnXkFtZTYwNTM0MzY2._V1_FMjpg_UX1000_.jpg",
                    Actors = "Christian Bale, Hugh Jackman, Scarlett Johansson",
                    TrailerUrl = "https://www.youtube.com/watch?v=RLtaA9fFNXU"
                },
                new Movie
                {
                    Id = 29,
                    Title = "Django Unchained",
                    Description = "A freed slave sets out to rescue his wife from a brutal Mississippi plantation owner.",
                    ReleaseYear = 2012,
                    Director = "Quentin Tarantino",
                    GenreId = 1,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjIyNTQ5NjQ1OV5BMl5BanBnXkFtZTcwODg1MDU4OA@@._V1_.jpg",
                    Actors = "Jamie Foxx, Christoph Waltz, Leonardo DiCaprio",
                    TrailerUrl = "https://www.youtube.com/watch?v=0fUCuvNlOCg"
                },
                new Movie
                {
                    Id = 30,
                    Title = "The Departed ",
                    Description = "An undercover cop and a mole in the police try to identify each other while infiltrating an Irish gang.",
                    ReleaseYear = 2006,
                    Director = "Martin Scorsese",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTI1MTY2OTIxNV5BMl5BanBnXkFtZTYwNjQ4NjY3._V1_FMjpg_UX1000_.jpg",
                    Actors = "Leonardo DiCaprio, Matt Damon, Jack Nicholson",
                    TrailerUrl = "https://www.youtube.com/watch?v=r-MiSNsCdQ4"
                },
                new Movie
                {
                    Id = 31,
                    Title = "The Pianist ",
                    Description = "A Polish Jewish musician struggles to survive the destruction of the Warsaw ghetto during World War II.",
                    ReleaseYear = 2002,
                    Director = "Roman Polanski",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjEwNmEwYjgtNTk3ZC00NjljLTg5ZDctZTY3ZGQwZjRkZmQxXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Adrien Brody, Thomas Kretschmann, Frank Finlay",
                    TrailerUrl = "https://www.youtube.com/watch?v=BFwGqLa_oAo"
                },
                new Movie
                {
                    Id = 32,
                    Title = "The Green Mile",
                    Description = "A death row corrections officer forms a bond with a mysterious inmate.",
                    ReleaseYear = 1999,
                    Director = "Frank Darabont",
                    GenreId = 2,
                    PosterUrl = "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24429_p_v12_bf.jpg",
                    Actors = "Tom Hanks, Michael Clarke Duncan, David Morse",
                    TrailerUrl = "https://www.youtube.com/watch?v=Ki4haFrqSrw"
                },
                new Movie
                {
                    Id = 33,
                    Title = "The Wolf of Wall Street ",
                    Description = "Based on the true story of Jordan Belfort, from his rise to a wealthy stockbroker to his fall involving crime and corruption.",
                    ReleaseYear = 2013,
                    Director = "Martin Scorsese",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjIxMjgxNTk0MF5BMl5BanBnXkFtZTgwNjIyOTg2MDE@._V1_.jpg",
                    Actors = "Leonardo DiCaprio, Jonah Hill, Margot Robbie",
                    TrailerUrl = "https://www.youtube.com/watch?v=iszwuX1AK6A"
                },
                new Movie
                {
                    Id = 34,
                    Title = "Shutter Island",
                    Description = "A U.S. Marshal investigates the disappearance of a patient from a mental hospital.",
                    ReleaseYear = 2010,
                    Director = "Martin Scorsese",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BN2FjNWExYzEtY2YzOC00YjNlLTllMTQtNmIwM2Q1YzBhOWM1XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Leonardo DiCaprio, Mark Ruffalo, Ben Kingsley",
                    TrailerUrl = "https://www.youtube.com/watch?v=v8yrZSkKxTA"
                },
                new Movie
                {
                    Id = 35,
                    Title = "The Lion King (1994)",
                    Description = "Lion prince Simba flees his kingdom after the death of his father, only to learn the true meaning of responsibility and bravery.",
                    ReleaseYear = 1994,
                    Director = "Roger Allers, Rob Minkoff",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BZGRiZDZhZjItM2M3ZC00Y2IyLTk3Y2MtMWY5YjliNDFkZTJlXkEyXkFqcGc@._V1_.jpg",
                    Actors = "Matthew Broderick, Jeremy Irons, James Earl Jones",
                    TrailerUrl = "https://www.youtube.com/watch?v=lFzVJEksoDY"
                },
                new Movie
                {
                    Id = 36,
                    Title = "The Truman Show ",
                    Description = "An insurance salesman discovers his whole life is actually a reality TV show.",
                    ReleaseYear = 1998,
                    Director = "Peter Weir",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNzA3ZjZlNzYtMTdjMy00NjMzLTk5ZGYtMTkyYzNiOGM1YmM3XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Jim Carrey, Ed Harris, Laura Linney",
                    TrailerUrl = "https://www.youtube.com/watch?v=dlnmQbPGuls"
                },
                new Movie
                {
                    Id = 37,
                    Title = "Once Upon a Time in Hollywood",
                    Description = "A faded television actor and his stunt double strive to achieve fame during Hollywood’s Golden Age.",
                    ReleaseYear = 2019,
                    Director = "Quentin Tarantino",
                    GenreId = 2,
                    PosterUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTij6IZWfKRuCG4tUJ-yMbqOy25SC0dlFYVTQ&s",
                    Actors = "Leonardo DiCaprio, Brad Pitt, Margot Robbie",
                    TrailerUrl = "https://www.youtube.com/watch?v=ELeMaP8EPAA"
                },
                new Movie
                {
                    Id = 38,
                    Title = "Ford Vs Ferrari ",
                    Description = "American car designer Carroll Shelby and driver Ken Miles battle corporate interference to build a revolutionary race car.",
                    ReleaseYear = 2019,
                    Director = "James Mangold",
                    GenreId = 1,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BOTBjNTEyNjYtYjdkNi00YzE5LTljYzUtZjVlYmYwZmJmZWYxXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Matt Damon, Christian Bale, Jon Bernthal",
                    TrailerUrl = "https://www.youtube.com/watch?v=zyYgDtY2AMY"
                },
                new Movie
                {
                    Id = 39,
                    Title = "Coco",
                    Description = "Aspiring musician Miguel enters the Land of the Dead to seek his great-great-grandfather.",
                    ReleaseYear = 2017,
                    Director = "Lee Unkrich, Adrian Molina",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMDIyM2E2NTAtMzlhNy00ZGUxLWI1NjgtZDY5MzhiMDc5NGU3XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Anthony Gonzalez, Gael García Bernal, Benjamin Bratt",
                    TrailerUrl = "https://www.youtube.com/watch?v=xlnPHQ3TLX8"
                },
                new Movie
                {
                    Id = 40,
                    Title = "Up",
                    Description = "An old man ties thousands of balloons to his house and flies to South America, inadvertently taking a young boy with him.",
                    ReleaseYear = 2009,
                    Director = "Pete Docter, Bob Peterson",
                    GenreId = 3,
                    PosterUrl = "https://m.media-amazon.com/images/I/81JOqsD2NmL._AC_UF894,1000_QL80_.jpg",
                    Actors = "Edward Asner, Jordan Nagai, John Ratzenberger",
                    TrailerUrl = "https://www.youtube.com/watch?v=HWEW_qTLSEE"
                },
                new Movie
                {
                    Id = 41,
                    Title = "Ratatouille",
                    Description = "A rat with a passion for cooking makes an unusual alliance with a young kitchen worker.",
                    ReleaseYear = 2007,
                    Director = "Brad Bird, Jan Pinkava",
                    GenreId = 3,
                    PosterUrl = "https://resizing.flixster.com/ySiX7RlyKRuuxCcAI7SgdkMAZ0U=/ems.cHJkLWVtcy1hc3NldHMvbW92aWVzLzc4ZmJhZjZiLTEzNWMtNDIwOC1hYzU1LTgwZjE3ZjQzNTdiNy5qcGc=",
                    Actors = "Patton Oswalt, Ian Holm, Lou Romano",
                    TrailerUrl = "https://www.youtube.com/watch?v=NgsQ8mVkN8w"
                },
                new Movie
                {
                    Id = 42,
                    Title = "Spirited Away",
                    Description = "A 10-year-old girl must save her parents in a world of spirits and gods.",
                    ReleaseYear = 2001,
                    Director = "Hayao Miyazaki",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNTEyNmEwOWUtYzkyOC00ZTQ4LTllZmUtMjk0Y2YwOGUzYjRiXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Rumi Hiiragi, Miyu Irino, Mari Natsuki",
                    TrailerUrl = "https://www.youtube.com/watch?v=ByXuk9QqQkk"
                },
                new Movie
                {
                    Id = 43,
                    Title = "1917",
                    Description = "Two British soldiers are tasked with delivering a critical message to save 1,600 men.",
                    ReleaseYear = 2019,
                    Director = "Sam Mendes",
                    GenreId = 1,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BYzkxZjg2NDQtMGVjMy00NWZkLTk0ZDEtZWE3NDYwYjAyMTg1XkEyXkFqcGc@._V1_.jpg",
                    Actors = "George MacKay, Dean-Charles Chapman, Mark Strong",
                    TrailerUrl = "https://www.youtube.com/watch?v=YqNYrYUiMfg"
                },
                new Movie
                {
                    Id = 44,
                    Title = "Hacksaw Ridge ",
                    Description = "The true story of a conscientious objector who won the Medal of Honor during WWII.",
                    ReleaseYear = 2016,
                    Director = "Mel Gibson",
                    GenreId = 1,
                    PosterUrl = "https://m.media-amazon.com/images/I/919jM7o2YXL._AC_UF1000,1000_QL80_.jpg",
                    Actors = "Andrew Garfield, Sam Worthington, Luke Bracey",
                    TrailerUrl = "https://www.youtube.com/watch?v=s2-1hz1juBI"
                },
                new Movie
                {
                    Id = 45,
                    Title = "Prisoners",
                    Description = "A father takes matters into his own hands after his daughter and her friend go missing.",
                    ReleaseYear = 2013,
                    Director = "Denis Villeneuve",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTg0NTIzMjQ1NV5BMl5BanBnXkFtZTcwNDc3MzM5OQ@@._V1_.jpg",
                    Actors = "Hugh Jackman, Jake Gyllenhaal, Viola Davis",
                    TrailerUrl = "https://www.youtube.com/watch?v=bpXfcTF6iVk"
                },
                new Movie
                {
                    Id = 46,
                    Title = "Sicario",
                    Description = "An idealistic FBI agent is enlisted in a government task force to aid the escalating war against drugs.",
                    ReleaseYear = 2015,
                    Director = "Denis Villeneuve",
                    GenreId = 1,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjA5NjM3NTk1M15BMl5BanBnXkFtZTgwMzg1MzU2NjE@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Emily Blunt, Josh Brolin, Benicio Del Toro",
                    TrailerUrl = "https://www.youtube.com/watch?v=G8tlEcnrGnU"
                },
                new Movie
                {
                    Id = 47,
                    Title = "No Country for Old Men",
                    Description = "Violence and mayhem ensue after a man finds a stash of money left from a drug deal gone wrong.",
                    ReleaseYear = 2007,
                    Director = "2007",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjA5Njk3MjM4OV5BMl5BanBnXkFtZTcwMTc5MTE1MQ@@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Tommy Lee Jones, Javier Bardem, Josh Brolin",
                    TrailerUrl = "https://www.youtube.com/watch?v=38A__WT3-o0"
                },
                new Movie
                {
                    Id = 48,
                    Title = "There WIll Be Blood",
                    Description = "A ruthless oil prospector pursues wealth during Southern California’s oil boom of the late 19th century.",
                    ReleaseYear = 2007,
                    Director = "Paul Thomas Anderson",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjAxODQ4MDU5NV5BMl5BanBnXkFtZTcwMDU4MjU1MQ@@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Daniel Day-Lewis, Paul Dano, Kevin J. O'Connor",
                    TrailerUrl = "https://www.youtube.com/watch?v=FeSLPELpMeM"
                },
                new Movie
                {
                    Id = 49,
                    Title = "Memories of Murder",
                    Description = "Based on the true story of South Korea’s first serial murders in history.",
                    ReleaseYear = 2003,
                    Director = "Bong Joon-ho",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BYmRjOWE5NmMtYTdkYS00ODFlLWJiMTMtYzE2NDZlZjlkZDQ0XkEyXkFqcGc@._V1_.jpg",
                    Actors = "Song Kang-ho, Kim Sang-kyung, Roe-ha Kim",
                    TrailerUrl = "https://www.youtube.com/watch?v=0n_HQwQU8ls"
                },
                new Movie
                {
                    Id = 50,
                    Title = "The Handmaiden",
                    Description = "A con woman is hired as a handmaiden to a Japanese heiress, but they fall in love.",
                    ReleaseYear = 2016,
                    Director = "Park Chan-wook",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BOTllZDI1OGItOGUxNS00OGZhLWIzMjAtYzllZTY1YTA0ZmYwXkEyXkFqcGc@._V1_.jpg",
                    Actors = "Kim Min-hee, Ha Jung-woo, Cho Jin-woong",
                    TrailerUrl = "https://www.youtube.com/watch?v=whldChqCsYk"
                },
                new Movie
                {
                    Id = 51,
                    Title = "La La Land ",
                    Description = "A jazz musician and an aspiring actress fall in love while pursuing their dreams.",
                    ReleaseYear = 2016,
                    Director = "Damien Chazelle",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMzUzNDM2NzM2MV5BMl5BanBnXkFtZTgwNTM3NTg4OTE@._V1_.jpg",
                    Actors = "Ryan Gosling, Emma Stone, John Legend",
                    TrailerUrl = "https://www.youtube.com/watch?v=0pdqf4P9MB8"
                },
                new Movie
                {
                    Id = 52,
                    Title = "Drive (2011)",
                    Description = "A mysterious Hollywood stuntman gets caught up in the world of gangsters after a heist gone wrong.",
                    ReleaseYear = 2011,
                    Director = "Nicolas Winding Refn",
                    GenreId = 1,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BYTFmNTFlOTAtNzEyNi00MWU2LTg3MGEtYjA2NWY3MDliNjlkXkEyXkFqcGc@._V1_.jpg",
                    Actors = "Ryan Gosling, Carey Mulligan, Bryan Cranston",
                    TrailerUrl = "https://www.youtube.com/watch?v=KBiOF3y1W0Y"
                },
                new Movie
                {
                    Id = 53,
                    Title = "Ex Machina",
                    Description = "A young programmer is selected to participate in a groundbreaking experiment in synthetic intelligence.",
                    ReleaseYear = 2014,
                    Director = "Alex Garland",
                    GenreId = 5,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTUxNzc0OTIxMV5BMl5BanBnXkFtZTgwNDI3NzU2NDE@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Alicia Vikander, Domhnall Gleeson, Oscar Isaac",
                    TrailerUrl = "https://www.youtube.com/watch?v=EoQuVnKhxaM"
                },
                new Movie
                {
                    Id = 54,
                    Title = "Jojo Rabbit",
                    Description = "A boy whose world view is shaped by Nazi propaganda finds his mother hiding a Jewish girl in their home.",
                    ReleaseYear = 2019,
                    Director = "Taika Waititi",
                    GenreId = 3,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BYmFmNmUyMjYtZTFjNS00OWQyLThhZmMtMGZkYTQ3YjY0ZDQ1XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    Actors = "Roman Griffin Davis, Thomasin McKenzie, Scarlett Johansson",
                    TrailerUrl = "https://www.youtube.com/watch?v=tL4McUzXfFI"
                },
                new Movie
                {
                    Id = 55,
                    Title = "Birdman",
                    Description = "A faded actor known for portraying a superhero struggles to mount a Broadway play.",
                    ReleaseYear = 2014,
                    Director = "Alejandro González Iñárritu",
                    GenreId = 3,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BODAzNDMxMzAxOV5BMl5BanBnXkFtZTgwMDMxMjA4MjE@._V1_.jpg",
                    Actors = "Michael Keaton, Emma Stone, Edward Norton",
                    TrailerUrl = "https://www.youtube.com/watch?v=uJfLoE6hanc"
                },
                new Movie
                {
                    Id = 56,
                    Title = "The Social Network",
                    Description = "The story of the founding of Facebook and the resulting lawsuits.",
                    ReleaseYear = 2010,
                    Director = "David Fincher",
                    GenreId = 2,
                    PosterUrl = "https://m.media-amazon.com/images/S/pv-target-images/ea4f1c75ddd9fd937a77875d48f9ce8225ed954afcefabe7a2195311b1a97ddd.jpg",
                    Actors = "Jesse Eisenberg, Andrew Garfield, Justin Timberlake",
                    TrailerUrl = "https://www.youtube.com/watch?v=lB95KLmpLR4"
                },
                new Movie
                {
                    Id = 57,
                    Title = "The Big Short ",
                    Description = "In the mid-2000s, a group of investors bet against the US mortgage market.",
                    ReleaseYear = 2015,
                    Director = "Adam McKay",
                    GenreId = 2, 
                    PosterUrl = "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p12157971_p_v13_al.jpg",
                    Actors = "Christian Bale, Steve Carell, Ryan Gosling",
                    TrailerUrl = "https://www.youtube.com/watch?v=vgqG3ITMv1Q"
                }
            );
        }
    }
}
