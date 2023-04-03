
using Chapter.WebApi.Models;


namespace Chapter.WebApi.Interfaces
{
    public interface IlivroRepository
    {
        List<Livro> Listar(); //Método Get 

        public Livro BuscarPorId(int id);


        public void Cadastrar(Livro livro);

        public void Atualizar(int Id, Livro livro); // Método Put

        public void Deletar(int id); //Método deletar




    }
}