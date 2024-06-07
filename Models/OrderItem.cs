using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicSoftware.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        
        public int OrderId { get; set; }
        
        public int ProcedureId { get; set; }
        
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
  
        public virtual Order? Order { get; set; }
        public virtual Procedure? Procedure { get; set; }



    }
}
