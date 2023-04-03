using Chapter.WebApi.Contexts;
using Chapter.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using Chapter.WebApi.Interfaces;



namespace Chapter.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ChapterContext _context = new ChapterContext();

        public UsuarioRepository(ChapterContext context)
        {
            _context = context;
        }

        public Usuario Login(String email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

        public List<Usuario> Listar() ///Método para listar todos os Usuarios (GET);
        {
            return _context.Usuarios.ToList();
        }

        public void Cadastrar(Usuario usuario) // Méotod Para cadastrar um usuario (POST)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario BuscarPorId(int id) // Método Para buscar pelo Id
        {
            return _context.Usuarios.Find(id);
        }

        public void Atualizar(int id, Usuario usuario) // Méttodo para editar (PUT)
        {
            Usuario UsuarioBuscado = new Usuario();
            UsuarioBuscado = _context.Usuarios.Find(id);
            
            if (UsuarioBuscado != null)
            {
                UsuarioBuscado.Email = usuario.Email;
                UsuarioBuscado.Senha = usuario.Senha;
            }
            _context.Usuarios.Update(UsuarioBuscado);
            _context.SaveChanges();
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