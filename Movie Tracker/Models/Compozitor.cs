using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie_Tracker.Models
{
    public partial class Compozitor
    {
        public Compozitor()
        {
            Film = new HashSet<Film>();
        }

        [Key] public long IdCompozitor { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime? DataNastere { get; set; }

        public ICollection<Film> Film { get; set; }
    }
}
