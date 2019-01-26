using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie_Tracker.Models
{
    public partial class FilmProducator
    {
        [Key] public long IdFilmProducator { get; set; }
        public long? IdProducator { get; set; }
        public long? IdFilm { get; set; }

        public Film IdFilmNavigation { get; set; }
        public Producator IdProducatorNavigation { get; set; }
    }
}
