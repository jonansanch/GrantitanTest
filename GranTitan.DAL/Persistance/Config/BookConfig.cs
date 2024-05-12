using GranTitan.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcces.Config
{
    public class BookConfig
    {
        public BookConfig(EntityTypeBuilder<Book> entityBuilder)
        {
            entityBuilder.Property(x => x.Id).IsRequired();
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            entityBuilder.Property(x => x.Library).HasMaxLength(150);
            entityBuilder.Property(x => x.Pages).IsRequired();
            entityBuilder.Property(x => x.Price).IsRequired();
            entityBuilder.Property(x => x.ReleaseDate).IsRequired();                        
        }
    }
}