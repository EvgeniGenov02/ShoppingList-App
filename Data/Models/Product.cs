using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Data.Models
{
    [Comment("Shopping List Product")]
    public class Product
    {
        [Key]
        [Comment("Product id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Comment("Product Name")]
        public string Name { get; set; } = null!;

        public List<ProductNotes> ProductNotes { get; set; }
          = new List<ProductNotes>();

    }
}