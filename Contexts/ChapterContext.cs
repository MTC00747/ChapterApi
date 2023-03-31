using Chapter.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Chapter.WebApi.Contexts
{
    public class ChapterContext : DbContext //Herda uma classe do sistema "DbContext"
    {

        public ChapterContext()
        {

        }

        public ChapterContext(DbContextOptions<ChapterContext> options) : base(options) 
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            if (!optionsBuilder.IsConfigured)
            {
                // String de Conexao de acordo com a máquina utilizada 
                optionsBuilder.UseSqlServer("Server=Localhost\\SQLEXPRESS;"
                + "Database=Chapter;Trusted_Connection=True;");

                //Exemplo de conexão de string
                // User ID=sa;Password = SuaSenha; Server=localhost; Database = Chapter; 
                //+ Trusted_Connection=False;

            }
        //Teste
           

        }
        public DbSet<Livro> Livros {get; set;} // add entidade Livro
        public DbSet<Usuario> Usuarios{get; set;} // add entidade usuario
    }
}