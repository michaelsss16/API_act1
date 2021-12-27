using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class appContext : DbContext
    {
        public appContext(DbContextOptions<appContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos{ get; set; }
        public DbSet<Venda> Vendas{ get; set; }
        public DbSet<ProdutoVendaDTO> ProdutoVendaDTOs{ get; set; }
        public DbSet<Usuario> Usuarios{ get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Venda)
                .WithMany(b => b.ListaProdutos)
                .HasForeignKey(p => p.VendaForeignKey);
        }
        */
    }
}
