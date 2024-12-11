using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("cart_details")]
    public class CartDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartDetailId { get; set; }
        public int CartId { get; set; }
        public int BookId { get; set; }


        [ForeignKey(nameof(CartId))]
        public virtual Cart Cart { get; set; } = default!;

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; } = default!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
