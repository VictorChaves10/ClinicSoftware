using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicSoftware.Models
{
    public class Procedure
    {
        public int ProcedureId { get; set; }

        [Required(ErrorMessage ="Informe o nome do procedimento")]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string? ProcedureName { get; set; }

        [StringLength(500)]
        [Display(Name = "Descrição")]
        public string? ProcedureDescription { get; set; }

        [Required(ErrorMessage = "Informe o preço do procedimento")]
        [Display(Name = "Preço")]
        [Column(TypeName = "Decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre 1 e 999,99")]
        public decimal ProcedurePrice { get; set; }

    }
}
