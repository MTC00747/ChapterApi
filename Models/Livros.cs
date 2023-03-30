using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter.WebApi.Models

{
    public class Livro //Entidade Livro, Abstração do DataBase
    {
        public int Id { get; set; }
        public string Titulo{ get; set; }

        public int QuantidadePaginas { get; set; }

        public bool Disponivel { get; set; }


    }
}