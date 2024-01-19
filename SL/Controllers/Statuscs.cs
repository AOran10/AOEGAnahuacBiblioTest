using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Statuscs : ControllerBase
    {
        // GET: api/<Statuscs>
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Status.StatusGetAll();

            if (result.Correct)
            {
                return Ok(result); 
            }
            else
            {
                return BadRequest(result);
            }
        }

        // GET api/<Statuscs>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Statuscs>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Statuscs>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Statuscs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
