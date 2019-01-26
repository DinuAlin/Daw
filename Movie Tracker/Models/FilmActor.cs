using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie_Tracker.Models
{
    public partial class FilmActor
    {
        [Key] public long IdFilmActor { get; set; }
        public long? IdActor { get; set; }
        public long? IdFilm { get; set; }

        public Actor IdActorNavigation { get; set; }
        public Film IdFilmNavigation { get; set; }
    }
}
