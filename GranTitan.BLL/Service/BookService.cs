using DataAcces.BusinessExeption;
using FluentValidation;
using FluentValidation.Results;
using GranTitan.BLL.Interface;
using GranTitan.BLL.Validators;
using GranTitan.DAL.Entities;
using GranTitan.DAL.Interface;
using Microsoft.Extensions.Options;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace GranTitan.BLL.Service
{
    public partial class BookService(IRepository<Book> _repositoryBook, IUnitOfWork unitOfWork, BookValidator _validations) : IBookService
    {
        public async Task<List<Book>> GetAllAsync()
        {
            List<Book> dataList = (await _repositoryBook.GetManyAsync()).ToList();

            return dataList;
        }

        public async Task<Book> GetId(Guid vBookID)
        {
            var data = await _repositoryBook.GetOneAsync(vBookID);

            return data;
        }
        public async Task<object> AddAsync(Book data)
        {
            Validate(data);
            try
            {
                if (await ExistByNameAndLibrary(data))
                {
                    return new
                    {
                        data = false,
                        status = "error",
                        msg = "Ya existe un libro con la misma libreria y nombre en el sistema."
                    };
                }
                await _repositoryBook.AddAsync(data);
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
        public async Task<object> UpdateAsync(Book data)
        {
            Validate(data);
            try
            {
                if (await ExistByNameAndLibrary(data))
                {
                    return new
                    {
                        data = false,
                        status = "error",
                        msg = "Ya existe un libro con la misma libreria y nombre en el sistema."
                    };
                }

                _repositoryBook.UpdateAsync(data);
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

        public async Task<bool> DeleteAsync(Guid vBookID)
        {
            try
            {
                var data = await _repositoryBook.GetOneAsync(vBookID);

                _repositoryBook.DeleteAsync(data);
                await unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        private async Task<bool> ExistByNameAndLibrary(Book data)
        {
            return (await _repositoryBook.GetManyAsync(x => x.Name.ToLower() == data.Name.ToLower() && x.Library.ToLower() == data.Library.ToLower())).Count() > 1 ? true : false;
        }

        private void Validate(Book data)
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
