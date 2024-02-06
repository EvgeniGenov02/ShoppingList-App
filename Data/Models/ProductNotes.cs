using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Azure.Core.HttpHeader;

namespace ShoppingList.Data.Models
{
    [Comment("Shopping Product")]
    public class ProductNotes
    {
        [Key]
        [Comment("Note id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Comment("Note Content")]
        public string Content { get; set; } = string.Empty;

        [Required]
        [Comment("Product Id")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}