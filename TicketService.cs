using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Mvc.Models;

namespace HelpDesk.Mvc.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TicketViewModel>> GetAllTicketsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TicketViewModel>>("api/Ticket");
                return response ?? new List<TicketViewModel>();
            }
            catch
            {
                return new List<TicketViewModel>();
            }
        }

        public async Task<TicketViewModel?> GetTicketByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<TicketViewModel>($"api/Ticket/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateTicketAsync(TicketViewModel ticket)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Ticket", ticket);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateTicketAsync(int id, TicketViewModel ticket)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Ticket/{id}", ticket);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Ticket/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TicketViewModel>> GetTicketsByStatusAsync(string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(status) || status.Equals("All", StringComparison.OrdinalIgnoreCase))
                {
                    return await GetAllTicketsAsync();
                }

                var response = await _httpClient.GetFromJsonAsync<List<TicketViewModel>>($"api/Ticket/status/{status}");
                return response ?? new List<TicketViewModel>();
            }
            catch
            {
                return new List<TicketViewModel>();
            }
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var tickets = await GetAllTicketsAsync();
            return new DashboardViewModel
            {
                TotalTickets = tickets.Count,
                OpenTickets = tickets.Count(t => string.Equals(t.Status, "Open", StringComparison.OrdinalIgnoreCase)),
                InProgressTickets = tickets.Count(t => string.Equals(t.Status, "In Progress", StringComparison.OrdinalIgnoreCase)),
                ClosedTickets = tickets.Count(t => string.Equals(t.Status, "Closed", StringComparison.OrdinalIgnoreCase))
            };
        }
    }
}
