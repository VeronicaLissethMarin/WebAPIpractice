using Microsoft.AspNetCore.Mvc;
using WebAPIpractice.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPIpractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class reservasController : Controller
    {
        private readonly equiposContext _equiposContexto;
        public reservasController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllReservas()
        {
            var listadoReservas = (from e in _equiposContexto.reservas
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

            if (listadoReservas.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoReservas);
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
        public IActionResult GuardarReserva([FromBody] reservas reserva)
        {
            try
            {
                _equiposContexto.reservas.Add(reserva);
                _equiposContexto.SaveChanges();
                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarReserva(int id, [FromBody] reservas reservasModificar)
        {
            reservas? reservaActual = (from e in _equiposContexto.reservas
                                                  where e.reserva_id == id
                                                  select e).FirstOrDefault();

            if (reservaActual == null)
            {
                return NotFound();
            }

            reservaActual.equipo_id = reservasModificar.equipo_id;
            reservaActual.usuario_id = reservasModificar.usuario_id;
            reservaActual.fecha_salida = reservasModificar.fecha_salida;
            reservaActual.hora_salida = reservasModificar.hora_salida;
            reservaActual.tiempo_reserva = reservasModificar.tiempo_reserva;
            reservaActual.estado_reserva_id = reservasModificar.estado_reserva_id;
            reservaActual.fecha_retorno = reservasModificar.fecha_retorno;
            reservaActual.hora_retorno = reservasModificar.hora_retorno;


            _equiposContexto.Entry(reservaActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(reservasModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarReserva(int id)
        {
            reservas? reservas = (from e in _equiposContexto.reservas
                                              where e.reserva_id == id
                                              select e).FirstOrDefault();

            if (reservas == null)
            {
                return NotFound();
            }

            _equiposContexto.reservas.Attach(reservas);
            _equiposContexto.reservas.Remove(reservas);
            _equiposContexto.SaveChanges();

            return Ok(reservas);
        }
    }
}
