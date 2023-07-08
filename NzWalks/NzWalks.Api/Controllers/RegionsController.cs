using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Data;
using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksDbContext _dbContext;
        public RegionsController(NzWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetRegions()
        {
            return Ok(_dbContext.Regions.ToList());
        }
        [HttpGet]
        [Route("{id:Guid}")]//id is case sensitive with parameters id
        public IActionResult GetRegionsById(Guid id)
        {
            var region = _dbContext.Regions.Find(id);
            if (region != null)
                return Ok(region);
            else
                return NotFound();
        }
        [HttpPost]
        public IActionResult AddRegion([FromBody] Region region)
        {
            region.Id = Guid.NewGuid();
            _dbContext.Regions.Add(region);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetRegionsById), new { id = region.Id }, region);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateRegion(Guid id,[FromBody] Region region) 
        {
            var regionupdate = _dbContext.Regions.FirstOrDefault(x=>x.Id == id);
            if(regionupdate != null)
            {
                regionupdate.Name = region.Name;
                regionupdate.Code = region.Code;
                regionupdate.RegionImageUrl = region.RegionImageUrl;

                _dbContext.SaveChanges();
                return Ok(regionupdate);
            }
            else
                return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var region =  _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(region != null)
            {
                _dbContext.Remove(region);
                _dbContext.SaveChanges();
                return Ok(region);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
