using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBot
{
    [Serializable]
    public class MeetingRoom
    {
        public string MeetingRoomName{ get; set; }
        public string Hour{ get; set; }
        public string Duration{ get; set; }
    }
}