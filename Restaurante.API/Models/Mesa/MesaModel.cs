using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.API.Models
{
    public class MesaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MesaId { get; set; }

        [Required(ErrorMessage = "Número é obrigatório!")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Status é obrigatório!")]
        public bool Status { get; set; }
        
        public DateTime? HoraAbertura { get; set; }
    }
}