using Lab.Core.Entities;
using Lab.Core.Interface.Repositories;
using MongoDB.Driver;

namespace Lab.Repository.Repositories
{
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(
            IMongoClient mongoClient, 
            IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "inventory")
        {

        }
    }
}