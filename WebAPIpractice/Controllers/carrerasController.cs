using Microsoft.AspNetCore.Mvc;
using WebAPIpractice.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPIpractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carrerasController : Controller
    {
        private readonly equiposContext _equiposContexto;
        public carrerasController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarCarrera([FromBody] estados_equipo carrera)
        {
            try
            {
                _equiposContexto.estados_equipos.Add(carrera);
                _equiposContexto.SaveChanges();
                return Ok(carrera);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarCarrera(int id, [FromBody] carreras carreraModificar)
        {
            carreras? carreraActual = (from e in _equiposContexto.carreras
                                                  where e.carrera_id == id
                                                  select e).FirstOrDefault();

            if (carreraActual == null)
            {
                return NotFound();
            }

            carreraActual.nombre_carrera = carreraModificar.nombre_carrera;
            carreraActual.facultad_id = carreraModificar.facultad_id;


            _equiposContexto.Entry(carreraActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carreraModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarCarrera(int id)
        {
            carreras? carrera = (from e in _equiposContexto.carreras
                                              where e.carrera_id == id
                                              select e).FirstOrDefault();

            if (carrera == null)
            {
                return NotFound();
            }

            _equiposContexto.carreras.Attach(carrera);
            _equiposContexto.carreras.Remove(carrera);
            _equiposContexto.SaveChanges();

            return Ok(carrera);
        }
    }
}
