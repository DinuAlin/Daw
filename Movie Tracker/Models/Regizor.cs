using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie_Tracker.Models
{
    public partial class Regizor
    {
        public Regizor()
        {
            Film = new HashSet<Film>();
        }

        [Key] public long Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime? DataNastere { get; set; }

        public ICollection<Film> Film { get; set; }
    }
}
