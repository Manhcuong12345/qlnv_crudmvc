using QuanLyNV.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace QuanLyNV.Dto
{
    public class TicketDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

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
