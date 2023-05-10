using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;


namespace Restaurante.Pages.Pages.Atendimento
{
    public class Details : PageModel
    {
        [BindProperty]
        public AtendimentoModel AtendimentoModel { get; set; } = new();
        public List<PedidoProdutoModel> PedidoProdutoList { get; set; } = new();
        public MesaModel MesaModel { get; set; } = new();
        public Details(){
        }

        public async Task<IActionResult> OnGetAsync(int? id){
            if (id == null)
            {
                return NotFound();
            }

            var httpClientAtendimento = new HttpClient();
            var urlAtendimento = $"http://localhost:5085/Atendimento/Details/{id}";
            var responseAtendimento = await httpClientAtendimento.GetAsync(urlAtendimento);

            if (!responseAtendimento.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var contentAtendimento = await responseAtendimento.Content.ReadAsStringAsync();
            AtendimentoModel = JsonConvert.DeserializeObject<AtendimentoModel>(contentAtendimento)!;
            
            var httpClientPedido = new HttpClient();
            var urlPedido = $"http://localhost:5085/PedidoProduto/{id}";
            var responsePedido = await httpClientPedido.GetAsync(urlPedido);

            if (!responsePedido.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var contentPedido = await responsePedido.Content.ReadAsStringAsync();
            var pedidoProdutoList = JsonConvert.DeserializeObject<List<PedidoProdutoModel>>(contentPedido);
            PedidoProdutoList = pedidoProdutoList!;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id){
            if(!ModelState.IsValid){
                return Page();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5085/PedidoProduto/Edit/{id}";
            var atendimentoJson = JsonConvert.SerializeObject(AtendimentoModel);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(atendimentoJson, Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(requestMessage);
            
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Atendimento/Details", id);
            } 
            else 
            {
                TempData["Aviso_Abrir_Atendimento"] = "A mesa que você esta tentando abrir o atendimento já está ocupada!!";
                return Page();
            }
        }

    }
}