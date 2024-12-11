using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = default!;

        public virtual ICollection<CartDetail> CartDetails { get; set; } = new HashSet<CartDetail>();
    }
}
