using Microsoft.AspNetCore.Mvc;
using WebAPIpractice.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPIpractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estados_equipoController : Controller
    {
        private readonly equiposContext _equiposContexto;
        public estados_equipoController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<estados_equipo> listadoestadoEquipo = (from e in _equiposContexto.estados_equipos select e).ToList();

            if (listadoestadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoestadoEquipo);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            estados_equipo? estados_Equipo = (from e in _equiposContexto.estados_equipos
                             where e.id_estados_equipo == id
                             select e).FirstOrDefault();

            if (estados_Equipo == null)
            {
                return NotFound();
            }
            return Ok(estados_Equipo);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            estados_equipo? estados_Equipo = (from e in _equiposContexto.estados_equipos
                             where e.descripcion.Contains(filtro)
                             select e).FirstOrDefault();

            if (estados_Equipo == null)
            {
                return NotFound();
            }
            return Ok(estados_Equipo);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult Findestados(string filtro)
        {
            estados_equipo? estados_Equipo = (from e in _equiposContexto.estados_equipos
                             where e.estado.Contains(filtro)
                             select e).FirstOrDefault();

            if (estados_Equipo == null)
            {
                return NotFound();
            }
            return Ok(estados_Equipo);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEstado([FromBody] estados_equipo estados_Equipo)
        {
            try
            {
                _equiposContexto.estados_equipos.Add(estados_Equipo);
                _equiposContexto.SaveChanges();
                return Ok(estados_Equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEstado(int id, [FromBody] estados_equipo estados_EquipoModificar)
        {
            estados_equipo? estadoEquipoActual = (from e in _equiposContexto.estados_equipos
                                   where e.id_estados_equipo == id
                                   select e).FirstOrDefault();

            if (estadoEquipoActual == null)
            {
                return NotFound();
            }

            estadoEquipoActual.descripcion = estados_EquipoModificar.descripcion;
            estadoEquipoActual.estado = estados_EquipoModificar.estado;


            _equiposContexto.Entry(estadoEquipoActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(estados_EquipoModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEstadoEquipo(int id)
        {
            estados_equipo? estados_Equipo = (from e in _equiposContexto.estados_equipos
                             where e.id_estados_equipo == id
                             select e).FirstOrDefault();

            if (estados_Equipo == null)
            {
                return NotFound();
            }

            _equiposContexto.estados_equipos.Attach(estados_Equipo);
            _equiposContexto.estados_equipos.Remove(estados_Equipo);
            _equiposContexto.SaveChanges();

            return Ok(estados_Equipo);
        }
    }
}
