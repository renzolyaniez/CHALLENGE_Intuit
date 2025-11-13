using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API_Clientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        private readonly ICliente _cliente;

        public ClientesController(ICliente cliente)
        {
            _cliente = cliente;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetAll(string? search)
        {
            var clientes = string.IsNullOrWhiteSpace(search)
                ? await _cliente.GetAll()
                : await _cliente.Search(search);

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var a = await _cliente.GetById(id);


            if (a == null)
            {
                return NotFound();
            }

            return Ok(a);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }

            var a = await _cliente.Insert(cliente);

            if (a.isSuccess)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }

            var a = await _cliente.Update(cliente);

            if (a.isSuccess)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("El Id no puede ser 0 o nulo");
            }

            var a = await _cliente.Delete(id);

            if (a.isSuccess)
            {
                return Ok(a.msg);
            }

            return BadRequest(a.msg);
        }
    }
}
