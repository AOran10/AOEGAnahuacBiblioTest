using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Editorial : Controller
    {
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Editorial.EditorialGetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("getbyid/{idEditorial}")]
        public IActionResult GetById(int idEditorial)
        {
            ML.Result result = BL.Editorial.EditorialGetById(idEditorial);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("delete/{idEditorial}")]
        public IActionResult Delete(int idEditorial)
        {
            ML.Result result = BL.Editorial.EditorialDelete(idEditorial);
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
        public IActionResult Add([FromBody] ML.Editorial editorial)
        {
            ML.Result result = BL.Editorial.EditorialAdd(editorial);
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
        public IActionResult Update([FromBody] ML.Editorial editorial)
        {
            ML.Result result = BL.Editorial.EditorialUpdate(editorial);
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