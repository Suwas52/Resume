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
using System.Configuration;
using Resume.DTOs.ContactDTOs;


namespace Resume.APIControllers
{
    [APIKeyAuth]

    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResumeContext _context;

        public ContactsController(IMapper mapper,  ResumeContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetContact()
        {
          if (_context.Contact == null)
          {
              return NotFound();
          }
          var contacts = await _context.Contact.ToListAsync(); 
          var result = _mapper.Map<List<ContactReadDTOs>>(contacts);
           return Ok(result);
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Contact>>> GetContact(int id)
        {
          if (_context.Contact == null)
          {
              return NotFound();
          }
            var contact = await _context.Contact.Where(c => c.info_id == id).ToListAsync();

            if (contact == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<List<ContactReadDTOs>>(contact);

            return Ok(result);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, ContactUpdateDTOs contactUpdateDTOs)
        {

            var contact = await _context.Contact.Where(c => c.info_id == id && c.contact_id == contactUpdateDTOs.contact_id).FirstOrDefaultAsync();

            if (contact == null)
            {
                return NotFound($"Contact with ID {id} not found.");
            }

            
            _mapper.Map(contactUpdateDTOs, contact);
            _context.Contact.Update(contact);
            await _context.SaveChangesAsync();

            var contactReadDTO = _mapper.Map<ContactReadDTOs>(contact);

            return Ok(contactReadDTO);

        }

        
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(ContactCreateDTOs contactCreateDTOs)
        {
           if (contactCreateDTOs == null)
            {
              return Problem("Entity set 'contactCreateDTOs'  is null.");
            }

            

            var contacts = _mapper.Map<Contact>(contactCreateDTOs);
            _context.Contact.Add(contacts);
            await _context.SaveChangesAsync();
            var records = _mapper.Map<ContactReadDTOs>(contacts);
            return CreatedAtAction("GetContact", new { id = contacts.info_id }, records);

          
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (_context.Contact == null)
            {
                return NotFound();
            }
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return (_context.Contact?.Any(e => e.contact_id == id)).GetValueOrDefault();
        }
    }
}
