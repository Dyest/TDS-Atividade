using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.API.Data;
using Restaurante.API.Models;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AtendimentoController : ControllerBase
    {
        [HttpGet]
        [Route("/Atendimento")]
        public IActionResult Get([FromServices] AppDbContext context){
            var atendimentos = context.Atendimento!.Include(p => p.Mesa).ToList();
            return Ok(atendimentos);
        }
        
        [HttpGet("/Atendimento/Details/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var atendimentoModel = context.Atendimento!.Include(p => p.Mesa).FirstOrDefault(x => x.AtendimentoId == id);
            if (atendimentoModel == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                AtendimentoId = atendimentoModel.AtendimentoId,
                AtendimentoFechado = atendimentoModel.AtendimentoFechado,
                DataSaida = atendimentoModel.DataSaida,
                MesaId = atendimentoModel.MesaId,
                Mesa = new
                {
                    MesaId = atendimentoModel.Mesa!.MesaId,
                    Numero = atendimentoModel.Mesa.Numero,
                    HoraAbertura = atendimentoModel.Mesa.HoraAbertura
                }
            });
        }

        [HttpPost("/Atendimento/Create")]
        public IActionResult Post([FromBody] AtendimentoModel atendimentoModel, [FromServices] AppDbContext context)
        {
            var atendimentoToAdd = context.Mesa!.FirstOrDefault(x => x.MesaId == atendimentoModel.MesaId);

            if (atendimentoToAdd == null) {
                return NotFound();
            }

            if(atendimentoToAdd!.Status == false){
                context.Atendimento!.Add(atendimentoModel);
                atendimentoToAdd.Status = true;
                atendimentoToAdd.HoraAbertura = DateTime.Now.AddHours(1.50);
                atendimentoModel.DataCriacao = DateTime.Now;
                context.SaveChanges();
                return Created($"/{atendimentoModel.AtendimentoId}", atendimentoModel);
            }
            else{
                return RedirectToPage("/Atendimento/Create");
            }
            
        }

        [HttpPut("/Atendimento/Edit/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] AtendimentoModel atendimentoModel,
        [FromServices] AppDbContext context)
        {
            var model = context.Atendimento!.Include(p => p.Mesa).FirstOrDefault(x => x.AtendimentoId == id);
            if (model == null) {
                return NotFound();
            }

            //var antigaMesaId = model.MesaId;
            var antigaMesa = context.Mesa!.FirstOrDefault(x => x.MesaId == atendimentoModel.MesaId);
            antigaMesa!.Status = true;
            antigaMesa!.HoraAbertura = DateTime.Now.AddHours(1);
            
            model.MesaId = atendimentoModel.MesaId;
            model.Mesa!.Status = false;
            model.Mesa!.HoraAbertura = null;

            context.Atendimento!.Update(model);

            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/Atendimento/Delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var toDeleteAtendimento = context.Atendimento!.Include(p => p.Mesa).FirstOrDefault(x => x.AtendimentoId == id);

            if (toDeleteAtendimento == null) {
                return NotFound();
            }

            toDeleteAtendimento.Mesa!.Status = false;
            toDeleteAtendimento.Mesa.HoraAbertura = null;
            context.Atendimento!.Remove(toDeleteAtendimento);
            context.SaveChanges();
            
            return Ok(toDeleteAtendimento);
        }
    }
}