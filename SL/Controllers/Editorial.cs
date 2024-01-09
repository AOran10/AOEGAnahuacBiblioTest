using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("API")]
    [ApiController]
    public class Editorial : ControllerBase
    {
        // GET: api/<Editorial>
        [HttpGet ("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Editorial.EditorialGetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else   
            {
                return BadRequest(result);
            }
        }

        // GET api/<Editorial>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Editorial>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Editorial>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Editorial>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
