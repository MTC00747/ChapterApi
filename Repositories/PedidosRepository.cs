using Chapter.WebApi.Contexts;
using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Chapter.WebApi.Repositories
{
    public class PedidosRepository : iPedidos
    {
        private readonly ChapterContext _context;

        public PedidosRepository(ChapterContext context)
        {
            _context = context;
        }

        public List<Pedidos> Listar(string nomeUsuario)
        {
            return _context.Pedidos.Join(_context.Usuarios,
            pedido => pedido.Usuario_id,
            usuario => usuario,
            ).
            ToList();
        }
    }
}