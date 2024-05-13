using GranTitan.BLL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTitan.DAL.Entities
{
    public class BookDto : PayloadBody
    {
        public Guid Id { get; set; }
        public required Guid AuthorId { get; set; }
        public required string Name { get; set; }
        public required string Library { get; set; }
        public required int Pages { get; set; }
        public required int Price { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public virtual AuthorDto? AuthorBook { get; set; }        
    }

    public class BookCreateDto : PayloadBody
    {
        public Guid Id { get; set; }
        public required Guid AuthorId { get; set; }
        public required string Name { get; set; }
        public required string Library { get; set; }
        public required int Pages { get; set; }
        public required int Price { get; set; }
        public required DateTime ReleaseDate { get; set; }               
    }
}
