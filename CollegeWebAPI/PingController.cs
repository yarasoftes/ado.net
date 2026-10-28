using Microsoft.AspNetCore.Mvc;

namespace CollegeWebAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Сервер работает! Контроллеры найдены.");
        }
    }
}