using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    [Table("FilmAktor")]
    public class FilmAktor
    {
        public int AktorId { get; set; }
        public Aktor Aktor { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
    }
}

