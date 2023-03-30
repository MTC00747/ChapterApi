using Chapter.WebApi.Contexts;
using Chapter.WebApi.Models;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter.WebApi.Repositories
{
    public class LivrosRepository
    {
        private readonly ChapterContext _context;
        public LivrosRepository(ChapterContext context) 
        {
            _context = context;

        }
        public List<Livro>  Listar()
        {
            return  _context.Livros.ToList(); // Controller se comunica com o Respository e Retorna uma lista com todos os livros
        }

        public Livro BuscaPoId(int id)
        {
            //select where id=id
            return _context.Livros.Find(id);
        }

        public void Atualizar(int id, Livro livro) 
        {
            Livro LivroBuscado = new Livro();
            LivroBuscado = _context.Livros.Find(id);

            if(LivroBuscado != null)
            {
                LivroBuscado.Titulo = livro.Titulo; // Ele vai buscar o livro.titulo e vai trocar pelo novo titulo
                LivroBuscado.QuantidadePaginas = livro.QuantidadePaginas; // Ele vai buscar o livro.QuantidadeDePaginas e vai trocar pelo novo titulo
                LivroBuscado.Disponivel = livro.Disponivel;
            }

            _context.Livros.Update(LivroBuscado);

            _context.SaveChanges();

        }

        public void Cadastrar(Livro livro)
        {
            _context.Livros.Add(livro); 
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            Livro LivroBuscado = new Livro();

            LivroBuscado = _context.Livros.Find(id);

            _context.Livros.Remove(LivroBuscado);
            _context.SaveChanges();
        }


    }
}