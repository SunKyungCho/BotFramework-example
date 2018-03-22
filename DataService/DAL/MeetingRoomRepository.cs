using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService.Models;

namespace DataService.DAL
{
	public class MeetingRoomRepository : IMeetingRoomRepository
	{
		public int AddBook(MeetingRoom room)
		{
			throw new NotImplementedException();
		}

		public int DeleteBook(int roomId)
		{
			throw new NotImplementedException();
		}

		public MeetingRoom GetMeetingRoom(string name)
		{
			throw new NotImplementedException();
		}

		public List<MeetingRoom> GetMeetingRooms(DateTime dateTime)
		{
			throw new NotImplementedException();
		}

		public int UpdateBook(MeetingRoom room)
		{
			throw new NotImplementedException();
		}
	}
}