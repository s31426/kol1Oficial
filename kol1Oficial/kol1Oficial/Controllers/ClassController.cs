using kol1Oficial.Services;
using Microsoft.AspNetCore.Mvc;

namespace kol1Oficial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IDbService _dbService;
        public ClassController(IDbService dbService)
        {
            _dbService = dbService;
        }
    }
    
}