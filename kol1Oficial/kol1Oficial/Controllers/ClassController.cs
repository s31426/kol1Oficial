using kol1Oficial.Exceptions;
using kol1Oficial.Services;
using Microsoft.AspNetCore.Mvc;

namespace kol1Oficial.Controllers
{
    [Route("api/deliveries")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IDbService _dbService;
        public ClassController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res = await _dbService.GetDeliveries(id);
                if (res == null)
                {
                    throw new NotFoundException("Delivery not found");
                }

                return Ok(res);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
    
}