using ContactApp.Data;
using Microsoft.AspNetCore.Mvc;
using ContactApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly ContactAPIDbContext dbContext;
        public ContactController(ContactAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
            
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if(contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddReq addRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addRequest.Address,
                Email = addRequest.Email,
                Name = addRequest.Name,
                Phone = addRequest.Phone,
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateReq updateRequest)
        {
           var contact = await dbContext.Contacts.FindAsync(id);

           if (contact != null)
            {
                contact.Name = updateRequest.Name;
                contact.Email = updateRequest.Email;
                contact.Phone = updateRequest.Phone;
                contact.Address = updateRequest.Address;

                await dbContext.SaveChangesAsync();

                return Ok(contact);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if(contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }
    }
}
