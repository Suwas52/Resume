using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Data;

using Resume.DTOs.EducationDTOs;
using Resume.Models;
using Resume.Helpers;

namespace Resume.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResumeContext _context;

        public EducationsController(IMapper mapper, ResumeContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<ActionResult<List<Education>>> GetEducation()
        {
          if (_context.Education == null)
          {
              return NotFound();
          }
            var education = await _context.Education.ToListAsync();
            var result = _mapper.Map<List<EducationReadDTOs>>(education);
            return Ok(result);
        }


        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Education>>> GetEducation(int id)
        {
          if (_context.Education == null)
          {
              return NotFound();
          }
            var education = await _context.Education.Where(c => c.info_id == id).ToListAsync();

            if (education == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<List<EducationReadDTOs>>(education);

            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducation(int id, EducationUpdateDTOs educationUpdateDTOs)
        {
            if (_context.Education == null)
            {
                return NotFound($"wrongs");
            }

            var education = await _context.Education.Where(c => c.info_id == id && c.education_id == educationUpdateDTOs.education_id).FirstOrDefaultAsync();


            if (education == null)
            {
                return NotFound($"Education with ID {id} not found.");
            }

            _mapper.Map(educationUpdateDTOs, education);
            _context.Education.Update(education);
            await _context.SaveChangesAsync();

            var educationRead = _mapper.Map<EducationReadDTOs>(education);

            return Ok(educationRead);
        }

        
        [HttpPost]
        public async Task<ActionResult<Education>> PostEducation(EducationCreateDTOs educationCreateDTOs)
        {
          if (educationCreateDTOs == null)
          {
              return Problem("Entity set 'educationCreateDTOs'  is null.");
            }

            if (_context.Education == null)
            {
                return NotFound();
            }

            var education = _mapper.Map<Education>(educationCreateDTOs);

            _context.Education.Add(education);
            await _context.SaveChangesAsync();
            var records = _mapper.Map<EducationReadDTOs>(education);
            return CreatedAtAction("GetEducation", new { id = education.info_id }, records);

            
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            if (_context.Education == null)
            {
                return NotFound();
            }
            var education = await _context.Education.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }

            _context.Education.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationExists(int id)
        {
            return (_context.Education?.Any(e => e.education_id == id)).GetValueOrDefault();
        }
    }
}
