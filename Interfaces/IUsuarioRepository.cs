using Chapter.WebApi.Models;
namespace Chapter.WebApi.Interfaces

{
    public interface IUsuarioRepository
    {
        public Usuario Login(string email, string senha); // Metodo Logim Post

        public List<Usuario> Listar();
        public Usuario BuscarPorId(int id);
        public void Cadastrar(Usuario usuario); // Vai cadastrar um Objeto do Tipo Usuario
        public void Atualizar(int id, Usuario usuario); //Recebe Um id de um usuário especifico e atualiza o objeto do tipo usuário

        public void Deletar(int id);
    }
}