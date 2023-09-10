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
using Resume.DTOs.ExperienceDTOs;

namespace Resume.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResumeContext _context;

        public ExperiencesController(IMapper mapper , ResumeContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Experience>>> GetExperience()
        {
          if (_context.Experience == null)
          {
              return NotFound();
          }
            var experience = await _context.Experience.ToListAsync();
            var result = _mapper.Map<List<ExperienceReadDTOs>>(experience);
            return Ok(result);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Experience>>> GetExperience(int id)
        {
          if (_context.Experience == null)
          {
              return NotFound();
          }
            var experience = await _context.Experience.Where(c => c.info_id == id).ToListAsync();

            if (experience == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<List<ExperienceReadDTOs>>(experience);

            return Ok(result);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperience(int id, ExperienceUpdateDTOs experienceUpdateDTOs)
        {
            if (_context.Experience == null)
            {
                return NotFound();
            }

            var experience = await _context.Experience.Where(c => c.info_id == id && c.experience_id == experienceUpdateDTOs.experience_id).FirstOrDefaultAsync();

            if (experience == null)
            {
                return NotFound($"Experience with ID {id} not found.");
            }


            _mapper.Map(experienceUpdateDTOs, experience);
            _context.Experience.Update(experience);
            await _context.SaveChangesAsync();

            var experienceReadDTO = _mapper.Map<ExperienceReadDTOs>(experience);

            return Ok(experienceReadDTO);
        }

        
        [HttpPost]
        public async Task<ActionResult<Experience>> PostExperience(ExperienceCreateDTOs experienceCreateDTOs)
        {
          if (_context.Experience == null)
          {
              return Problem("Entity set 'ExperienceCreateDTOs'  is null.");
          }
            var experience = _mapper.Map<Experience>(experienceCreateDTOs);
            _context.Experience.Add(experience);
            await _context.SaveChangesAsync();
            var records = _mapper.Map<ExperienceReadDTOs>(experience);
            return CreatedAtAction("GetExperience", new { id = experience.info_id }, records);
        }

        // DELETE: api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            if (_context.Experience == null)
            {
                return NotFound();
            }
            var experience = await _context.Experience.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }

            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExperienceExists(int id)
        {
            return (_context.Experience?.Any(e => e.experience_id == id)).GetValueOrDefault();
        }
    }
}
