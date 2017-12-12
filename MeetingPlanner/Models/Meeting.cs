using System;
using System.Collections.Generic;

namespace MeetingPlanner.Models
{
    public partial class Meeting
    {
        public int MeetingId { get; set; }
        public string Conductor { get; set; }
        public string OpeningHymn { get; set; }
        public string OpeningPrayer { get; set; }
        public string SacramentHymn { get; set; }
        public string IntermediateHymn { get; set; }
        public string ClosingHymn { get; set; }
        public string ClosingPrayer { get; set; }
        public int Date { get; set; }

        public Speaker Speaker { get; set; }
    }
}
