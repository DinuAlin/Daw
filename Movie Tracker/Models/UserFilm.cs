using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie_Tracker.Models
{
    public partial class UserFilm
    {
        [Key] public long IdUserFilm { get; set; }
        public long? IdFilm { get; set; }

        public Film IdFilmNavigation { get; set; }
    }
}
