using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Repositories;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        UsuarioRepository usuarioRepository;

        public UsuarioController()
        {
            usuarioRepository = new UsuarioRepository();
        }

        [HttpGet("{NombreUsuario}")]
        public ActionResult ObtenerUsuario([FromQuery] string NombreUsuario)
        {
            var result = usuarioRepository.TraerUsuario(NombreUsuario);

            if (result.Count >= 1) 
            {
                return Ok(result);
            }
            else
            {
                return Ok("No existe usuarios con el nombre de usuario ingresado");
            }
        }

        [HttpGet("{NombreUsuario}/{Contra}")]
        public ActionResult InicioSesion([FromRoute] string NombreUsuario,[FromRoute] string Contra)
        {
            var result = usuarioRepository.InicioDeSesion(NombreUsuario, Contra);

            return Ok(result);
        }

        [HttpPut] 
        public void ModificarUsuario([FromQuery] Usuario usuario)
        {
            usuarioRepository.ModificarUsuario(usuario);

        }

        [HttpPost]
        public ActionResult CrearUsuario([FromQuery] Usuario usuario)
        {

            var result1 = usuarioRepository.CrearUsuario(usuario);

            if (usuario.Nombre == string.Empty || usuario.Apellido == string.Empty || usuario.NombreUsuario == string.Empty || usuario.Password == string.Empty || usuario.Mail == string.Empty)
            {
                return Ok("hay uno o varios campos vacios y no se puede crear un nuevo usuario");
                
            }
            else
            {
                return Ok(result1);
            }

        }

        [HttpDelete]
        public void EliminarUsuario([FromQuery] int idUsuario)
        {
            usuarioRepository.EliminarUsuario(idUsuario);
        }


    }
}
