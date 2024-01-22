using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Prestamo : ControllerBase
    {
        [HttpGet("getall/{filtro}")]
        public IActionResult GetAll(int filtro)
        {
            ML.Result result = BL.Prestamo.PrestamoGetAll(filtro);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
  

    [HttpGet("getbyid/{idPrestamo}")]
    public IActionResult GetById(int idPrestamo)
    {
        ML.Result result = BL.Prestamo.PrestamoGetById(idPrestamo);
        if (result.Correct)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result.Message);
        }
    }

    [HttpDelete("delete/{idPrestamo}")]
    public IActionResult Delete(int idPrestamo)
    {
        ML.Result result = BL.Prestamo.PrestamoDelete(idPrestamo);
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
    public IActionResult Add([FromBody] ML.Prestamo prestamo)
    {
        ML.Result result = BL.Prestamo.PrestamoAdd(prestamo);
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
    public IActionResult Update([FromBody] ML.Prestamo prestamo)
    {
        ML.Result result = BL.Prestamo.PrestamoUpdate(prestamo);
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
