using GranTitan.BLL.Interface;
using GranTitan.BLL.Validators;
using GranTitan.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GranTitan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookService _bookService, BookValidator _validator) : ControllerBase
    {
        /// <summary>
        /// Obtiene la lista de todos los libros
        /// </summary>
        /// <returns></returns>

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var lstBooks = await _bookService.GetAllAsync();
            return Ok(lstBooks);
        }

        /// <summary>
        /// Obtiene libro por id
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBookId/{id}")]
        public async Task<IActionResult> GetUserId(Guid id)
        {
            var lst = await _bookService.GetId(id);
            return Ok(lst);
        }

        /// <summary>
        /// Crea un libro
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<IActionResult> Create(Book data)
        {            
            var result = await _bookService.AddAsync(data);
            return Ok(result);
        }

        /// <summary>
        /// Actualiza un libro
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]

        public async Task<IActionResult> Update(Book data)
        {
            var result = await _bookService.UpdateAsync(data);
            return Ok(result);
        }

        /// <summary>
        /// Elimina un libro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<object> Delete(Guid id)
        {
            var result = await _bookService.DeleteAsync(id);
            return new
            {
                data = result,
                status = "success",
                msg = "success_save"
            };
        }
    }
}
