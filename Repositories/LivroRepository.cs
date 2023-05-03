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
            return _context.Livros.ToList(); // Controller se comunica com o Respository e Retorna uma lista com todos os elementos da tabela livros
        }

        public Livro BuscarPorId(int id) => _context.Livros.Find(id); // Acha pelo Id
        
        //select where id=id

        public void Atualizar(int id, Livro livro)
        {
            Livro LivroBuscado = new Livro();
            LivroBuscado = _context.Livros.Find(id);

            if (LivroBuscado != null)
            {
                LivroBuscado.Titulo = livro.Titulo; // Ele vai buscar o livro.titulo e vai trocar pelo novo titulo
                LivroBuscado.QuantidadePaginas = livro.QuantidadePaginas; // Ele vai buscar o livro.QuantidadeDePaginas e vai trocar pelo nova quantidade de pages
                LivroBuscado.Disponivel = livro.Disponivel;
                LivroBuscado.Preco = livro.Preco;
                LivroBuscado.Classificacao = livro.Classificacao;
                LivroBuscado.Categoria = livro.Categoria;
                LivroBuscado.Imagem = livro.Imagem;
            }

            _context.Livros.Update(LivroBuscado);

            _context.SaveChanges(); //Salva a mudança feitas

        }

        public void Cadastrar(Livro livro)
        {
            _context.Livros.Add(livro);//Acessa o Database, vai na tabela livros e cadastra o objeto do tipo livro. 
            _context.SaveChanges(); // Salva as mudanças Feitas
        }

        public void Deletar(int id)
        {
            Livro LivroBuscado = new Livro(); //Instancia do objeto livro 

            LivroBuscado = _context.Livros.Find(id); // Armazena em LivroBuscado as infromações da tabela Livros

            _context.Livros.Remove(LivroBuscado);
            _context.SaveChanges();
        }

    }
}