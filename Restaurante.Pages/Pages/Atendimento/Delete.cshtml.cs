using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using System.Net;


namespace Restaurante.Pages.Pages.Atendimento
{
  public class Delete : PageModel
    {
        [BindProperty]
        public AtendimentoModel AtendimentoModel { get; set; } = new();
        public List<PedidoProdutoModel> PedidoProdutoList { get; set; } = new();
        public Delete(){
        }

        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null){
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://webapi/Atendimento/Details/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            AtendimentoModel = JsonConvert.DeserializeObject<AtendimentoModel>(content)!;
            
            var httpClientPedido = new HttpClient();
            var urlPedido = $"http://webapi/PedidoProduto/{id}";
            var responsePedido = await httpClientPedido.GetAsync(urlPedido);

            if (!responsePedido.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var contentPedido = await responsePedido.Content.ReadAsStringAsync();
            var pedidoProdutoList = JsonConvert.DeserializeObject<List<PedidoProdutoModel>>(contentPedido);

            PedidoProdutoList = pedidoProdutoList!;

            if(PedidoProdutoList is not null){
                TempData["Aviso_Excluir"] = "Esse atendimento não pode ser excluido, ele tem pedidos cadastrados!!!";
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id){
            var httpClient = new HttpClient();
            var url = $"http://webapi/Atendimento/Delete/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            var response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Atendimento/Index");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
    }
}