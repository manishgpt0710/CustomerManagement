using CustomerManagement.Data.Models;
using CustomerManagement.WebApi.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagement.Controllers;

[ApiController]
[Route("api/customer/{customerId}/[controller]")]
public class ContactInfosController : ControllerBase
{
    private readonly IContactInfoService _service;
    private readonly ILogger<ContactInfosController> _logger;
    public ContactInfosController(IContactInfoService service, ILogger<ContactInfosController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET: api/contactInfos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactInfo>>> GetContactInfos(long customerId)
    {
        var _contactInfos = await _service.GetAllAsync(customerId);
        return Ok(_contactInfos);
    }

    // GET: api/contactInfos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactInfo>> GetContactInfo(long id)
    {
        var contactInfo = await _service.GetByIdAsync(id);
        if (contactInfo == null)
        {
            return NotFound();
        }
        return Ok(contactInfo);
    }

    // POST: api/contactInfos
    [HttpPost]
    public async Task<ActionResult<ContactInfo>> CreateContactInfo(ContactInfo contactInfo)
    {
        await _service.AddAsync(contactInfo);
        return CreatedAtAction(nameof(GetContactInfo), new { id = contactInfo.Id }, contactInfo);
    }

    // PUT: api/contactInfos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContactInfo(long id, ContactInfo updatedContactInfo)
    {
        var contactInfo = await _service.GetByIdAsync(id);
        if (contactInfo == null)
        {
            return NotFound();
        }
        updatedContactInfo.Id = contactInfo.Id;
        await _service.UpdateAsync(updatedContactInfo);
        return NoContent();
    }

    // DELETE: api/contactInfos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactInfo(long id)
    {
        var contactInfo = await _service.GetByIdAsync(id);
        if (contactInfo == null)
        {
            return NotFound();
        }
        await _service.DeleteAsync(contactInfo);
        return NoContent();
    }
}
