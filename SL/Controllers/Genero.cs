using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[Controller]")]
    [EnableCors("API")]
    [ApiController]
    public class Genero : ControllerBase
    {
        // GET: api/<Genero>
       
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Genero.GeneroGetAll();           

            if (result.Correct)
            {
                return Ok(result);  
            }
            else
            {
                return BadRequest(result);
            }
        }

        // GET api/<Genero>/5
       
        [EnableCors("API")]
        [HttpGet("GetBiId/{IdGenero}")]
        public IActionResult GetById(int IdGenero) 
        {
            ML.Result result = BL.Genero.GeneroGetById(IdGenero);
            
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // POST api/<Genero>
        [EnableCors("API")]
        [HttpPost("Add")]
        public IActionResult Add(ML.Genero genero)
        {
            ML.Result result = BL.Genero.GeneroAdd(genero);

            if (genero.IdGenero == 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // PUT api/<Genero>/5
        [EnableCors("API")]
        [HttpPut("Update")]
        public IActionResult Update(ML.Genero genero)
        {
            ML.Result result = BL.Genero.GeneroUpdate(genero);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // DELETE api/<Genero>/5
        [EnableCors("API")]
        [HttpDelete("Delete")]
        public IActionResult Delete(int IdGenero)
        {
            ML.Result result = BL.Genero.GeneroDelete(IdGenero);

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
