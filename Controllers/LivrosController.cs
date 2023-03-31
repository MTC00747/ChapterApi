using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chapter.WebApi.Repositories;
using Chapter.WebApi.Models;


namespace Chapter.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivrosRepository _livrorepository; 
        public LivrosController(LivrosRepository livroRepository) 
        {
                _livrorepository = livroRepository;
        }
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_livrorepository.Listar()); // ele chama o repository e dentro do repository ele chama o método Listar
        }

        //get  /api/lirvos/{1}

        [HttpGet("{id}")] // Busca Pelo Id Passado
        public IActionResult BuscaPorId(int id)
        {
            Livro livro = new Livro(); // Instancia a classe Livro  

           livro = _livrorepository.BuscaPoId(id); // Atribui a variavel _livroRepository
             // Instancia  livro 
            if(livro == null) //Se livro for diferente de nulo , retorna o livro, se for nulo , retorna NotFound
            {
                    return NotFound(); 
            }
            return Ok(livro);
        }

        //put /api/livros/{id}
        //Recebe a informação do livro 
        //Atualiza o corpo da requisição 

        [HttpPut ("{id}")]

        public IActionResult Atualizar(int id, Livro livro)
        {
            _livrorepository.Atualizar (id, livro);
            return StatusCode(204);

        }

        //Recebe A info de Livro que deseja Salvar do corpo da Requisição 

        [HttpPost]

        public IActionResult Cadastrar (Livro livro)
        {
            _livrorepository.Cadastrar(livro);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]

        public IActionResult Deletar(int id)
        {
            try{
            _livrorepository.Deletar(id);
            return StatusCode(204);
            }
            catch(SystemException)
            {
                return BadRequest();
            }
        
        }

    }
}