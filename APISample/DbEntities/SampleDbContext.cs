using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISample.DbEntities
{
    public class SampleDbContext : DbContext, ISampleDbContext
    {
        private readonly string _connectionString;
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {
        }
        public SampleDbContext(string connectionString) : base()
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
        public DbSet<User> Users { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }
        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            return base.Add(entity);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<BaseEntity>().Property(b => b.CreatedDate).HasDefaultValueSql("getutcdate()");

            new UserMap(modelBuilder.Entity<User>());
        }
    }
}
