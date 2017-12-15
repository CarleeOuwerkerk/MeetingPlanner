using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingPlanner.Models
{
    public partial class Speaker
    {
        public int SpeakerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Subject { get; set; }

        public int MeetingId { get; set; }

        public Meeting SpeakerNavigation { get; set; }
    }
}
