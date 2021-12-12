using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tulkas.Core.Components;
using Tulkas.Core.Domain;
using Tulkas.Core.Services;

namespace Tulkas.Api.Controllers
{
    [Route("api/products")]
    public class ProductController:BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get() => await GetResponse(async () => await _productService.Get());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(string id = "") =>
            await GetResponse(async () => await _productService.Get(id));

        [HttpPost]
        public async Task<IActionResult> Add(Product product) =>
            await GetResponse(async () => await _productService.Add(product));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Product product) =>
            await GetResponse(async () => await _productService.Update(id, product));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) =>
            await GetResponse(async () => await _productService.Delete(id));
    }
}