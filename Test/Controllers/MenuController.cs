using Microsoft.AspNetCore.Mvc;
using Test.Filters;
using Test.Services.Interfaces;
using Test.Services.ViewModels;

namespace Test.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string keyword)
        {
            var data = _menuService.GetAll(keyword);
            return Ok(data);
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Add([FromBody] MenuViewModel vm)
        {
            var model = _menuService.Add(vm);
            _menuService.SaveChanges();
            return Ok(model);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public IActionResult Edit(int id, [FromBody] MenuViewModel vm)
        {
            var model = _menuService.Edit(id, vm);
            _menuService.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _menuService.Delete(id);
            _menuService.SaveChanges();
            return Ok(model);
        }
    }
}