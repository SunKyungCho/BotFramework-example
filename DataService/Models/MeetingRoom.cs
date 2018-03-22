using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataService.Models
{
	public class MeetingRoom
	{
		public int RoomId { get; set; }
		public string Name { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string BookedEmployee { get; set; }
	}
}