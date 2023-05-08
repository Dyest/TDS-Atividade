using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.API.Data;
using Restaurante.API.Models;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("/Pedido/Create")]
        public IActionResult Post([FromBody] PedidoModel pedidoModel,
            [FromServices] AppDbContext context)
        {
            context.Pedido!.Add(pedidoModel);
            context.SaveChanges();
            return Created($"/{pedidoModel.PedidoId}", pedidoModel);
        }
    }
}