using InventoryService.Data.Entitites;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Repositories
{
    public class ItemInventoryRepository
    {
        private readonly IMongoCollection<ItemInventory> itemInventoryCollection;
        public ItemInventoryRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("InventoryDb");

            itemInventoryCollection = database.GetCollection<ItemInventory>("itemInventories");
        }

        public async Task<List<ItemInventory>?> GetAll()
        {
            var filter = Builders<ItemInventory>.Filter.Empty;
            var result = await itemInventoryCollection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<ItemInventory?> GetById(string id)
        {
            var filter = Builders<ItemInventory>.Filter.Eq(x => x.Id, id);

            var result = await itemInventoryCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ItemInventory> Create(ItemInventory itemInventory)
        {
            await itemInventoryCollection.InsertOneAsync(itemInventory);

            return itemInventory;
        }

        public async Task Update(ItemInventory updatedItemInventory)
        {
            var filter = Builders<ItemInventory>.Filter.Eq(x => x.Id, updatedItemInventory.Id);
            await itemInventoryCollection.FindOneAndReplaceAsync(filter, updatedItemInventory);
        }

        public async Task Remove(string id)
        {
            var filter = Builders<ItemInventory>.Filter.Eq(x => x.Id, id);
            await itemInventoryCollection.DeleteOneAsync(filter);
        }
    }
}
