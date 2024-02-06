using Microsoft.EntityFrameworkCore;
using ShoppingList.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Models
{
    public class ProductNotesViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Content")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "Field {0} must be between {2} and {1} symbols")]
        public string Content { get; set; } = string.Empty;


    }
}
