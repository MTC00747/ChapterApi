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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivrosRepository _Ilivrorepository; // protegendo os dados com método privado 
        public LivrosController(LivrosRepository IlivroRepository)
        {
            _Ilivrorepository = IlivroRepository; // armazenando dados de livro repositry no método privado
        }
        [HttpGet]
        public IActionResult Listar()
        {
            try // Tratamento de erros;
            {
                return Ok(_Ilivrorepository.Listar()); // ele chama o repository e dentro do repository ele chama o método Listar
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //get  /api/lirvos/{1}

        [HttpGet("{id}")] // Busca Pelo Id Passado
        public IActionResult BuscarPorId(int id)
        {
            Livro livro = new Livro(); // Instancia a classe Livro  

            livro = _Ilivrorepository.BuscarPorId(id); // Atribui a variavel _livroRepository
                                                       // Instancia  livro 
            if (livro == null) //Se livro for diferente de nulo , retorna o livro, se for nulo , retorna NotFound
            {
                return NotFound();
            }
            return Ok(livro);
        }

        //put /api/livros/{id}
        //Recebe a informação do livro 
        //Atualiza o corpo da requisição 

        [HttpPut("{id}")]

        public IActionResult Atualizar(int id, Livro livro)
        {
            try
            {
                _Ilivrorepository.Atualizar(id, livro);
                return StatusCode(204);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        //Recebe A info de Livro que deseja Salvar do corpo da Requisição 

        [HttpPost]

        public IActionResult Cadastrar(Livro livro)
        {
            try
            {
                _Ilivrorepository.Cadastrar(livro);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpDelete("{id}")]

        public IActionResult Deletar(int id)
        {
            try
            {
                _Ilivrorepository.Deletar(id);
                return StatusCode(204);
            }
            catch (SystemException)
            {
                return BadRequest();
            }

        }

    }
}