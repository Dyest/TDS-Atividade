using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Pages.Models;
using System.Net;

namespace Restaurante.Pages.Pages
{
    public class Index : PageModel
    {

        public List<PedidoModel>PedidoList { get; set; } = new();
        public List<GarconModel>GarconList { get; set; } = new();
        public List<PedidoView>PedidoViewList { get; set; } = new();
        public List<Pedido_ProdutoModel>PedidoProdutoList { get; set; }
        
        public Index(){
            //_context = context;//
        }
        public PedidoModel pedidoModel { get; set; } = new();
       
        public async Task<IActionResult> OnGetAsync(){
            
            return Page();
        }

    }
}