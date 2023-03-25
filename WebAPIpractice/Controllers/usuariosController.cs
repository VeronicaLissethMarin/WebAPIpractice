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
            var listadoUsuarios = (from e in _equiposContexto.reservas
                                   join u in _equiposContexto.equipos on e.equipo_id equals u.id_equipos
                                   join us in _equiposContexto.usuarios on e.usuario_id equals us.usuario_id
                                   join es in _equiposContexto.estados_reservas on e.estado_reserva_id equals es.estado_res_id
                                   select new
                                   {
                                       e,
                                       u.id_equipos,
                                       u.nombre,
                                       u.descripcion,
                                       us.carnet,
                                       es.estados

                                   }).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            var Listadoreservas = (from e in _equiposContexto.reservas
                                   join u in _equiposContexto.equipos on e.equipo_id equals u.id_equipos
                                   join us in _equiposContexto.usuarios on e.usuario_id equals us.usuario_id
                                   join es in _equiposContexto.estados_reservas on e.estado_reserva_id equals es.estado_res_id
                                   where e.reserva_id == id
                                   select new
                                   {
                                       e,
                                       u.id_equipos,
                                       u.nombre,
                                       u.descripcion,
                                       us.carnet,
                                       es.estados
                                   }).FirstOrDefault();

            if (Listadoreservas == null)
            {
                return NotFound();
            }
            return Ok(Listadoreservas);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult Estadoytiempo(int filtro)
        {
            var Listadoreservas = (from e in _equiposContexto.reservas
                                   join us in _equiposContexto.usuarios on e.usuario_id equals us.usuario_id
                                   join es in _equiposContexto.estados_reservas on e.estado_reserva_id equals es.estado_res_id
                                   where e.reserva_id == filtro
                                   select new
                                   {
                                       e,
                                       us.nombre,
                                       es.estados
                                   }).FirstOrDefault();

            if (Listadoreservas == null)
            {
                return NotFound();
            }
            return Ok(Listadoreservas);
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
