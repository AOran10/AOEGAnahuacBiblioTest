using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("API")]
    [ApiController]
    public class TipoMedio : ControllerBase
    {
        // GET: api/<TipoMedio>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.TipoMedio.TipoMedioGetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // GET api/<TipoMedio>/5
        [EnableCors("API")]
        [HttpGet("GetById")]
        public IActionResult GetById(int IdTipoMedio)
        {
            ML.Result result = BL.TipoMedio.TipoMedioGetById(IdTipoMedio);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // POST api/<TipoMedio>
        [EnableCors("API")]
        [HttpPost("Add")]
        public IActionResult Add(ML.TipoMedio tipoMedio)
        {
            ML.Result result = BL.TipoMedio.TipoMedioAdd(tipoMedio);

            if(tipoMedio.IdTipoMedio != 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // PUT api/<TipoMedio>/5
        [EnableCors("API")]
        [HttpPut("Update")]
        public IActionResult Update(ML.TipoMedio tipoMedio)
        {
            ML.Result result = BL.TipoMedio.TipoMedioUpdate(tipoMedio);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // DELETE api/<TipoMedio>/5
        [EnableCors("API")]
        [HttpDelete("Delete")]
        public IActionResult Delete(int IdTipoMedio)
        {
            ML.Result result = BL.TipoMedio.TipoMedioDelete(IdTipoMedio);

            if (!result.Correct)
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
