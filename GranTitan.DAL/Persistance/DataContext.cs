using GranTitan.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GranTitan.DAL.Persistance
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                return;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            modelBuilder.Entity<Author>();
            modelBuilder.Entity<Book>();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var type = entityType.ClrType;
                if (typeof(DomainEntity).IsAssignableFrom(type))
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedOn");
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModifiedOn");
                }
            }

            base.OnModelCreating(modelBuilder);
        }        
    }
}
