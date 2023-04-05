using Chapter.WebApi.Contexts;
using Chapter.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Controllers;



namespace Chapter.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ChapterContext _context = new ChapterContext();

        public UsuarioRepository(ChapterContext context)
        {
            _context = context;
        }

        public Usuario Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha); //FirstOrDefault faz a busca  e verifica se é igual
        }
        public List<Usuario> Listar() => _context.Usuarios.ToList();//Método para listar todos os Usuarios (GET);
        public void Cadastrar(Usuario usuario) // Méotod Para cadastrar um usuario (POST)
        {
            _context.Usuarios.Add(usuario); //Armazana os dados no banco de dados na tabela livros e depois acionia os dados
            _context.SaveChanges();  // Salva as informações 
        }

        public Usuario BuscarPorId(int id) => _context.Usuarios.Find(id); // Método Para buscar pelo Id , ele busca o usuario na tabela Usuarios pelo Id
        public void Atualizar(int id, Usuario usuario) // Méttodo para editar (PUT)
        {
            Usuario UsuarioBuscado = new Usuario(); // Instancia o Objeto UsuarioBuscado do tipo Usuario
            UsuarioBuscado = _context.Usuarios.Find(id); //Armazena em UsuarioBuscado Dados do id selecionado

            if (UsuarioBuscado != null) // Se UsuarioBuscado for diferente de Null 
            {
                UsuarioBuscado.Email = usuario.Email; // armazena  usuario.Email e usuario.Senha e sobrepõe o usuarioBuscado
                UsuarioBuscado.Senha = usuario.Senha;
                UsuarioBuscado.Tipo = usuario.Tipo;

                _context.Usuarios.Update(UsuarioBuscado); //Vai até a tablea usuarios e dá um Updtade no Usuario buscado 
                _context.SaveChanges(); //Salba as alterações
            }

        }

        public void Deletar(int id)
        {
            try
            {
                Usuario UsuarioBuscado = new Usuario();
                UsuarioBuscado = _context.Usuarios.Find(id);
                _context.Usuarios.Remove(UsuarioBuscado);
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}