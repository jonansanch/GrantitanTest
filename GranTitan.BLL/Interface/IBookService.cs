using GranTitan.DAL.Entities;

namespace GranTitan.BLL.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<Book> GetId(Guid vAuthorID);
        Task<object> AddAsync(BookCreateDto data);
        Task<object> UpdateAsync(BookCreateDto data);
        Task<bool> DeleteAsync(Guid vAuthorID);
    }
}
