
namespace Chapter.WebApi.Models
{
    public class Usuario
    {
        public int id {get;set;} //Id 
        public string Email { get; set; } //Email
        public string Senha { get; set; } //Senha

        public string Tipo {get; set;} // Tipo de Usúario 

        public string Nome {get; set;}
    }
}