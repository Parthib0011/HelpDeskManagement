using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelpDesk.Mvc.Models;
using HelpDesk.Mvc.Services;

namespace HelpDesk.Mvc.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: /Ticket/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var model = await _ticketService.GetDashboardDataAsync();
            return View(model);
        }

        // GET: /Ticket or /Ticket/Index
        public async Task<IActionResult> Index(string? status)
        {
            ViewBag.SelectedStatus = status ?? "All";
            var tickets = string.IsNullOrEmpty(status) || status == "All"
                ? await _ticketService.GetAllTicketsAsync()
                : await _ticketService.GetTicketsByStatusAsync(status);

            return View(tickets);
        }

        // GET: /Ticket/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // GET: /Ticket/Create
        public IActionResult Create()
        {
            var model = new TicketViewModel
            {
                Status = "Open", // Hardcoded to "Open" as specified
                CreatedDate = DateTime.Now
            };
            return View(model);
        }

        // POST: /Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel ticket)
        {
            // Enforce Status is "Open" on create
            ticket.Status = "Open";

            if (ModelState.IsValid)
            {
                var success = await _ticketService.CreateTicketAsync(ticket);
                if (success)
                {
                    TempData["SuccessMessage"] = "Ticket raised successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to create ticket via API.");
            }
            return View(ticket);
        }

        // GET: /Ticket/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: /Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketViewModel ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var success = await _ticketService.UpdateTicketAsync(id, ticket);
                if (success)
                {
                    TempData["SuccessMessage"] = "Ticket updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update ticket via API.");
            }
            return View(ticket);
        }

        // GET: /Ticket/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: /Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _ticketService.DeleteTicketAsync(id);
            if (success)
            {
                TempData["SuccessMessage"] = "Ticket deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete ticket.";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Ticket/Filter
        public async Task<IActionResult> Filter(string status)
        {
            return RedirectToAction(nameof(Index), new { status });
        }
    }
}
