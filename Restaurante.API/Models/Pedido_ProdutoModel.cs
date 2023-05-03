using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.API.Models
{
    public class Pedido_ProdutoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PedidoProdutoId { get; set; }

        [ForeignKey("Pedido")]
        public int PedidoId { get; set; }
        public PedidoModel? Pedido { get; set; }

        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public ProdutoModel? Produto { get; set; }
        public double Quantidade { get; set; }
    }
}