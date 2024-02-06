using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingList.Contracts;
using ShoppingList.Data.Models;
using ShoppingList.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShoppingList.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
        
        [HttpGet]
        public IActionResult AddProductNote(int id)
        {
            var productNote = new ProductNotesViewModel(); 


            return View(productNote);
        }

       [HttpPost]
       public async Task<IActionResult> AddProductNote(ProductNotesViewModel product)
       {
            if (ModelState.IsValid == false)
            {
                return View(product);
            }

            await productService
            .AddNoteToProduct(product);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<ProductViewModel>
            model = await productService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult  Add() 
        {
            var model = new ProductViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if(ModelState.IsValid == false)
            {
                return View(model);
            }

             await productService
            .AddProductAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id) 
        {
            var model = await productService.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model , int id)
        {
            if(ModelState.IsValid == false || model.Id != id)
            {
                return View(model);
            }

            await productService.UpdateProductAsync(model);

            return  RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.
            DeleteProductAsync(id);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> GetByIdProduct(int id)
        {
           var model =  await productService.
                GetByIdAsync(id);

            return View(model);
        }


    }
}
