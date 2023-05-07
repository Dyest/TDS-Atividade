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
        public PedidoModel PedidoModel { get; set; } = new();

        [BindProperty]
        public Pedido_ProdutoModel Pedido_ProdutoModel { get; set; } = new();
        public List<GarconModel> GarconList { get; set; } = new();
        public List<ProdutoModel> ProdutoList { get; set; } = new();
        public List<Pedido_ProdutoModel> Pedido_ProdutoList { get; set; } = new();
        
        public Create(){
            
        }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var httpClient = new HttpClient();
        var url = "http://localhost:5085/Pedido/Create";
        var pedidoJson = JsonConvert.SerializeObject(PedidoModel);
        var content = new StringContent(pedidoJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var pedido = JsonConvert.DeserializeObject<PedidoModel>(await response.Content.ReadAsStringAsync());
            var pedidoProduto = new Pedido_ProdutoModel()
            {
                PedidoId = pedido.PedidoId,
                ProdutoId = Pedido_ProdutoModel.ProdutoId,
                Quantidade = Pedido_ProdutoModel.Quantidade
            };

            var pedidoProdutoJson = JsonConvert.SerializeObject(pedidoProduto);
            var pedidoProdutoContent = new StringContent(pedidoProdutoJson, Encoding.UTF8, "application/json");
            var pedidoProdutoResponse = await httpClient.PostAsync("http://localhost:5085/Pedido_Produto/Create", pedidoProdutoContent);

            if (pedidoProdutoResponse.IsSuccessStatusCode)
            {
                return RedirectToPage("/Atendimento/Index");
            }
        }

        return Page();
    }
            public async Task<IActionResult> OnGetAsync(int? id){
            var httpClientGarcon = new HttpClient();
            var urlGarcon = "http://localhost:5085/Garcon";
            var requestMessageGarcon = new HttpRequestMessage(HttpMethod.Get, urlGarcon);
            var responseGarcon = await httpClientGarcon.SendAsync(requestMessageGarcon);
            var contentGarcon = await responseGarcon.Content.ReadAsStringAsync();

            GarconList = JsonConvert.DeserializeObject<List<GarconModel>>(contentGarcon)!;

            var httpClientProduto = new HttpClient();
            var urlProduto = "http://localhost:5085/Produto";
            var requestMessageProduto  = new HttpRequestMessage(HttpMethod.Get, urlProduto);
            var responseProduto = await httpClientProduto.SendAsync(requestMessageProduto);
            var contentProduto = await responseProduto.Content.ReadAsStringAsync();

            ProdutoList = JsonConvert.DeserializeObject<List<ProdutoModel>>(contentProduto)!;

            var httpClientPedido_Produto = new HttpClient();
            var urlPedido_Produto = "http://localhost:5085/Pedido_Produto";
            var requestMessagePedido_Produto  = new HttpRequestMessage(HttpMethod.Get, urlPedido_Produto);
            var responsePedido_Produto = await httpClientPedido_Produto.SendAsync(requestMessagePedido_Produto);
            var contentPedido_Produto = await responsePedido_Produto.Content.ReadAsStringAsync();

            Pedido_ProdutoList = JsonConvert.DeserializeObject<List<Pedido_ProdutoModel>>(contentPedido_Produto)!;

            return Page();
        }

        
    }
}