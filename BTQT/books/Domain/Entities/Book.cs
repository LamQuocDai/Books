using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public int AuthorId { get; set; }
        public int BookTypeId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual Author Author { get; set; } = default!;

        [ForeignKey(nameof(BookTypeId))]
        public virtual BookType BookType { get; set; } = default!;

        [Required]
        public DateOnly ReleaseDate { get; set; }

        public virtual ICollection<BookCatalogue> BookCatalogues { get; set; } = new HashSet<BookCatalogue>();

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();

        public virtual ICollection<CartDetail> CartDetails { get; set; } = new HashSet<CartDetail>();
    }
}
