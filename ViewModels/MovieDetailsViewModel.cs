using IMDBClone.Models;
using System.Collections.Generic;

namespace IMDBClone.ViewModels
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; }
        public string GenreName { get; set; }
        public List<ReviewDisplayViewModel> Reviews { get; set; }
        public string Actors { get; set; }
        public string TrailerUrl { get; set; }
        public bool IsInWatchlist { get; set; }


    }
}
