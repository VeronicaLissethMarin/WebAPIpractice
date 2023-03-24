using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using WebAPIpractice.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPIpractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipo_equipoController : Controller
    {
        private readonly equiposContext _equiposContexto;

        public tipo_equipoController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<tipo_equipo> listadoTipo_Equipo = (from e in _equiposContexto.tipo_equipo select e).ToList();

            if (listadoTipo_Equipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoTipo_Equipo);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            tipo_equipo? tipo_equipo = (from e in _equiposContexto.tipo_equipo
                             where e.id_tipo_equipo == id
                             select e).FirstOrDefault();

            if (tipo_equipo == null)
            {
                return NotFound();
            }
            return Ok(tipo_equipo);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            tipo_equipo? tipo_equipo = (from e in _equiposContexto.tipo_equipo
                             where e.descripcion.Contains(filtro)
                             select e).FirstOrDefault();

            if (tipo_equipo == null)
            {
                return NotFound();
            }
            return Ok(tipo_equipo);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult Findestados(string filtro)
        {
            tipo_equipo? tipo_equipo = (from e in _equiposContexto.tipo_equipo
                             where e.estado.Contains(filtro)
                             select e).FirstOrDefault();

            if (tipo_equipo == null)
            {
                return NotFound();
            }
            return Ok(tipo_equipo);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Guardartipo_equipo([FromBody] tipo_equipo tipo_equipo)
        {
            try
            {
                _equiposContexto.tipo_equipo.Add(tipo_equipo);
                _equiposContexto.SaveChanges();
                return Ok(tipo_equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizartipo_equipo(int id, [FromBody] tipo_equipo tipo_equipoModificar)
        {
            tipo_equipo? tipo_equipoActual = (from e in _equiposContexto.tipo_equipo
                                   where e.id_tipo_equipo == id
                                   select e).FirstOrDefault();

            if (tipo_equipoActual == null)
            {
                return NotFound();
            }

            tipo_equipoActual.descripcion = tipo_equipoModificar.descripcion;
            tipo_equipoActual.estado = tipo_equipoModificar.estado;


            _equiposContexto.Entry(tipo_equipoActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(tipo_equipoModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminartipo_equipo(int id)
        {
            tipo_equipo? tipo_equipo = (from e in _equiposContexto.tipo_equipo
                             where e.id_tipo_equipo == id
                             select e).FirstOrDefault();

            if (tipo_equipo == null)
            {
                return NotFound();
            }

            _equiposContexto.tipo_equipo.Attach(tipo_equipo);
            _equiposContexto.tipo_equipo.Remove(tipo_equipo);
            _equiposContexto.SaveChanges();

            return Ok(tipo_equipo);
        }
    }
}
