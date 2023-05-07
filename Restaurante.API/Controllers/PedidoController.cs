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

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(PedidoModel pedidoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Pedido.Add(pedidoModel);
            _context.SaveChanges();

            return Created($"/Pedido/{pedidoModel.PedidoId}", pedidoModel);
        }
    }
}