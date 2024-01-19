using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("API")]  // Habilitar CORS
    [ApiController]
    public class Idioma : ControllerBase
    {
        // GET: api/<IdiomaController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Idioma.IdiomaGetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // GET api/<IdiomaController>/5
        [HttpGet("GetById")]
        public IActionResult GetById(int IdIdioma)
        {
            ML.Result result = BL.Idioma.IdiomaGetById(IdIdioma);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
           
        }

        // POST api/<IdiomaController>
        [EnableCors("API")]
        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add(ML.Idioma idioma)
        {
            ML.Result result = BL.Idioma.IdiomaAdd(idioma);

            if (idioma.IdIdioma == 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
           
        }

        // PUT api/<IdiomaController>/5
        [EnableCors("API")]
        [HttpPut("Update")]
        [Authorize]
        public IActionResult Update(ML.Idioma idioma)
        {
            ML.Result result = BL.Idioma.IdiomaUpdate(idioma);

            if (result.Correct)
            {
                return Ok(result);  // Estatus 200
            }
            else
            {
                return BadRequest(result); // Error estatus 500
            }
        }

        // DELETE api/<IdiomaController>/5
        [EnableCors("API")]
        [HttpDelete("Delete")]
        public IActionResult Delete(int IdIdioma)
        {
            ML.Result result = BL.Idioma.IdiomaDelete(IdIdioma);

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
