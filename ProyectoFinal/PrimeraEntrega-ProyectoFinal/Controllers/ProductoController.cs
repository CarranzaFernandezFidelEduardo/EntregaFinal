using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Repositories;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        ProductoRepository productoRepository;

        public ProductoController()
        {
            productoRepository = new ProductoRepository();
        }

        [HttpGet("{idUsuario}")]
        public ActionResult ObtenerProducto([FromQuery] int IdUsuario)
        {
            var result = productoRepository.TraerProducto(IdUsuario);
            return Ok(result);
        }

        [HttpGet("ObtenerProductos")]
        public ActionResult ObtenerProductos()
        {
            var result = productoRepository.TraerProductos();
            return Ok(result);
        }

        [HttpPost]
        public void CrearProducto([FromQuery] Producto producto)
        {

            productoRepository.CrearProducto(producto);
        }

        [HttpPut]
        public void ModificarProducto([FromQuery] Producto producto)
        {
            productoRepository.ModificarProducto(producto);

        }

        [HttpDelete("{idProducto}")]
        public void EliminarProducto([FromQuery] int idProducto)
        {
            productoRepository.EliminarProducto(idProducto);
        }
    }
}
