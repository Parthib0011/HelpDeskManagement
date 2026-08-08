using Microsoft.EntityFrameworkCore;
using HelpDesk.Api.Models;

namespace HelpDesk.Api.Data
{
    public class HelpDeskDbContext : DbContext
    {
        public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data for testing/demo
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    Title = "VPN Access Issue",
                    Description = "Unable to connect to internal VPN server from home network.",
                    Priority = "High",
                    Status = "Open",
                    RaisedBy = "John Doe",
                    CreatedDate = System.DateTime.Now.AddDays(-2)
                },
                new Ticket
                {
                    Id = 2,
                    Title = "Software License Request",
                    Description = "Requesting license for Visual Studio Enterprise edition.",
                    Priority = "Medium",
                    Status = "In Progress",
                    RaisedBy = "Jane Smith",
                    CreatedDate = System.DateTime.Now.AddDays(-1)
                },
                new Ticket
                {
                    Id = 3,
                    Title = "Monitor Flicker Fix",
                    Description = "Secondary monitor flickers randomly when connected via HDMI.",
                    Priority = "Low",
                    Status = "Closed",
                    RaisedBy = "Alice Johnson",
                    CreatedDate = System.DateTime.Now.AddDays(-5)
                }
            );
        }
    }
}
