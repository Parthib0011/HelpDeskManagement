using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Priority { get; set; } = string.Empty; // Valid values: Low, Medium, High

        [Required]
        public string Status { get; set; } = string.Empty; // Valid values: Open, In Progress, Closed

        [Required]
        [StringLength(100)]
        public string RaisedBy { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
