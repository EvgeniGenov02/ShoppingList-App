using Microsoft.EntityFrameworkCore;
using ShoppingList.Contracts;
using ShoppingList.Data;
using ShoppingList.Data.Models;
using ShoppingList.Models;

namespace ShoppingList.Service
{
    public class ProductService : IProductService
    {
        private readonly ShoppingListDbContext _context;
        public ProductService(ShoppingListDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductNotesViewModel>> GetNotesByIDAsync(int productId)
        {
            var notes = new List<ProductNotesViewModel>();

            var contextProductNotes = await _context.ProductNotes.AsNoTracking().ToListAsync(); 

            foreach (var item in contextProductNotes)
            {
                if (item.ProductId == productId) 
                {
                    notes.Add(new ProductNotesViewModel()
                    {
                        Id = item.ProductId,
                        Content = item.Content
                    });
                }
            }

            return notes;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();

            var productViewModels = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var productNotes = GetNotesByIDAsync(product.Id);
                productViewModels.Add(new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    ProductNotes = await productNotes
                });
            }

            return productViewModels;
        }



        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid Product");
            }

            return new ProductViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
        public async Task AddNoteToProduct(ProductNotesViewModel productNotes)
        {
            if (productNotes == null)
            {
                throw new ArgumentException("Invalid Note");
            }


            _context.ProductNotes.Add(new ProductNotes()
            {
                Content = productNotes.Content,
                ProductId = productNotes.Id
            });

            await _context.SaveChangesAsync();
        }

        public async Task AddProductAsync(ProductViewModel model)
        {
            await _context.Products.AddAsync(new Product
            {
                Name = model.Name,
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {

            var entity = await _context.Products.FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid Product");
            }



            _context.Products.Remove(entity);

            await _context.SaveChangesAsync();
        }
     
        public async Task<List<ProductNotesViewModel>> GetNotesByID(int productId)
        {
            var notes = await _context.ProductNotes.Where(pn => pn.Id == productId).ToListAsync();
            return (List<ProductNotesViewModel>)(notes?.Select(p => new ProductNotesViewModel
            {
                Id = p.Id,
                Content = p.Content
            }) ?? Enumerable.Empty<ProductNotesViewModel>());
        }

        public async Task UpdateProductAsync(ProductViewModel model)
        {

            var entity = await _context.Products.FindAsync(model.Id);
            if (entity == null)
            {
                throw new ArgumentException("Invalid Product");
            }
            entity.Name = model.Name;

            await _context.SaveChangesAsync();
        }
    }
}