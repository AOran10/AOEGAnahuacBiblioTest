using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Medio : ControllerBase
    {
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Medio.MedioGetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("getbyid/{idMedio}")]
        public IActionResult GetById(int idMedio)
        {
            ML.Result result = BL.Medio.MedioGetById(idMedio);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("delete/{idMedio}")]
        public IActionResult Delete(int idMedio)
        {
            ML.Result result = BL.Medio.MedioDelete(idMedio);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("add")]
        [Authorize]
        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        public IActionResult Add([FromBody] ML.Medio medio)
        {
            ML.Result result = BL.Medio.MedioAdd(medio);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut("update")]
        [Authorize]
        public IActionResult Update([FromBody] ML.Medio medio)
        {
            ML.Result result = BL.Medio.MedioUpdate(medio);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
