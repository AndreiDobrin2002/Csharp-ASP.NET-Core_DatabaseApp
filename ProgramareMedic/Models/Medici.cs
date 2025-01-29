using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgramareMedic.Models
{
    public partial class Medici
    {
        public Medici()
        {
            Programari = new HashSet<Programari>();
        }

        [DisplayName("Medic ID")]
        public int Id { get; set; }

        [DisplayName("Nume")]
        public string NumeMedic { get; set; } = null!;

        [DisplayName("Prenume")]
        public string PrenumeMedic { get; set; } = null!;

        [DisplayName("Specializare")]
        public string Specializare { get; set; } = null!;

        [DisplayName("Clinica")]
        public string Clinica { get; set; } = null!;

        public virtual ICollection<Programari> Programari { get; set; }

        [NotMapped]
        public string NumeComplet => $"{NumeMedic} {PrenumeMedic}";
    }
}
