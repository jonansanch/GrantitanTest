﻿using GranTitan.DAL.Entities;

namespace GranTitan.BLL.Interface
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<Author> GetId(Guid vAuthorID);
        Task<object> AddAsync(Author data);
        Task<object> UpdateAsync(Author data);
        Task<bool> DeleteAsync(Guid vAuthorID);
    }
}
