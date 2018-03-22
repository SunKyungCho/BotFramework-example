using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BookingBot.Dialogs.SubDialogs
{
	[Serializable]
	public class BookDialog : IDialog
	{

        private ILuisService service;
        private MeetingRoom meetingRoom { get; set; }

        [NonSerialized]
		LuisResult result = null;

        public BookDialog() { }
		public BookDialog(LuisResult result, ILuisService service)
		{
			this.result = result;
            this.service = service;
            this.meetingRoom = new MeetingRoom();
		}

        public async Task StartAsync(IDialogContext context)
		{
			context.Wait(this.MessageReceivedAsync);			
		}


        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> item)
		{
            var message = await item;
            var paramValue = message.Text;

            var result = await service.QueryAsync(paramValue.ToString(), context.CancellationToken);
            var queryIntent = result.Intents.FirstOrDefault();

            if (!"None".Equals(queryIntent.Intent, StringComparison.InvariantCultureIgnoreCase)
                && !"MS.MeetingRoom.Book".Equals(queryIntent.Intent, StringComparison.InvariantCultureIgnoreCase))
            {
                /*
                 * Intent가 None값이라면.. 질의에 대한 대답으로 판단.
                 * 새로운 Intent라면 시나리오 변경.
                 */
                QueryResult queryResult = new QueryResult(false)
                {
                    NewIntent = queryIntent.Intent
                };
                await context.PostAsync("진행중인 회의실 예약을 취소합니다.");
                context.Done<object>(message);
            }
            else
            {
            //회의실정보 있나 확인
            if (meetingRoom.Hour == null)
            {
                // Entity에서 Hour값 가져온다. 있다면..
                if (!AskHour(context, result))
                {
                    //시간정보가 없다 받아야 한다.
                    await context.PostAsync("몇시부터 사용하실 건가요?");
                    context.Wait(this.MessageReceivedAsync);
                }
            }

            if (meetingRoom.Hour != null)
            {
                //먼저 가능한 회의실을 찾아서 알려준다. 
                ReservationManagement management = new ReservationManagement();
                List<string> roomNameList = management.GetAvailableReservation(meetingRoom.Hour);

                if (roomNameList.Count > 0)
                {
                    string meetingRoomName;
                    //사용자의 입력에 meetingRooom entity가 존재하는지만 체크
                    if ( !GetUserInputMeetingRoomEntity(context, result, out meetingRoomName))
                    {
                        //회의실정보를 입력하지 않았다...받아야 한다.
                        var availableMeetingRoom = string.Join(", ", roomNameList.ToArray());
                        await context.PostAsync($"사용가능한 회의실은 {availableMeetingRoom} 입니다. 어떤 회의실을 예약할까요?");
                        context.Wait(this.MessageReceivedAsync);
                    }
                    else if(roomNameList.Contains(meetingRoomName))
                    {
                        meetingRoom.MeetingRoomName = meetingRoomName;
                    }
                }
                else
                {
                    meetingRoom.Hour = null;
                    await context.PostAsync("사용가능한 회의실이 없습니다. 다른 시간을 입력해주세요.");
                    context.Wait(this.MessageReceivedAsync);
                }
            }

            //회의실정보 있나 확인
            if (meetingRoom.Hour != null && meetingRoom.MeetingRoomName != null)
            {
                //회의실 예약
                ReservationManagement management = new ReservationManagement();
                management.SaveMeetingRoom(meetingRoom.Hour, meetingRoom.MeetingRoomName);
                await context.PostAsync($"{meetingRoom.MeetingRoomName} 회의실 {meetingRoom.Hour}에 예약 되었습니다.");
                context.Done<object>(meetingRoom);
            }
            }
        }

        public bool AskHour(IDialogContext context, LuisResult result)
		{
            var isHour = false;
            //시간 정보 있나 확인
            EntityRecommendation meetingRoomEntity;
            if (result.TryFindEntity("Hour", out meetingRoomEntity))
            {
                //MeetingRoom entity list의 값 가져오기.
                meetingRoomEntity.Type = "Hour";
                object hour;
                meetingRoomEntity.Resolution.TryGetValue("values", out hour);
                this.meetingRoom.Hour = (string)((List<object>)hour).FirstOrDefault();
                isHour = true;
            }
            return isHour;
        }

        public bool GetUserInputMeetingRoomEntity(IDialogContext context, LuisResult result, out string roomName)
        {
            var isMeetingRoom = false;
            roomName = "";
            EntityRecommendation meetingRoomEntity;
            if (result.TryFindEntity("MeetingRoom", out meetingRoomEntity))
            {
                meetingRoomEntity.Type = "MeetingRoom";
                object meetingRoomName;
                meetingRoomEntity.Resolution.TryGetValue("values", out meetingRoomName);
                roomName = (string)((List<object>)meetingRoomName).FirstOrDefault();
                isMeetingRoom = true;
            }
            return isMeetingRoom;
        }
    }
}