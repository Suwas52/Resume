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
using Resume.DTOs.SkillDTOs;

namespace Resume.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResumeContext _context;

        public SkillsController(IMapper mapper, ResumeContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Skill>>> GetSkill()
        {
          if (_context.Skill == null)
          {
              return NotFound();
          }
            var skill = await _context.Skill.ToListAsync();
            var result = _mapper.Map<List<SkillReadDTOs>>(skill);
            return Ok(result);
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(int id)
        {
          if (_context.Skill == null)
          {
              return NotFound();
          }
            var skill = await _context.Skill.Where(c => c.info_id == id).ToListAsync();

            if (skill == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<List<SkillReadDTOs>>(skill);

            return Ok(result);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(int id, SkillUpdateDTOs skillUpdateDTOs)
        {
            if (_context.Skill == null)
            {
                return NotFound();
            }

            var skill = await _context.Skill.Where(c => c.info_id == id && c.skill_id == skillUpdateDTOs.skill_id).FirstOrDefaultAsync();

            if (skill == null)
            {
                return NotFound($"skill with ID {id} not found.");
            }


            _mapper.Map(skillUpdateDTOs, skill);
            _context.Skill.Update(skill);
            await _context.SaveChangesAsync();

            var skillReadDTO = _mapper.Map<SkillReadDTOs>(skill);

            return Ok(skillReadDTO);
        }

        
        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill(SkillCreateDTOs skillCreateDTOs)
        {
            if (skillCreateDTOs == null)
            {
                return Problem("Entity set 'skillCreateDTOs'  is null.");
            }

            if (_context.Skill == null)
            {
                return NotFound();
            }


            var skill = _mapper.Map<Skill>(skillCreateDTOs);
            _context.Skill.Add(skill);
            await _context.SaveChangesAsync();
            var records = _mapper.Map<SkillReadDTOs>(skill);
            return CreatedAtAction("GetSkill", new { id = skill.info_id }, records);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            if (_context.Skill == null)
            {
                return NotFound();
            }
            var skill = await _context.Skill.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skill.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkillExists(int id)
        {
            return (_context.Skill?.Any(e => e.skill_id == id)).GetValueOrDefault();
        }
    }
}
