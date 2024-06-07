using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicSoftware.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }

        [Display(Name = "Data do atendimento")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total do atendimento")]
        public decimal TotalValue { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total de desconto")]
        public decimal TotalDiscount { get; set; }

        [AllowedValues("Pix", "Crédito", "Débito", "Dinheiro", "Outro")]
        public string? PaymentMethod  { get; set; }

        public List<OrderItem>? OrderItens { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Client? Client { get; set; }

    }
}
