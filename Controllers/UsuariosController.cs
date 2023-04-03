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
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_IUsuarioRepository.Listar());
        }

        //Post
        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            Usuario UsuarioBuscado = _IUsuarioRepository.Login(usuario.Email, usuario.Senha);
            if (UsuarioBuscado == null)
            {
                return NotFound("Email ou Senha invalido");

            }
            //Se o usuario foe encontrado, Segue coma criação do token

            //Define os dados que serão fornecidos no token - payload

            var claims = new[]
            {
            //Armazena na claim o email usario autenticado
            new Claim (JwtRegisteredClaimNames.Email, UsuarioBuscado.Email),

            //Armazena na claim o id do usuario autenticado 

            new Claim (JwtRegisteredClaimNames.Jti, UsuarioBuscado.id.ToString()),
        };

            // define a chave de acesso ao token 
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapterapi-chave-autenticacao"));


            //Define as credencias do token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "chapterapi.webapi", //emissor do token
                audience: "chapterapi.webapi", // destinatario do token
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), //Tempo de expiração
                signingCredentials: creds
            );

            //Retorna ok com o token
            return Ok(
                new { token = new JwtSecurityTokenHandler().WriteToken(token) }
            );
        }

        //get -> /api/usuarios/{id}
        [HttpGet("{id}")]

        public IActionResult BuscarPorId(int id)
        {
            Usuario usuario = _IUsuarioRepository.BuscarPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
        [Authorize]
        //put -> /api/usuario/{id}
        //Atualiza
        [HttpPut("{id}")]

        public IActionResult Atualizar(int id, Usuario usuario)
        {
            _IUsuarioRepository.Atualizar(id, usuario);
            return StatusCode(204);
        }

        //Delete -> /api/usuarios/{id}
        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult Deletar(int id)
        {
            try
            {
                _IUsuarioRepository.Deletar(id);
                return StatusCode(204);

            }
            catch (SystemException)
            {
                return BadRequest();
            }
        }
    }
}