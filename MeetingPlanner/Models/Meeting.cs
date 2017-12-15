using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingPlanner.Models
{
    public partial class Meeting
    {
        public int MeetingId { get; set; }

        [Required]
        public string Conductor { get; set; }

        [Required]
        public string OpeningHymn { get; set; }

        [Required]
        public string OpeningPrayer { get; set; }

        [Required]
        public string SacramentHymn { get; set; }

        public string IntermediateHymn { get; set; }

        [Required]
        public string ClosingHymn { get; set; }

        [Required]
        public string ClosingPrayer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public Speaker Speaker { get; set; }
    }
}
