using Models;
using System;
using System.Collections.Generic;

namespace DBManager
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Add(TEntity entity);
        void Update(User user, TEntity entity);
        void Delete(User user);
    }
}
