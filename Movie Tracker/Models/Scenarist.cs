using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie_Tracker.Models
{
    public partial class Scenarist
    {
        public Scenarist()
        {
            Film = new HashSet<Film>();
        }

        [Key] public long IdScenarist { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime? DataNastere { get; set; }

        public ICollection<Film> Film { get; set; }
    }
}
