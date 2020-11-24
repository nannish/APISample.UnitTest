using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISample.DbEntities
{
    public interface ISampleDbContext : IDisposable
    {
        int SaveChanges();
        DbSet<User> Users { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    }
}
