using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedisCachingApp.Application.Interfaces.Services;
using RedisCachingApp.Domain.Entities;
using RedisCachingApp.Persistence.Context;

namespace RedisCachingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private readonly ICacheService _cacheService;
        private readonly ApplicationDbContext _context;

        public DriversController(ILogger<DriversController> logger, ICacheService cacheService, ApplicationDbContext context)
        {
            _logger = logger;
            _cacheService = cacheService;
            _context = context;
        }

        [HttpGet("drivers")]
        public async Task<IActionResult> Get()
        {
            // check cache data
            var cacheData = _cacheService.GetData<IEnumerable<Driver>>("drivers");

            if (cacheData != null && cacheData.Count() > 0) return Ok(cacheData);

            cacheData = await _context.Drivers.ToListAsync();

            // Set expiry time
            var expiryTime = DateTimeOffset.Now.AddSeconds(30);

            _cacheService.SetData<IEnumerable<Driver>>("drivers", cacheData, expiryTime);

            return Ok(cacheData);
        }

        [HttpPost("AddDriver")]
        public async Task<IActionResult> Post(Driver value)
        {
            var addedObj = await _context.Drivers.AddAsync(value);

            var expiryTime = DateTimeOffset.Now.AddSeconds(30);

            _cacheService.SetData<Driver>($"driver{value.Id}", addedObj.Entity, expiryTime);

            await _context.SaveChangesAsync();

            return Ok(addedObj.Entity);
        }

        [HttpDelete("DeleteDriver")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = _context.Drivers.FirstOrDefault(x => x.Id == id);

            if (exists != null)
            {
                _context.Remove(exists);
                _cacheService.RemoveData($"driver{id}");
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return NotFound();
        }

    }
}
