using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NzWalks.Api.Dtos;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Repositories;

namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper autoMapper;
        private readonly IWalks walks;

        public WalksController(IMapper mapper, IWalks walks)
        {
            this.autoMapper = mapper;
            this.walks = walks;
        }
        [HttpGet]
        public async Task<IActionResult> GetWalks([FromQuery] string? filterOn, [FromQuery] string? filterValue, [FromQuery] int pageSize = 1000, [FromQuery] int pageNumbers = 1)
        {
            var walksLst = await walks.GetWalks(filterOn, filterValue, pageSize, pageNumbers);
            return Ok(autoMapper.Map<List<WalksDto>>(walksLst));
            //return Ok(walksLst);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalks(Guid id)
        {
            var walksData = await walks.GetWalksById(id);
            if (walksData != null)
                return Ok(autoMapper.Map<WalksDto>(walksData));
            else
                return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddWalks(WalksInput walksInput)
        {
            if (ModelState.IsValid)//this is not required bad request will handle by self.
            {
                var walksData = autoMapper.Map<Walks>(walksInput);
                await walks.AddWalk(walksData);
                return Ok(walksData);
            }else
                return BadRequest(ModelState);
            
        }
        [HttpPut]
        public async Task<IActionResult> UpdateWalks(Guid id, WalksInput walksInput)
        {
            var walksData = autoMapper.Map<Walks>(walksInput);
            var walksresult = await walks.UpdateWalk(id, walksData);
            if (walksresult != null)
                return Ok(autoMapper.Map<WalksDto>(walksresult));
            else
                return NotFound();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteWalks(Guid id)
        {
            var walksData = await walks.DeleteWalk(id);
            if (walksData != null)
            {
                return Ok(autoMapper.Map<WalksDto>(walksData));
            }else
                return NotFound();
        }
    }
}
