using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIpractice.Models;

namespace WebAPIpractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estados_reservasController : Controller
    {
        private readonly equiposContext _equiposContexto;
        public estados_reservasController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<estados_reserva> listadoEstadoReserva = (from e in _equiposContexto.estados_reservas select e).ToList();

            if (listadoEstadoReserva.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEstadoReserva);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            estados_reserva? estados_reserva = (from e in _equiposContexto.estados_reservas
                                     where e.estado_res_id == id
                                     select e).FirstOrDefault();

            if (estados_reserva == null)
            {
                return NotFound();
            }
            return Ok(estados_reserva);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            estados_reserva? estados_reserva = (from e in _equiposContexto.estados_reservas
                                     where e.estados.Contains(filtro)
                                     select e).FirstOrDefault();

            if (estados_reserva == null)
            {
                return NotFound();
            }
            return Ok(estados_reserva);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarestados_reserva([FromBody] estados_reserva estados_reserva)
        {
            try
            {
                _equiposContexto.estados_reservas.Add(estados_reserva);
                _equiposContexto.SaveChanges();
                return Ok(estados_reserva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizarestados_reserva(int id, [FromBody] estados_reserva estados_reservaModificar)
        {
            estados_reserva? estados_reservaActual = (from e in _equiposContexto.estados_reservas
                                           where e.estado_res_id == id
                                           select e).FirstOrDefault();

            if (estados_reservaActual == null)
            {
                return NotFound();
            }

            estados_reservaActual.estados = estados_reservaModificar.estados;


            _equiposContexto.Entry(estados_reservaActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(estados_reservaModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarestados_reserva(int id)
        {
            estados_reserva? estados_reserva = (from e in _equiposContexto.estados_reservas
                                     where e.estado_res_id == id
                                     select e).FirstOrDefault();

            if (estados_reserva == null)
            {
                return NotFound();
            }

            _equiposContexto.estados_reservas.Attach(estados_reserva);
            _equiposContexto.estados_reservas.Remove(estados_reserva);
            _equiposContexto.SaveChanges();

            return Ok(estados_reserva);
        }
    }
}
