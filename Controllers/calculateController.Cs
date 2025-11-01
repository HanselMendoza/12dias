using Microsoft.AspNetCore.Mvc;

namespace rutaApiv1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CalculateController : ControllerBase
    {
        
        [HttpGet("{number}/{calculo}")]
        public ActionResult<int> Get(int number, int calculo)
        {
            if(number<0 || calculo<0)
            {
                return BadRequest("Number and calculo must be non-negative.");
            }
            if(number == 0 || calculo == 0)
            {
                return UnprocessableEntity("Number and calculo are required.");
            }
            else if(number <0)
            {
                return UnprocessableEntity("Number must be non-negative.");
            }
            else
            {
                number= number*calculo;
                return Ok($"el valor resultad es: {number}");
            }
             
            
        }
        
    }
}