using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Autor : ControllerBase
    {
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Autor.AutorGetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("getbyid/{idAutor}")]
        public IActionResult GetById(int idAutor)
        {
            ML.Result result = BL.Autor.AutorGetById(idAutor);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("delete/{idAutor}")]
        public IActionResult Delete(int idAutor)
        {
            ML.Result result = BL.Autor.AutorDelete(idAutor);
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
        public IActionResult Add([FromBody] ML.Autor autor)
        {
            ML.Result result = BL.Autor.AutorAdd(autor);
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
        public IActionResult Update([FromBody] ML.Autor autor)
        {
            ML.Result result = BL.Autor.AutorUpdate(autor);
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