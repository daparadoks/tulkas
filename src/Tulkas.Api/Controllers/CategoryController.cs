using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tulkas.Core.Components;
using Tulkas.Core.Domain;
using Tulkas.Core.Services;

namespace Tulkas.Api.Controllers
{
    [Route("api/categories")]
    public class CategoryController:BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => await GetResponse(async () => await _categoryService.Get());

        [HttpGet("{id}", Name = "Detail")]
        public async Task<IActionResult> GetDetail(string id) =>
            await GetResponse(async () => await _categoryService.Get(id));

        [HttpPost]
        public async Task<IActionResult> Add(Category category) =>
            await GetResponse(async () => await _categoryService.Add(category));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Category category) =>
            await GetResponse(async () => await _categoryService.Update(id, category));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) =>
            await GetResponse(async () => await _categoryService.Delete(id));
    }
}