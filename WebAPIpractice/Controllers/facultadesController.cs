using Microsoft.AspNetCore.Mvc;
using WebAPIpractice.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPIpractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class facultadesController : Controller
    {
        private readonly equiposContext _equiposContexto;
        public facultadesController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<facultades> listadoFacultad = (from e in _equiposContexto.facultades select e).ToList();

            if (listadoFacultad.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoFacultad);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            facultades? facultades = (from e in _equiposContexto.facultades
                                              where e.facultad_id == id
                                              select e).FirstOrDefault();

            if (facultades == null)
            {
                return NotFound();
            }
            return Ok(facultades);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByName(string filtro)
        {
            facultades? facultades = (from e in _equiposContexto.facultades
                                              where e.nombre_facultad.Contains(filtro)
                                              select e).FirstOrDefault();

            if (facultades == null)
            {
                return NotFound();
            }
            return Ok(facultades);
        }

       

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarFacultad([FromBody] facultades facultades)
        {
            try
            {
                _equiposContexto.facultades.Add(facultades);
                _equiposContexto.SaveChanges();
                return Ok(facultades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarFacultad(int id, [FromBody] facultades facultadesModificar)
        {
            facultades? facultadActual = (from e in _equiposContexto.facultades
                                                  where e.facultad_id == id
                                                  select e).FirstOrDefault();

            if (facultadActual == null)
            {
                return NotFound();
            }

            facultadActual.nombre_facultad = facultadesModificar.nombre_facultad;
    


            _equiposContexto.Entry(facultadActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(facultadesModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarFacultad(int id)
        {
            facultades? facultades = (from e in _equiposContexto.facultades
                                              where e.facultad_id == id
                                              select e).FirstOrDefault();

            if (facultades == null)
            {
                return NotFound();
            }

            _equiposContexto.facultades.Attach(facultades);
            _equiposContexto.facultades.Remove(facultades);
            _equiposContexto.SaveChanges();

            return Ok(facultades);
        }
    }
}
