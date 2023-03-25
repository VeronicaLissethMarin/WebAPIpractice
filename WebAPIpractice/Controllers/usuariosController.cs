using Microsoft.AspNetCore.Mvc;
using WebAPIpractice.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPIpractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : Controller
    {
        private readonly equiposContext _equiposContexto;
        public usuariosController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllUsuarios()
        {
            var listadoUsuarios = (from e in _equiposContexto.usuarios
                                   join u in _equiposContexto.carreras on e.carrera_id equals u.carrera_id
                                   join c in _equiposContexto.facultades on u.facultad_id equals c.facultad_id
                                   select new
                                   {
                                       e,
                                       u.nombre_carrera,
                                       c.nombre_facultad
                                   }).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var ListadoUsuarios = (from e in _equiposContexto.usuarios
                                   join u in _equiposContexto.carreras on e.carrera_id equals u.carrera_id
                                   join c in _equiposContexto.facultades on u.facultad_id equals c.facultad_id
                                   where e.usuario_id == id
                                   select new
                                   {
                                       e,
                                       u.nombre_carrera,
                                       c.nombre_facultad
                                   }).FirstOrDefault();

            if (ListadoUsuarios == null)
            {
                return NotFound();
            }
            return Ok(ListadoUsuarios);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FiltradoNombre(string filtro)
        {
            var ListadoUsuarios = (from e in _equiposContexto.usuarios
                                   join u in _equiposContexto.carreras on e.carrera_id equals u.carrera_id
                                   join c in _equiposContexto.facultades on u.facultad_id equals c.facultad_id
                                   where e.nombre.Contains(filtro)
                                   select new
                                   {
                                       e,
                                       u.nombre_carrera,
                                       c.nombre_facultad
                                   }).FirstOrDefault();

            if (ListadoUsuarios == null)
            {
                return NotFound();
            }
            return Ok(ListadoUsuarios);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarUsuario([FromBody] usuarios usuarios)
        {
            try
            {
                _equiposContexto.usuarios.Add(usuarios);
                _equiposContexto.SaveChanges();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEstado(int id, [FromBody] usuarios usuariosModificar)
        {
            usuarios? usuarioActual = (from e in _equiposContexto.usuarios
                                                  where e.usuario_id == id
                                                  select e).FirstOrDefault();

            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.nombre = usuariosModificar.nombre;
            usuarioActual.documento = usuariosModificar.documento;
            usuarioActual.tipo = usuariosModificar.tipo;
            usuarioActual.carnet = usuariosModificar.carnet;
            usuarioActual.carrera_id = usuariosModificar.carrera_id;


            _equiposContexto.Entry(usuarioActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(usuariosModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEstadoEquipo(int id)
        {
            usuarios? usuarios = (from e in _equiposContexto.usuarios
                                              where e.usuario_id == id
                                              select e).FirstOrDefault();

            if (usuarios == null)
            {
                return NotFound();
            }

            _equiposContexto.usuarios.Attach(usuarios);
            _equiposContexto.usuarios.Remove(usuarios);
            _equiposContexto.SaveChanges();

            return Ok(usuarios);
        }
    }
}
