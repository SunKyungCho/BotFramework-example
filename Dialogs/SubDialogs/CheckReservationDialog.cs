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
	public class CheckReservationDialog : IDialog
	{

        private ILuisService service;

        public CheckReservationDialog() { }
		public CheckReservationDialog(LuisResult result, ILuisService service)
		{
			this.result = result;
            this.service = service;
		}

		[NonSerialized]
		LuisResult result = null;

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

            ReservationManagement management = new ReservationManagement();
            List<string> roomNameList = management.GetAvailableReservation("1시");
            if (roomNameList.Count > 0)
            {
                string meetingRoomName;
                //사용자의 입력에 meetingRooom entity가 존재하는지만 체크
                var availableMeetingRoom = string.Join(", ", roomNameList.ToArray());
                await context.PostAsync($"사용가능한 회의실은 {availableMeetingRoom} 입니다.");
                context.Wait(this.MessageReceivedAsync);
            }
            else
            {
                await context.PostAsync($"해당 시간에 사용 가능한 회의실이 없습니다.");
                context.Done<object>(null);
            }
		}
	}
}