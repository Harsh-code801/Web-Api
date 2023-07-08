using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Data;
using NzWalks.Api.Dtos;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Repositories;

namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IResionRepositories sqlResionRepositories;

        public RegionsController(IResionRepositories sqlResionRepositories)
        {
            this.sqlResionRepositories = sqlResionRepositories;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            var regions = await sqlResionRepositories.GetRegions();
            #region AutoMapper
            /*var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<Region>, List<RegionDto>>().ForMember("Id",x=>x.Ignore());
                //cfg.CreateMap<List<RegionDto>, List<Region>>().ReverseMap();
            });
            IMapper iMapper = config.CreateMapper();
            var MappedValuc = iMapper.Map<List<Region>, List<RegionDto>>(regions);*/
            #endregion

            return Ok(regions);
        }
        [HttpGet]
        [Route("{id:Guid}")]//id is case sensitive with parameters id
        public async Task<IActionResult> GetRegionsById(Guid id)
        {
            var region = await sqlResionRepositories.GetRegionsById(id);
            if (region != null)
            {
                return Ok(region);
            }
            else
                return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody] Region region)
        {
            region.Id = Guid.NewGuid();
            await sqlResionRepositories.AddRegion(region);
            return CreatedAtAction(nameof(GetRegionsById), new { id = region.Id }, region);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion(Guid id,[FromBody] Region region) 
        {
            var regionupdate = await sqlResionRepositories.UpdateRegion(id, region);
            if(regionupdate != null)
            {
                return Ok(regionupdate);
            }
            else
                return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await sqlResionRepositories.DeleteRegion(id);
            if(region != null)
            {   
                return Ok(region);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
