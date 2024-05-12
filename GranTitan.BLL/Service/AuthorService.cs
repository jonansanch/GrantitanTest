using DataAcces.BusinessExeption;
using FluentValidation.Results;
using GranTitan.BLL.Interface;
using GranTitan.BLL.Validators;
using GranTitan.DAL.Entities;
using GranTitan.DAL.Interface;

namespace GranTitan.BLL.Service
{
    public partial class AuthorService(IRepository<Author> _repositoryAuthor, IUnitOfWork unitOfWork,AuthorValidator _validations) : IAuthorService
    {
        public async Task<List<Author>> GetAllAsync()
        {
            List<Author> dataList = (await _repositoryAuthor.GetManyAsync()).ToList();

            return dataList;
        }

        public async Task<Author> GetId(Guid vAuthorID)
        {
            var data = await _repositoryAuthor.GetOneAsync(vAuthorID);   

            return data;
        }
        public async Task<object> AddAsync(Author data)
        {
            Validate(data);
            try
            {               
                if (await ExistByNameOrSurname(data))
                {
                    return new
                    {
                        data = false,
                        status = "error",
                        msg = "Ya existe un autor con el mismo nombre y apellido en la base de datos, favor revisar."
                    };
                }
                await _repositoryAuthor.AddAsync(data);
                await unitOfWork.SaveAsync();
                return new
                {
                    data = true,
                    status = "success",
                    msg = "success_save"
                };
            }
            catch (Exception e)
            {
                return new
                {
                    data = false,
                    status = "error",
                    msg = "Se genero un error favor revisar los datos y volver a intentar." + e.Message
                };
                throw;
            }
            
        }
        public async Task<object> UpdateAsync(Author data)
        {
            Validate(data);
            try
            {
                _repositoryAuthor.UpdateAsync(data);
                await unitOfWork.SaveAsync();
                return new
                {
                    data = true,
                    status = "success",
                    msg = "success_save"
                };
            }
            catch (Exception e)
            {
                return new
                {
                    data = false,
                    status = "error",
                    msg = "Se genero un error favor revisar los datos y volver a intentar." + e.Message
                };
                throw;
            }
            
        }

        public async Task<bool> DeleteAsync(Guid vAuthorID)
        {
            try
            {
                var data = await _repositoryAuthor.GetOneAsync(vAuthorID);

                _repositoryAuthor.DeleteAsync(data);
                await unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }  
        
        private async Task<bool> ExistByNameOrSurname(Author data)
        {
            return (await _repositoryAuthor.GetManyAsync(x => x.FirstName.ToLower() == data.FirstName.ToLower() && x.Surname.ToLower() == data.Surname.ToLower())).Count() > 1 ? true : false;
        }

        private void Validate(Author data)
        {
            ValidationResult results = _validations.Validate(data);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    throw new BusinessExeption("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }
        }
    }
}
