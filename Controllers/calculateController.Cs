using Microsoft.AspNetCore.Mvc;

namespace rutaApiv1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CalculateController : ControllerBase
    {

        [HttpGet("multiplicar/{number}/{calculo}")]
        public ActionResult<int> Get(int number, int calculo)
        {
            if (number < 0 || calculo < 0)
            {
                return BadRequest("Number and calculo must be non-negative.");
            }
            if (number == 0 || calculo == 0)
            {
                return UnprocessableEntity("Number and calculo are required.");
            }
            else if (number < 0)
            {
                return UnprocessableEntity("Number must be non-negative.");
            }
            else
            {
                number = number * calculo;
                return Ok($"el valor resultad es: {number}");
            }


        }
        [HttpGet("ejercicio2/{numero1}/{numero2}")]

        public ActionResult<int> Getejercicio2(int numero1, int numero2)
        {
            if (numero1 < 0 || numero2 < 0 || numero1 > 1000 || numero2 > 1000)
            {
                return BadRequest("Numbers must be non-negative and less than or equal to 1000.");
            }
            if (numero1 == 0 || numero2 == 0)
            {
                return UnprocessableEntity("Both numbers are required.");
            }
            else if (numero1 < 0)
            {
                return UnprocessableEntity("numero1 must be non-negative.");
            }
            else
            {
                int resultado = numero1 + numero2;
                return Ok($"el valor resultad es: {resultado}");
            }
        }

        
    }
}