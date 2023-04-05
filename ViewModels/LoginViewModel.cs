using System.ComponentModel.DataAnnotations;

namespace Chapter.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "EmaiL obrigatório!")]//o atributo que vem abaixo é obrigatorio
        public string Email { get; set; }

         [Required(ErrorMessage = "Senha obrigatória!")]
        public string Senha { get; set; }
    }
}