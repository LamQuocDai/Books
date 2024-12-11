using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("book_catalogues")]
    public class BookCatalogue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookCatalogueId { get; set; }

        public int BookId { get; set; }

        public int CatalogueId { get; set; }

        [Column("book_id")]
        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; } = default!;

        [Column("catalogue_id")]
        [ForeignKey(nameof(CatalogueId))]
        public virtual Catalogue Catalogue { get; set; } = default!;

    }
}
