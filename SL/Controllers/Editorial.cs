using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

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
        [Authorize]
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
        [Authorize]
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