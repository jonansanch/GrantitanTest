using GranTitan.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcces.Config
{
    public class AuthorConfig
    {
        public AuthorConfig(EntityTypeBuilder<Author> entityBuilder)
        {
            entityBuilder.Property(x => x.Id).IsRequired();
            entityBuilder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.SecondName).HasMaxLength(50);
            entityBuilder.Property(x => x.Surname).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.SecondSurname).HasMaxLength(50);
            entityBuilder.Property(x => x.BirthDate).IsRequired();                        
        }
    }
}