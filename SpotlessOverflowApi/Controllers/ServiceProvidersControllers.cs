using Domain.Models;
using Infrastructure.EFContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SpotlessOverflowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProvidersController : ControllerBase
    {
        private readonly SpotlessDataBaseContext _context;

        public ServiceProvidersController(SpotlessDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/ServiceProviders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceProviders>>> GetServiceProviders()
        {
            return await _context.ServiceProviders.ToListAsync();
        }

        // GET: api/ServiceProviders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceProviders>> GetServiceProvider(int id)
        {
            var serviceProvider = await _context.ServiceProviders.FindAsync(id);

            if (serviceProvider == null)
            {
                return NotFound();
            }

            return serviceProvider;
        }

        // POST: api/ServiceProviders
        [HttpPost]
        public async Task<ActionResult<ServiceProviders>> CreateServiceProvider(ServiceProviders serviceProvider)
        {
            _context.ServiceProviders.Add(serviceProvider);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceProvider), new { id = serviceProvider.Id }, serviceProvider);
        }

        // PUT: api/ServiceProviders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceProvider(int id, ServiceProviders serviceProvider)
        {
            if (id != serviceProvider.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviceProvider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceProviderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ServiceProviders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceProvider(int id)
        {
            var serviceProvider = await _context.ServiceProviders.FindAsync(id);
            if (serviceProvider == null)
            {
                return NotFound();
            }

            _context.ServiceProviders.Remove(serviceProvider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceProviderExists(int id)
        {
            return _context.ServiceProviders.Any(s => s.Id == id);
        }
    }
}
