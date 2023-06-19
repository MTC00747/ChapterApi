using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chapter.WebApi.Repositories;

namespace Chapter.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosRepository _PedidosRepository;

        public PedidosController(PedidosRepository PedidosRepository)
        {
           _PedidosRepository =  PedidosRepository;
           
        }

        [HttpGet]

        public IActionResult Listar()
        {
            return Ok(_PedidosRepository.Listar());
        }
    }
}