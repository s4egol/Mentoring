using Catalog.Business.Interfaces;
using Catalog.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _catalogService;

        public ProductController(IProductService catalogService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var product = (await _catalogService.GetAsync(id)).ToMvc();

            return View(product);
        }
    }
}
