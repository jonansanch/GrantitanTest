using System.ComponentModel.DataAnnotations.Schema;

namespace GranTitan.DAL.Entities
{
    public class Book : DomainEntity
    {        
        public required Guid AuthorId { get; set; }
        public required string Name { get; set; }
        public required string Library { get; set; }
        public required int Pages { get; set; }
        public required int Price { get; set; }
        public required DateTime ReleaseDate { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author? AuthorBook { get; set; }

    }
}
