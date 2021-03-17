using System.Collections.Generic;
using Lab.Core.Entities;
using Lab.Core.Interface.Repositories;
using MongoDB.Driver;

namespace Lab.Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private const string DATABASE = "jobsDB";
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly string _collection;

        public BaseRepository(IMongoClient mongoClient,
            IClientSessionHandle clientSessionHandle,
            string collection)
        {
            _mongoClient = mongoClient;
            _clientSessionHandle = clientSessionHandle;
            _collection = collection;
            if (!_mongoClient.GetDatabase(DATABASE).ListCollectionNames().ToList().Contains(collection))
                _mongoClient.GetDatabase(DATABASE).CreateCollection(collection);
        }
        protected virtual IMongoCollection<T> Collection =>
            _mongoClient.GetDatabase(DATABASE).GetCollection<T>(_collection);

        public T Create(T data)
        {
            throw new System.NotImplementedException();
        }

        public List<T> Get()
        {
            var test1 = _mongoClient.GetDatabase(DATABASE).GetCollection<T>(_collection).AsQueryable();
            var test = Collection.AsQueryable().ToList();
            return test;
        }

        public List<T> Get(FilterDefinition<T> filter)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(FilterDefinition<T> filter)
        {
        throw new System.NotImplementedException();
        }

        public void Update(FilterDefinition<T> filter, T data)
        {
        throw new System.NotImplementedException();
        }
    }
}