using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Repositories;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        VentaRepository ventaRepository;

        public VentaController()
        {
            ventaRepository = new VentaRepository();
        }

        [HttpGet("ObtenerVentasPorIdDeVenta")]
        public ActionResult ObtenerVenta([FromQuery] int idVenta)
        {
            var result = ventaRepository.TraerVentaXIdVenta(idVenta);
            
            return Ok(result);
        }

        [HttpGet("ObtenerVentasPorIdUsuario")]
        public ActionResult ObtenerVentaXIdUsuario([FromQuery] int idUsuario)
        {
            var result = ventaRepository.TraerVentaXIdUsuario(idUsuario);
            return Ok(result);
        }

        [HttpGet("ObtenerVentasYProductosVendidos")]
        public ActionResult TraerVentasYProductosVendidos()
        {
            var result = ventaRepository.TraerVentasYProductosVendidos();
            return Ok(result);
        }


        [HttpPost]
        public void CargarVenta([FromBody] Venta Venta)
        {

            ventaRepository.CargarVenta(Venta);
        }

        [HttpDelete]
        public void EliminarVenta([FromQuery] int idVenta)
        {
            ventaRepository.EliminarVenta(idVenta);
        }
    }
}
