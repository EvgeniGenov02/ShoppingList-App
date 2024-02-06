using ShoppingList.Data;
using ShoppingList.Data.Models;
using ShoppingList.Models;
using System.Collections;

namespace ShoppingList.Contracts
{
    public interface IProductService
    {
            Task<IEnumerable<ProductViewModel>> GetAllAsync();
            Task<ProductViewModel> GetByIdAsync(int id);
        Task<IEnumerable<ProductNotesViewModel>> GetNotesByIDAsync(int productId);
            Task AddProductAsync(ProductViewModel model);
            Task UpdateProductAsync(ProductViewModel model);
            Task DeleteProductAsync(int id);
            Task<List<ProductNotesViewModel>> GetNotesByID(int productId);
            Task AddNoteToProduct(ProductNotesViewModel model);
    }
}
