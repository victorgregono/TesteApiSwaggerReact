using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryServices;
using Microsoft.AspNetCore.Http;
using LibraryModells;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjetoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly IProduto _service;

        public ProdutoController(IProduto service)
        {
            _service = service;
        }


        // GET: api/Employee
        [HttpGet]
        //[Authorize]
        //[ProducesDefaultResponseType]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        //public async Task<IActionResult> Get()
        [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            try
            {
                var recebe = await _service.GetAll();

                if (recebe == null || recebe.Count() == 0)
                {
                    return NotFound(new { Mensagem = "Não existe dados!" });
                }
                return Ok(recebe);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.GetBaseException().Message });
            }
        }
        //buscar produto por idproduto
        [HttpGet("{key}", Name = "GetTodo")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetById(int key)
        {
            try
            {
                var recebe = await _service.GetFind(key);

                if (recebe == null || recebe.Count() == 0)
                {
                    return NotFound(new { Mensagem = "Não existe dados!" });
                }
                return Ok(recebe);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.GetBaseException().Message });
            }
        }


        [HttpPost, Route("Insert")]

        [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<int>> insertProduto([FromBody] Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    return NotFound(new { Mensagem = "Não existe dados!" });
                }

                var recebe = await _service.Insertproduto(produto);

                return Ok(recebe);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.GetBaseException().Message });
            }
        }

        [HttpPost, Route("Update")]

        [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<int>> Updateproduto([FromBody] Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    return NotFound(new { Mensagem = "Não existe dados!" });
                }

                var recebe = await _service.Updateproduto(produto);

                return Ok(recebe);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.GetBaseException().Message });
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound(new { Mensagem = "Informe IdProduto!" });
                }
                var recebe = await _service.DeleteProduto(id);

                if (recebe == null || recebe == 0)
                {
                    return NotFound(new { Mensagem = "Não existe dados!" });
                }
                return Ok(recebe);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.GetBaseException().Message });
            }
        }

    }
}
