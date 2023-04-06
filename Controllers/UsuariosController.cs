using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Chapter.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _IUsuarioRepository;

        public UsuariosController(UsuarioRepository usuarioRepository)
        {
            _IUsuarioRepository = usuarioRepository;
        }

        //Get -> /api/usuarios
        [Authorize(Roles = "1")] //Roles é o pareametro para dizer se o user é adm ou não
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_IUsuarioRepository.Listar());
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "1")] //Roles é o pareametro para dizer se o user é adm ou não
        [HttpPost, Route("Cadastrar")] //Configurando Rota
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _IUsuarioRepository.Cadastrar(usuario); // vai na interface e acessa o método cadastrar e cadastra um objeto do tipo usaurio
                return StatusCode(201);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //get -> /api/usuarios/{id}
        [Authorize(Roles = "1")] //Roles é o pareametro para dizer se o user é adm ou não
        [HttpGet("{id}")]

        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario usuarioBuscado = _IUsuarioRepository.BuscarPorId(id); //Instancia do Resultado do método Busca Por id
                if (usuarioBuscado == null)//Verifica se é nulo
                {
                    return NotFound();
                }
                return Ok(usuarioBuscado);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "1")] //Roles é o pareametro para dizer se o user é adm ou não
        //put -> /api/usuario/{id}
        //Atualiza
        [HttpPut("{id}")]

        public IActionResult Atualizar(int id, Usuario usuario)
        {
            try
            {
                _IUsuarioRepository.Atualizar(id, usuario); //acessa o método atualizar da interface , recebe um id e usuario
                return StatusCode(204);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //Delete -> /api/usuarios/{id}
        [Authorize(Roles = "1")] //Roles é o pareametro para dizer se o user é adm ou não
        [HttpDelete("{id}")]

        public IActionResult Deletar(int id)
        {
            try
            {
                _IUsuarioRepository.Deletar(id); // Rece um id , acesssa o método deletar da interface e deleta o id passado
                return StatusCode(204);

            }
            catch (SystemException)
            {
                return BadRequest();
            }
        }
    }
}