using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [ApiController]
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}")]        
        public IActionResult GetById(int id)
        {
            if (true) return NotFound();
            else
            {
                return Ok();
            }
        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            if (true) return NotFound();
            else
            {
                return Ok();
            }

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
        public IActionResult Index()
        {
            throw new NotImplementedException();
        }
    }
}
