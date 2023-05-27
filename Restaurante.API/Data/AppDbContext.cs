using Microsoft.EntityFrameworkCore;
using Restaurante.API.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Restaurante.API.Data{
    public class AppDbContext : DbContext
    {
        public DbSet<GarconModel>? Garcon {get; set;}
        public DbSet<MesaModel>? Mesa {get; set;}
        public DbSet<ProdutoModel>? Produto {get; set;}
        public DbSet<CategoriaModel>? Categoria {get; set;}
        public DbSet<AtendimentoModel>? Atendimento {get; set;}
        public DbSet<PedidoModel>? Pedido {get; set;}
        public DbSet<PedidoProdutoModel>? PedidoProduto {get; set;}

        static readonly string connectionString = "Server=db_mysql;Port=3306;Database=restaurante;Uid=root;Pwd=restaurante;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseMySql(connectionString,
                                    ServerVersion.AutoDetect(connectionString));
        }

            
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<GarconModel>().ToTable("Garcon");
            modelBuilder.Entity<MesaModel>().ToTable("Mesa");
            modelBuilder.Entity<ProdutoModel>().ToTable("Produto");
            modelBuilder.Entity<CategoriaModel>().ToTable("Categoria");
            modelBuilder.Entity<AtendimentoModel>().ToTable("Atendimento");
            modelBuilder.Entity<PedidoModel>().ToTable("Pedido");
            modelBuilder.Entity<PedidoProdutoModel>().ToTable("Pedido_Produto");
        }
    }
}