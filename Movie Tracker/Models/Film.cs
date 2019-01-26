
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie_Tracker.Models
{
    public partial class Film
    {
        public Film()
        {
            FilmActor = new HashSet<FilmActor>();
            FilmProducator = new HashSet<FilmProducator>();
            UserFilm = new HashSet<UserFilm>();
        }

        [Key] public long IdFilm { get; set; }
        public string Gen { get; set; }
        public string Durata { get; set; }
        public DateTime? DataLansare { get; set; }
        public long? IdRegizor { get; set; }
        public long? IdScenarist { get; set; }
        public long? IdCompozitor { get; set; }

        public Compozitor IdCompozitorNavigation { get; set; }
        public Regizor IdRegizorNavigation { get; set; }
        public Scenarist IdScenaristNavigation { get; set; }
        public ICollection<FilmActor> FilmActor { get; set; }
        public ICollection<FilmProducator> FilmProducator { get; set; }
        public ICollection<UserFilm> UserFilm { get; set; }
    }
}
