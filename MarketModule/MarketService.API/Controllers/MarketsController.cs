using InventoryService.Data.Repositories;
using MarketService.Data.Dtos;
using MarketService.Data.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace MarketService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketsController : ControllerBase
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ItemInventoryRepository _itemInventoryRepository;

        public MarketsController(IMarketRepository marketRepository, IPublishEndpoint publishEndpoint, ItemInventoryRepository itemInventoryRepository)
        {
            _marketRepository = marketRepository;
            _publishEndpoint = publishEndpoint;
            _itemInventoryRepository = itemInventoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MarketDtos dto)
        {
            var itemInventory = await _itemInventoryRepository.GetItemInventory(dto.ItemId, dto.InventoryId);
            if (itemInventory == null)
            {
                return BadRequest("Envanter bilgisi bulunamadı.");
            }
            var lockAcquired = await _itemInventoryRepository.TryLockMarketAsync(itemInventory.Id, TimeSpan.FromSeconds(30));
            // Kilit, başka bir işlem tarafından alınmamışsa true döner.


            if (!lockAcquired)
            {
                return Conflict("Şu anda başka bir işlem yapılıyor. Lütfen tekrar deneyin.");
            }

            try
            {
                // Envanteri kontrol et
                //var itemInventory =  await itemInventoryRepository.GetItemInventory(dto.ItemId, dto.InventoryId);
                if (itemInventory == null || itemInventory.Count < 1)
                {
                    return BadRequest("Yeterli stok yok.");
                }

               // itemInventory.Count -= dto.Quantity; // Stok miktarını azalt
               //await _itemInventoryRepository.Update(itemInventory);

                await _publishEndpoint.Publish<IMarketCreated>(new
                {
                    dto.InventoryId,
                    dto.ItemId,
                    Count = dto.Quantity
                });

                return Created("", Ok());
            }
            finally
            {
                await _itemInventoryRepository.ReleaseLockMarketAsync(itemInventory.Id); // Kilidi serbest bırak
            }
        }
    }
}
