using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgramareMedic.Models
{
    public partial class Pacienti
    {
        public Pacienti()
        {
            Programari = new HashSet<Programari>();
        }

        [DisplayName("Pacient ID")]
        public int Id { get; set; }

        [DisplayName("Nume")]
        public string Nume { get; set; } = null!;

        [DisplayName("Prenume")]
        public string Prenume { get; set; } = null!;

        [DisplayName("Varsta")]
        public int Varsta { get; set; }

        [DisplayName("Adresa")]
        public string Adresa { get; set; } = null!;

        public virtual ICollection<Programari> Programari { get; set; }

        [NotMapped]
        public string NumeComplet => $"{Nume} {Prenume}";
    }
}
