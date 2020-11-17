using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryModells;
using LibraryServices;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjetoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedido _service;

        public PedidoController(IPedido service)
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
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Pedido>>> Get()
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
        //buscar Pedido por Id
        [HttpGet("{key}", Name = "GetTodoPedido")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetById(int key)
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

        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<int>> insertPedido([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido == null)
                {
                    return NotFound(new { Mensagem = "Não existe dados!" });
                }

                var recebe = await _service.InsertPedido(pedido);

                return Ok(recebe);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.GetBaseException().Message });
            }
        }

        [HttpPost, Route("Update")]

        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<int>> UpdatePesdido([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido == null)
                {
                    return NotFound(new { Mensagem = "Não existe dados!" });
                }

                var recebe = await _service.UpdatePedido(pedido);

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
                var recebe = await _service.DeletePedido(id);

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
