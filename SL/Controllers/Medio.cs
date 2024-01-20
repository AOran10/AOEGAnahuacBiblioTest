using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Medio : ControllerBase
    {
        // GET: api/<Medio>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Medio.MedioGetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        // GET api/<Medio>/5
        [HttpGet("GetById")]
        public IActionResult GetById(int IdMedio) 
        {
            ML.Result result = BL.Medio.MedioGetById(IdMedio);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // POST api/<Medio>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Medio medio)
        {
            ML.Result result = BL.Medio.MedioAdd(medio);

            if(result.Correct)
            {
                return Ok(result);
            }
            else 
            {
                return BadRequest(result); 
            }
        }

        // PUT api/<Medio>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] ML.Medio medio)
        {
            ML.Result result = BL.Medio.MedioUpdate(medio);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // DELETE api/<Medio>/5
        [HttpDelete("delete")]
        public IActionResult Delete(int IdMedio)
        {
            ML.Result result = BL.Medio.MedioDelete(IdMedio);

            if(result.Correct)
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
