using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("API")]   // Esta linea habilita los CORS
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        // GET: api/<PrestamosController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Prestamo.PrestamoGetAll();

            ML.Prestamo prestamo = new ML.Prestamo();
            prestamo.Prestamos = result.Objects;

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // GET api/<PrestamosController>/5
        [HttpGet("{id}")]
        public string GetById(int id)
        {
            return "value";
        }

        // POST api/<PrestamosController>
        [EnableCors("API")]
        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Prestamo prestamo)
        {
            ML.Result result = BL.Prestamo.PrestamoAdd(prestamo);
            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }


        // PUT api/<PrestamosController>/5
        [EnableCors("API")]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] ML.Prestamo prestamo)
        {
            ML.Result result = BL.Prestamo.PrestamoUpdate(prestamo);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<PrestamosController>/5

        [EnableCors("API")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] ML.Prestamo prestamo)
        {
            ML.Result result = BL.Prestamo.PrestamoDelete(prestamo.IdPrestamo);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
