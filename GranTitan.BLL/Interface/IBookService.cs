using GranTitan.DAL.Entities;

namespace GranTitan.BLL.Interface
{
    public interface IBookService
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetId(Guid vAuthorID);
        Task<object> AddAsync(Book data);
        Task<object> UpdateAsync(Book data);
        Task<bool> DeleteAsync(Guid vAuthorID);
    }
}
