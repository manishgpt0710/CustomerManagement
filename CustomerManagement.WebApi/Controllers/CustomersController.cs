using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Data.Models;
using CustomerManagement.WebApi.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;
    private readonly ILogger<CustomersController> _logger;
    public CustomersController(ICustomerService service, ILogger<CustomersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET: api/customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        var _customers = await _service.GetAllCustomersAsync();
        return Ok(_customers);
    }

    // GET: api/customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(long id)
    {
        var customer = await _service.GetCustomerByIdAsync(id, true);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    // POST: api/customers
    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
    {
        await _service.AddCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    // PUT: api/customers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(long id, Customer updatedCustomer)
    {
        var customer = await _service.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        updatedCustomer.Id = customer.Id;
        await _service.UpdateCustomerAsync(updatedCustomer);
        return NoContent();
    }

    // DELETE: api/customers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(long id)
    {
        var customer = await _service.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        await _service.DeleteCustomerAsync(customer);
        return NoContent();
    }
}
