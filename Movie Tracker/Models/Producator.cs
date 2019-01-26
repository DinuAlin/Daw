using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie_Tracker.Models
{
    public partial class Producator
    {
        public Producator()
        {
            FilmProducator = new HashSet<FilmProducator>();
        }

        [Key] public long IdProducator { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime? DataNastere { get; set; }

        public ICollection<FilmProducator> FilmProducator { get; set; }
    }
}
