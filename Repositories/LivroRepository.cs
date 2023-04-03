using Chapter.WebApi.Contexts;
using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter.WebApi.Repositories
{
    public class LivrosRepository : IlivroRepository
    {
        private readonly ChapterContext _context; // Classe para estar Protegendo os dados, deixando os dados apenas para leitura e privados
        public LivrosRepository(ChapterContext context) // Método construtor , toda vez que isntanciarmos um Objeto da classe Livro repository, vamos depender do DataBase ( Context)
        {
            _context = context; //Salvando os dados quem vem do context Na instancia _context que é privada

        }
        public List<Livro> Listar()
        {
            return _context.Livros.ToList(); // Controller se comunica com o Respository e Retorna uma lista com todos os livros
        }

        public Livro BuscarPorId(int id) => (_context.Livros.Find(id)); //select where id=id

        public void Atualizar(int id, Livro livro)
        {
            Livro LivroBuscado = new Livro();
            LivroBuscado = _context.Livros.Find(id);

            if (LivroBuscado != null)
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