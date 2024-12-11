using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities

{
    [Table("order_details")]
    public class OrderDetail {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int BookId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; } = default!;


        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; } = default!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
