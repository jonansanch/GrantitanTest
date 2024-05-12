using GranTitan.BLL.Interface;
using GranTitan.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GranTitan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(IAuthorService _authorService) : ControllerBase
    {
        /// <summary>
        /// Obtiene la lista de todos los autores
        /// </summary>
        /// <returns></returns>

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var lstAreaDeTrabajo = await _authorService.GetAllAsync();
            return Ok(lstAreaDeTrabajo);
        }

        /// <summary>
        /// Obtiene autor por id
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAuthorId/{id}")]
        public async Task<IActionResult> GetUserId(Guid id)
        {
            var lst = await _authorService.GetId(id);
            return Ok(lst);
        }

        /// <summary>
        /// Crea un autor
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<IActionResult> Create(Author data)
        {
            var result = await _authorService.AddAsync(data);
            return Ok(result);
        }

        /// <summary>
        /// Actualiza un autor
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]

        public async Task<IActionResult> Update(Author data)
        {
            var result = await _authorService.UpdateAsync(data);
            return Ok(result);
        }

        /// <summary>
        /// Elimina un autor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<object> Delete(Guid id)
        {
            var result = await _authorService.DeleteAsync(id);
            return new
            {
                data = result,
                status = "success",
                msg = "success_save"
            };
        }
    }
}
