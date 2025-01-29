using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgramareMedic.Models
{
    public partial class Programari
    {
        [DisplayName("Programare ID")]
        public int Id { get; set; }

        [DisplayName("Pacient ID")]
        public int PacientId { get; set; }

        [DisplayName("Medic ID")]
        public int MedicId { get; set; }

        [DisplayName("Data Programare")]
        public DateTime DataProgramare { get; set; } = DateTime.Now;

        [DisplayName("Categorie Servicii")]
        public string CategorieServicii { get; set; } = null!;

        [DisplayName("PacientId")]
        public virtual Pacienti Pacient { get; set; } = null!;

        [ForeignKey("MedicId")]
        public virtual Medici Medic { get; set; } = null!;
    }
}
