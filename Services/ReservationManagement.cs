using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookingBot
{
    public class ReservationManagement
    {
        private string connectionString = "Server=tcp:queryjetchatbot.database.windows.net,1433;Initial Catalog=chatbot-db;Persist Security Info=False;User ID=sunkyung;Password=Qlalf#21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<string> GetAvailableReservation(string hour)
        {
            List<string> roomList = new List<string>();
            roomList.Add("Tokyo");
            roomList.Add("Seoul");
            roomList.Add("Paris");

            string queryString =
                "SELECT MeetingRoomName from dbo.MeetingRoom_reservation "
                    + "WHERE Hour = @Hour";

            MeetingRoom meetingRoom = new MeetingRoom();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Hour", hour);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var meetingRoomName = (string)reader[0];
                        roomList.Remove(meetingRoomName);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return roomList;
        }

        public MeetingRoom GetReservation(string hour, string meetingRoomName)
        {
            string queryString =
                "SELECT MeetingRoomName, Hour from dbo.MeetingRoom_reservation "
                    + "WHERE Hour = @Hour";

            MeetingRoom meetingRoom = new MeetingRoom();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Hour", hour);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        meetingRoom.MeetingRoomName = (string)reader[0];
                        meetingRoom.Hour = (string)reader[1];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return meetingRoom;
        }

        public bool SaveMeetingRoom(string hour, string meetingRoomName)
        {
            var result = true;
            string queryString =
                "insert into dbo.MeetingRoom_reservation (MeetingRoomName, Hour) values (@MeetingRoomName, @Hour)";

            MeetingRoom meetingRoom = new MeetingRoom();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Hour", hour);
                command.Parameters.AddWithValue("@MeetingRoomName", meetingRoomName);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result = false;
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }
    }
}