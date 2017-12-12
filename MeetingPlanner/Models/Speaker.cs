using System;
using System.Collections.Generic;

namespace MeetingPlanner.Models
{
    public partial class Speaker
    {
        public int SpeakerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public int MeetingId { get; set; }

        public Meeting SpeakerNavigation { get; set; }
    }
}
