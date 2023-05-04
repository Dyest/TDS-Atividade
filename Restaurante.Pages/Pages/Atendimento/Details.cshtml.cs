using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Restaurante.Pages.Pages.Atendimento
{
    public class Details : PageModel
    {
        [BindProperty]
        public AtendimentoModel AtendimentoModel { get; set; } = new();
        public List<Pedido_ProdutoModel> Pedido_ProdutoList { get; set; } = new();
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
            var urlPedido = $"http://localhost:5085/Pedido_Produto/{id}";
            var responsePedido = await httpClientPedido.GetAsync(urlPedido);

            if (!responsePedido.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var contentPedido = await responsePedido.Content.ReadAsStringAsync();
            var pedido_ProdutoList = JsonConvert.DeserializeObject<List<Pedido_ProdutoModel>>(contentPedido);
            Pedido_ProdutoList = pedido_ProdutoList!;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id){
            if(!ModelState.IsValid){
                return Page();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5085/Mesa/Edit/{id}";
            var mesaJson = JsonConvert.SerializeObject(MesaModel);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(mesaJson, Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode){
                return RedirectToPage("/Mesa/Index");
            }
            var httpClientAtendimento = new HttpClient();
            var urlAtendimento = $"http://localhost:5085/Atendimento/Edit/{id}";
            var atendimentoJson = JsonConvert.SerializeObject(AtendimentoModel);

            var requestMessageAtendimento = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessageAtendimento.Content = new StringContent(atendimentoJson, Encoding.UTF8, "application/json");
            var responseAtendimento = await httpClient.SendAsync(requestMessageAtendimento);

            if(!response.IsSuccessStatusCode){
                return RedirectToPage("/Atendimento/Index");
            }
            
            return Page();
        }

    }
}