using MarketService.Data.Dtos;
using MarketService.Data.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;

namespace MarketService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketsController : ControllerBase
    {
        private readonly IMarketRepository _marketRepository;
        private IPublishEndpoint _publishEndpoint;

        public MarketsController(IMarketRepository marketRepository, IPublishEndpoint publishEndpoint)
        {
            _marketRepository = marketRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _marketRepository.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MarketDtos dto)
        {
            var result = _marketRepository.Add(new Data.Entities.Market
            {
                InventoryId = dto.InventoryId,
                ItemId = dto.ItemId,
                PlayerId = dto.PlayerId,
                Price = dto.Price,
            });

            await _publishEndpoint.Publish<IMarketCreated>(new
            {
                dto.InventoryId,
                dto.ItemId,
                Count = 1
            });
            return Created("", result);
        }

    }
}
