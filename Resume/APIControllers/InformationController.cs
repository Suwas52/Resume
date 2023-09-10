using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Data;
using Resume.Models;
using Resume.Helpers;
using AutoMapper;
using Resume.DTOs.InfomationDTOs;

namespace Resume.APIControllers
{
    [APIKeyAuth]

    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResumeContext _context;

        public InformationController(IMapper mapper, ResumeContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        
        [HttpGet]

        public async Task<ActionResult<List<Information>>> GetInformation()
        {
            if (_context.Information == null)
            {
                return NotFound();
            }
            var information = await _context.Information.ToListAsync();
            var records = _mapper.Map<List<InformationReadDTOs>>(information);
            return Ok(records);

        }


        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Information>>> GetInformation(int id)
        {

            if (_context.Information == null)
          {
              return NotFound();
          }
            
            var information = await _context.Information.Where(c => c.info_id == id ).ToListAsync();

            var records = _mapper.Map<List<InformationReadDTOs>>(information);

            return Ok(records);
        }

        // PUT: api/Information/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformation(int id, InformationUpdateDTOs informationUpdateDTOs)
        {
            var info = await _context.Information.FindAsync(id);

            if (id != informationUpdateDTOs.info_id)
            {
                return BadRequest();
            }

            if (info == null)
            {
                throw new Exception($"Information {id} is not found.");
            }
            _mapper.Map(informationUpdateDTOs, info);
            _context.Information.Update(info);
            await _context.SaveChangesAsync();

            var infoReadDTO = _mapper.Map<InformationReadDTOs>(info);

            return Ok(infoReadDTO);
        }

        // POST: api/Information
        [HttpPost]
        public async Task<ActionResult<Information>> PostInformation(InformationCreateDTOs informationCreateDTOs)
        {
            if (informationCreateDTOs == null)
            {
                return BadRequest("Entity set 'ResumeContext.Info' is null.");
            }
            var infoEntity = _mapper.Map<Information>(informationCreateDTOs);
            _context.Information.Add(infoEntity);
            await _context.SaveChangesAsync();
            var records = _mapper.Map<InformationReadDTOs>(infoEntity);
            return CreatedAtAction("GetInformation", new { id = infoEntity.info_id }, records);
        }

        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            if (_context.Information == null)
            {
                return NotFound();
            }
            var information = await _context.Information.FindAsync(id);
            if (information == null)
            {
                return NotFound();
            }

            _context.Information.Remove(information);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InformationExists(int id)
        {
            return (_context.Information?.Any(e => e.info_id == id)).GetValueOrDefault();
        }
    }
}
