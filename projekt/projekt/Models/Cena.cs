using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    [Table("Cena")]
    public class Cena
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Wpisz to pole")]
        [StringLength(160)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Style nazewnictwa", Justification = "<Oczekujące>")]
        public string Cenaa { get; set; }
        public ICollection<Film> Films { get; set; }
    }
}
