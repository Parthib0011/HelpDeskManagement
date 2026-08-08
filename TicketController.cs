using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;

namespace HelpDesk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _repository;

        public TicketController(ITicketRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTickets()
        {
            var tickets = await _repository.GetAllTicketsAsync();
            return Ok(tickets);
        }

        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(int id)
        {
            var ticket = await _repository.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        // POST: api/Ticket
        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }

            var createdId = await _repository.CreateTicketAsync(ticket);
            ticket.Id = createdId;

            return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
        }

        // PUT: api/Ticket/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }

            var existingTicket = await _repository.GetTicketByIdAsync(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            ticket.Id = id;
            await _repository.UpdateTicketAsync(ticket);
            return Ok(ticket);
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var existingTicket = await _repository.GetTicketByIdAsync(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            await _repository.DeleteTicketAsync(id);
            return Ok(new { message = "Ticket deleted successfully" });
        }

        // GET: api/Ticket/status/Open
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsByStatus(string status)
        {
            var tickets = await _repository.GetTicketsByStatusAsync(status);
            return Ok(tickets);
        }
    }
}
