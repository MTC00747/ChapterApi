using Chapter.WebApi.Models;
namespace Chapter.WebApi.Interfaces

{
    public interface IUsuarioRepository
    {
         public Usuario Login(string email , string senha); // Metodo Logim Post

         public List<Usuario> Listar(); 

         public void Cadastrar(Usuario usuario);

         public Usuario BuscarPorId(int id);

         public void Atualizar(int id, Usuario usuario) ;

          public void Deletar(int id);
    }
}