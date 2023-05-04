using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.API.Data;
using Restaurante.API.Models;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PedidoProdutoController : ControllerBase
    {
        [HttpGet]
        [Route("/Pedido_Produto/{id:int}")]
        public IActionResult Get([FromRoute] int id, [FromServices] AppDbContext context){
            var pedidoProdutos = context.Pedido_Produto!.Include(p => p.Pedido).ThenInclude(p => p!.Garcon)
                .Include(p => p.Pedido).ThenInclude(p => p!.Atendimento).ThenInclude(a => a!.Mesa)
                .Include(p => p.Produto).Where(e => e.Pedido!.Atendimento!.AtendimentoId  == id).ToList();

            return Ok(pedidoProdutos);
        }

        [HttpGet("/Pedido_Produto/Details/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var pedidoProdutoModel = context.Pedido_Produto!.Include(p => p.Pedido)
                    .ThenInclude(p => p!.Garcon)
                .Include(p => p.Pedido)
                    .ThenInclude(p => p!.Atendimento)
                        .ThenInclude(a => a!.Mesa)
                .Include(p => p.Produto)
                .Where(e => e.Pedido!.Atendimento!.AtendimentoId == id)
                .FirstOrDefault(x => x.PedidoProdutoId == id);
            if (pedidoProdutoModel == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                Id = pedidoProdutoModel.PedidoProdutoId,
                PedidoId = pedidoProdutoModel.PedidoId,
                ProdutoId = pedidoProdutoModel.ProdutoId,
                Quantidade = pedidoProdutoModel.Quantidade,
                Pedido = new
                {
                    Id = pedidoProdutoModel.Pedido!.PedidoId,
                    AtendimentoId = pedidoProdutoModel.Pedido.AtendimentoId,
                    GarconId = pedidoProdutoModel.Pedido.GarconId,
                },
                Garcon = new
                {
                    Id = pedidoProdutoModel.Pedido.Garcon!.GarconId,
                    Nome = pedidoProdutoModel.Pedido.Garcon!.Nome,
                    Sobrenome = pedidoProdutoModel.Pedido.Garcon!.Sobrenome
                },
                Produto = new
                {
                    Id = pedidoProdutoModel.Produto!.ProdutoId,
                    Nome = pedidoProdutoModel.Produto.Nome,
                    Descricao = pedidoProdutoModel.Produto.Descricao,
                    Preco = pedidoProdutoModel.Produto.Preco
                }
            });
        }

        [HttpPost("/Pedido_Produto/Create")]
        public IActionResult Post([FromBody] Pedido_ProdutoModel pedido_ProdutoModel,
            [FromServices] AppDbContext context)
        {
            context.Pedido_Produto!.Add(pedido_ProdutoModel);
            context.SaveChanges();
            return Created($"/{pedido_ProdutoModel.PedidoProdutoId}", pedido_ProdutoModel);
        }

        [HttpPut("/Pedido_Produto/Edit/{id:int}")]
        public IActionResult Put([FromRoute] int id, 
            [FromBody] Pedido_ProdutoModel pedidoProdutoModel,[FromServices] AppDbContext context)
        {
            var model = context.Pedido_Produto!.Include(p => p.Categoria).FirstOrDefault(x => x.PedidoProdutoId == id);
            if (model == null) {
                return NotFound();
            }

            model.Nome = pedidoProdutoModel.Nome;
            model.Descricao = pedidoProdutoModel.Descricao;
            model.Preco = pedidoProdutoModel.Preco;
            model.CategoriaId = pedidoProdutoModel.CategoriaId;

            context.Pedido_Produto!.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/Pedido_Produto/Delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var model = context.Pedido_Produto!.FirstOrDefault(x => x.PedidoProdutoId == id);
            if (model == null) {
                return NotFound();
            }

            context.Pedido_Produto!.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
}