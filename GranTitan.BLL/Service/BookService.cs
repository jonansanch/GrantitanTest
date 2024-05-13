using AutoMapper;
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
    public partial class BookService(IRepository<Book> _repositoryBook, IUnitOfWork unitOfWork, BookValidator _validations, IMapper _mapper) : IBookService
    {
        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            List<Book> dataList = (await _repositoryBook.GetManyAsync(includeStringProperties: "AuthorBook")).ToList();
            return _mapper.Map<IEnumerable<Book>, List<BookDto>>(dataList);
        }

        public async Task<Book> GetId(Guid vBookID)
        {
            var data = await _repositoryBook.GetOneAsync(vBookID, includeStringProperties: "AuthorBook");
            return data;
        }
        public async Task<object> AddAsync(BookCreateDto data)
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

                var bookDt = _mapper.Map<BookCreateDto, Book>(data);
                await _repositoryBook.AddAsync(bookDt);
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
        public async Task<object> UpdateAsync(BookCreateDto data)
        {
            Validate(data);
            try
            {
                if (data.Id == Guid.Empty)
                {
                    return new
                    {
                        data = false,
                        status = "error",
                        msg = "Datos incompletos favor revisar y volver a realizar el envio."
                    };
                }

                if (await ExistByNameAndLibrary(data))
                {
                    return new
                    {
                        data = false,
                        status = "error",
                        msg = "Ya existe un libro con la misma libreria y nombre en el sistema."
                    };
                }
                var bookDt = _mapper.Map<BookCreateDto, Book>(data);
                _repositoryBook.UpdateAsync(bookDt);
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

        private async Task<bool> ExistByNameAndLibrary(BookCreateDto data)
        {
            return (await _repositoryBook.GetManyAsync(x => x.Name.ToLower() == data.Name.ToLower() && x.Library.ToLower() == data.Library.ToLower())).Count() > 1 ? true : false;
        }

        private void Validate(BookCreateDto data)
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
