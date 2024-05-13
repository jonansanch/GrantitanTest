using GranTitan.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTitan.DAL.Entities
{
    public class AuthorDto : PayloadBody
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public required string Surname { get; set; }
        public string? SecondSurname { get; set; }
        public required DateTime BirthDate { get; set; }
    }
}
