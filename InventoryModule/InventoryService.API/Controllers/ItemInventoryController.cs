using InventoryService.Data.Dtos;
using InventoryService.Data.Entitites;
using InventoryService.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemInventoryController : ControllerBase
    {
        private readonly ItemInventoryRepository itemInventoryRepository;

        public ItemInventoryController(ItemInventoryRepository itemInventoryRepository)
        {
            this.itemInventoryRepository = itemInventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await itemInventoryRepository.GetAll();
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    var result = await itemInventoryRepository.GetById(id);
        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(ItemInventoryCreateDto dto)
        {
            var result = await itemInventoryRepository.Create(new ItemInventory
            {
                ItemId = dto.ItemId,
                InventoryId = dto.InventoryId,
                Count = dto.Count
            });
            return Created("", result);
        }


        //     Envanterdeki bir öğenin sayısını günceller
        //     Market servisinde satış olduğu zaman count azalır
    
        [HttpPatch]
        public async Task<IActionResult> Update(ItemInventoryUpdateDto dto)
        {
           var updatedEntity = await itemInventoryRepository.GetItemInventory(dto.ItemId, dto.InventoryId);
            if (updatedEntity != null)
            {
                updatedEntity.Count = updatedEntity.Count - dto.Count;
                await itemInventoryRepository.Update(updatedEntity);
                return NoContent();
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await itemInventoryRepository.Remove(id);
            return NoContent();
        }
    }
}
