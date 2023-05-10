using Restaurante.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Restaurante.Pages.Pages.Pedido
{
    public class Create : PageModel
    {
        public AtendimentoModel AtendimentoModel { get; set; } = new();
        [BindProperty]
        public PedidoModel PedidoModel { get; set; } = new();

        [BindProperty]
        public PedidoProdutoModel PedidoProdutoModel { get; set; } = new();
        public List<GarconModel> GarconList { get; set; } = new();
        public List<ProdutoModel> ProdutoList { get; set; } = new();
        
        public Create(){
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
            
            // Pedido_ProdutoList = await _context.Pedido_Produto!.ToListAsync();
            
            var httpClientGarcon = new HttpClient();
            var urlGarcon = "http://localhost:5085/Garcon";
            var requestMessageGarcon = new HttpRequestMessage(HttpMethod.Get, urlGarcon);
            var responseGarcon = await httpClientGarcon.SendAsync(requestMessageGarcon);
            var contentGarcon = await responseGarcon.Content.ReadAsStringAsync();

            GarconList = JsonConvert.DeserializeObject<List<GarconModel>>(contentGarcon)!;

            var httpClientProduto = new HttpClient();
            var urlProduto = "http://localhost:5085/Produto";
            var requestMessageProduto = new HttpRequestMessage(HttpMethod.Get, urlProduto);
            var responseProduto = await httpClientProduto.SendAsync(requestMessageProduto);
            var contentProduto = await responseProduto.Content.ReadAsStringAsync();

            ProdutoList = JsonConvert.DeserializeObject<List<ProdutoModel>>(contentProduto)!;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id){
            if(!ModelState.IsValid){
                return RedirectToAction("/Pedido/Create/"+id);
            }
            
            var httpClientPedido = new HttpClient();
            var urlPedido = "http://localhost:5085/Pedido/Create";
            var produtoJsonPedido = JsonConvert.SerializeObject(PedidoModel);
            var contentPedido = new StringContent(produtoJsonPedido, Encoding.UTF8, "application/json");
            var responsePedido = await httpClientPedido.PostAsync(urlPedido, contentPedido);

            if(responsePedido.IsSuccessStatusCode){
                string responseContent = await responsePedido.Content.ReadAsStringAsync();
                PedidoModel pedido = JsonConvert.DeserializeObject<PedidoModel>(responseContent)!;
                int pedidoId = pedido.PedidoId;

                var httpClientPedidoProduto = new HttpClient();
                var urlPedidoProduto = $"http://localhost:5085/PedidoProduto/Create/{pedidoId}";
                var produtoJsonPedidoProduto = JsonConvert.SerializeObject(PedidoProdutoModel);

                var contentPedidoProduto = new StringContent(produtoJsonPedidoProduto, Encoding.UTF8, "application/json");
                var responsePedidoProduto = await httpClientPedidoProduto.PostAsync(urlPedidoProduto, contentPedidoProduto);
                if(responsePedidoProduto.IsSuccessStatusCode){
                    return Redirect("/Atendimento/Details/"+id);
                }
                else{
                    return Redirect("/");
                }
            } else {
                return Redirect("/Atendimento");
            }
            
        }
    }
}