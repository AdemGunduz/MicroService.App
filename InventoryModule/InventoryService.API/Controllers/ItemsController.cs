using InventoryService.Data.Dtos;
using InventoryService.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemRepository itemRepository;

        public ItemsController(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await itemRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await itemRepository.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ItemCreateDto dto)
        {
            var result = await itemRepository.Create(new Data.Entitites.Item
            {
                Name = dto.Name
            });
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ItemUpdateDto dto)
        {
            await itemRepository.Update(new Data.Entitites.Item
            { Name = dto.Name, Id = dto.Id, });

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await itemRepository.Remove(id);
            return NoContent();
        }
    }
}
