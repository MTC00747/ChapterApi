using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chapter.WebApi.Repositories;
using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using Chapter.WebApi.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Chapter.WebApi.Controllers
{   [Produces ("Application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _IusuarioRepository;  //Deixando uma variavel publica privada, Utilizando a interface IUsuarioRepository e atribuindo ela a uma variavel 
        public LoginController(IUsuarioRepository iUsarioRepository) // 
        {
            _IusuarioRepository = iUsarioRepository; //Transformando a variavel privada em publica
        }

    
         //Post
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            Usuario UsuarioBuscado = _IusuarioRepository.Login(login.Email, login.Senha);
            if (UsuarioBuscado == null)
            {
                return NotFound(new{msg ="Email ou Senha invalido"});

            }
            //Se o usuario foe encontrado, Segue coma criação do token

            //Define os dados que serão fornecidos no token - payload

            var claims = new[]
            {
            //Armazena na claim o email usario autenticado
            new Claim (JwtRegisteredClaimNames.Email, UsuarioBuscado.Email),

            //Armazena na claim o id do usuario autenticado 

            new Claim (JwtRegisteredClaimNames.Jti, UsuarioBuscado.id.ToString()),

            //Armazena o tipo de usuário
            new Claim (ClaimTypes.Role, UsuarioBuscado.Tipo)
        };

            // define a chave de acesso ao token 
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapterapi-chave-autenticacao"));


            //Define as credencias do token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "chapterapi.webapi", //emissor do token
                audience: "chapterapi.webapi", // destinatario do token
                claims: claims, //Aray com as claims
                expires: DateTime.Now.AddMinutes(30), //Tempo de expiração
                signingCredentials: creds
            );

            //Retorna ok com o token
            return Ok(
                new { token = new JwtSecurityTokenHandler().WriteToken(token) }
            );
        }


    }
}