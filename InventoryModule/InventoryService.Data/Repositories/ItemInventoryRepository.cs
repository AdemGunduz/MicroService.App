using InventoryService.Data.Entitites;
using MongoDB.Driver;
using StackExchange.Redis;

namespace InventoryService.Data.Repositories
{
    public class ItemInventoryRepository
    {
        private readonly StackExchange.Redis.IDatabase _redisDatabase;
        private readonly IMongoCollection<ItemInventory> itemInventoryCollection;
        public ItemInventoryRepository(IConnectionMultiplexer redisConnection)
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("InventoryDb");

            itemInventoryCollection = database.GetCollection<ItemInventory>("itemInventories");

            _redisDatabase = redisConnection.GetDatabase();
        }

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

        public async Task<ItemInventory?> GetItemInventory(string itemId, string inventoryId)
        {
            var filterI = Builders<ItemInventory>.Filter.Eq(x => x.ItemId, itemId);
            var filterII = Builders<ItemInventory>.Filter.Eq(x => x.InventoryId, inventoryId);


            var filter = Builders<ItemInventory>.Filter.And(filterI, filterII);

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

        public async Task<bool> TryLockMarketAsync(string itemInventoryId, TimeSpan expiry)
        {
            return await _redisDatabase.StringSetAsync($"itemInventory:lock:{itemInventoryId}", "locked", expiry, When.NotExists);
        }

        public async Task ReleaseLockMarketAsync(string itemInventoryId)
        {
            await _redisDatabase.KeyDeleteAsync($"itemInventory:lock:{itemInventoryId}");
        }
    }
}
