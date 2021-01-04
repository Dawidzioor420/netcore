using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    [Table("Grades")]
    public class Grade
    {
        public int AktorId { get; set; }
        public int FilmId { get; set; }
        public int  Ocena { get; set; }
        public Aktor Aktor { get; set; }
        public Film Film { get; set; }
    }
}
