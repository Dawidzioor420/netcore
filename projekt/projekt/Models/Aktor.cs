using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    [Table("Aktor")]
    public class Aktor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Required(ErrorMessage = "Wpisz to pole")]
        [StringLength(160)]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Wpisz to pole")]
        [StringLength(160)]
        public string Nazwisko { get; set; }
        public ICollection<Film>Films { get; set; }
        public ICollection<FilmAktor> FilmAktors { get; set; }
        public string ImieNazwisko
        {
            get
            {
                return Imie+" "+Nazwisko;
            }
        }
    }
}
