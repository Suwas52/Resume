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
using Resume.DTOs.ProjectDTOs;

namespace Resume.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResumeContext _context;

        public ProjectsController(IMapper mapper, ResumeContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProject()
        {
          if (_context.Project == null)
          {
              return NotFound();
          }
            var project = await _context.Project.ToListAsync();
            var result = _mapper.Map<List<ProjectReadDTOs>>(project);
            return Ok(result);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Project>>> GetProject(int id)
        {
          if (_context.Project == null)
          {
              return NotFound();
          }
            var project = await _context.Project.Where(c => c.info_id == id).ToListAsync();

            if (project == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<List<ProjectReadDTOs>>(project);

            return Ok(result);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectUpdateDTOs projectUpdateDTOs)
        {
            if (_context.Project == null)
            {
                return NotFound();
            }

            var project = await _context.Project.Where(c => c.info_id == id && c.project_id == projectUpdateDTOs.project_id).FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }


            _mapper.Map(projectUpdateDTOs, project);
            _context.Project.Update(project);
            await _context.SaveChangesAsync();

            var projectReadDTO = _mapper.Map<ProjectReadDTOs>(project);

            return Ok(projectReadDTO);
        }

       
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(ProjectCreateDTOs projectCreateDTOs)
        {
            if (projectCreateDTOs == null)
            {
                return Problem("Entity set 'projectCreateDTOs'  is null.");
            }

            if (_context.Project == null)
            {
                return NotFound();
            }

            var project = _mapper.Map<Project>(projectCreateDTOs);
            _context.Project.Add(project);
            await _context.SaveChangesAsync();
            var records = _mapper.Map<ProjectReadDTOs>(project);
            return CreatedAtAction("GetProject", new { id = project.info_id }, records);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (_context.Project == null)
            {
                return NotFound();
            }
            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return (_context.Project?.Any(e => e.project_id == id)).GetValueOrDefault();
        }
    }
}
