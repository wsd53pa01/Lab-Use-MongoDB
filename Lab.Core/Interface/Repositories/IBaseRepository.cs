using System.Collections.Generic;
using Lab.Core.DIType;
using Lab.Core.Entities;
using MongoDB.Driver;

namespace Lab.Core.Interface.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        List<T> Get();
        List<T> Get(FilterDefinition<T> filter);
        T Create(T data);
        void Update(FilterDefinition<T> filter , T data);
        void Remove(FilterDefinition<T> filter);
    }
}