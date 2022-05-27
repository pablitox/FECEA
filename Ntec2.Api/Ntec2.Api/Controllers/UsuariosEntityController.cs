using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ntec2.Api.Database;
using Ntec2.Api.Models;

namespace Ntec2.Api.Controllers
{
    [ApiController]
    [Route("api/entity")]
    public class UsuariosEntityController : ControllerBase
    {

        private readonly Contexto _contexto;

        public UsuariosEntityController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosEntity()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosEntityByName(string nombre)
        {
            return await _contexto.Usuarios.Where(c => EF.Functions.Like(c.Nombre, nombre)).ToListAsync();
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuariosEntityById(int id)
        {
            return await _contexto.Usuarios.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostUsuarioEntity(Usuario usuario)
        {
            _contexto.Add(usuario);
            await _contexto.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> PutUsuarioEntity(Usuario datosNuevos, int id)
        {

            if (datosNuevos.Id != id)
            {
                return BadRequest("El id esta mal");
            }

            //Capturo los datos originales del usuario
            var datosActuales = await _contexto.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (datosNuevos.Nombre != ""){ datosActuales.Nombre = datosNuevos.Nombre;}
            if (datosNuevos.Email != "") { datosActuales.Email = datosNuevos.Email;}
            if (datosNuevos.Dni != "")   { datosActuales.Dni = datosNuevos.Dni;}

            _contexto.Update(datosActuales);
            await _contexto.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUsuarioEntity(int id )
        {
            var existe = await _contexto.Usuarios.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            _contexto.Remove(new Usuario() { Id = id});
            await _contexto.SaveChangesAsync();
            return Ok();
        }

    }
}
