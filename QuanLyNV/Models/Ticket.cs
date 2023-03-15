using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace QuanLyNV.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Log { get; set; }

        [Required]
        [EnumDataType(typeof(TicketState))]
        public TicketState State { get; set; }

        public string OwnerId { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public enum TicketState
    {
       FE,
       BE
    }
}
