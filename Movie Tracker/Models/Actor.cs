using Movie_Tracker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie_Tracker.Models 
{
    public partial class Actor
    {
        public Actor()
        {
            FilmActor = new HashSet<FilmActor>();
        }

        [Key] public long IdActor { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime? DataNastere { get; set; }

        public ICollection<FilmActor> FilmActor { get; set; }
    }
}
