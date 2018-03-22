using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Models;

namespace DataService.DAL
{
	public interface IMeetingRoomRepository
	{
		List<MeetingRoom> GetMeetingRooms(DateTime dateTime);

		MeetingRoom GetMeetingRoom(string name);

		int AddBook(MeetingRoom room);

		int DeleteBook(int roomId);

		int UpdateBook(MeetingRoom room);
	}
}
