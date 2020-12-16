using Kooliprojekt.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase

    {
        private readonly ApplicationDbContext _context;
        public ApiController(ApplicationDbContext dataContext)
        {
            _context = dataContext;
        }

       
        public async Task<ActionResult<IEnumerable<Car>>> List()
        {
            return await _context.Cars.ToListAsync();
        }




        [HttpPost]
        public async Task<ActionResult<Car>> PostCars(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasks", new { id = car.Id }, car);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Car>> PutCar(Car car)
        {
            _context.Update(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
