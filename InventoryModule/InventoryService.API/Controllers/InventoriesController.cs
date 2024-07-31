using InventoryService.Data.Dtos;
using InventoryService.Data.Entitites;
using InventoryService.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly InventoryRepository inventoryRepository;

        public InventoriesController(InventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await inventoryRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await inventoryRepository.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(InventoryCreateDto dto)
        {
            var result = await inventoryRepository.Create(new Inventory
            {
                PlayerId = dto.PlayerId,
                Name = dto.Name
            });
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(InventoryUpdateDto dto)
        {
            await inventoryRepository.Update(new Inventory
            {
                PlayerId = dto.PlayerId,
                Name = dto.Name,
                Id = dto.Id,
            });

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await inventoryRepository.Remove(id);
            return NoContent();
        }
    }
}
