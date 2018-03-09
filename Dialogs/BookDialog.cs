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
        [NonSerialized]
        LuisResult result = null;

        public BookDialog() { }
        public BookDialog(LuisResult result)
        {
            this.result = result;
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            if (1 == 1) //entity 가 있다고 가정할 때
            {
                await context.PostAsync("몇시간 사용 하실 거에요?");
                context.Wait(AskHour);
            }
            else
            {
                await context.PostAsync("StartAsync in BookDialog");
                context.Done<object>(null);
            }

        }

        public async Task AskHour(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var rooms = await DummyGetMeetingRoom();

            if (rooms != null)
            {
                await context.PostAsync($"사용 가능한 회의실은 rooms 입니다. 어떤 회의실 할래요?");
                context.Wait(AskRoom);
            }
        }

        public async Task AskRoom(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;


            if (message.Text.ToLower() != "tokyo" && message.Text.ToLower() != "paris")
            {
                await context.PostAsync("다시 한번 입력해 주세요 Tokyo 와 Paris 중 어떤 회의실?");
                context.Wait(AskRoom);
            }
            else
            {
                if (await DummyAddRoom(1) > 0)
                {
                    await context.PostAsync($"message.Text 회의실로 예약 되었습니다.");
                }
                else
                {
                    await context.PostAsync($"message.Text 회의실로 예약중 오류!");
                }

                context.Done<object>(null);
            }
        }

        public async Task<string> DummyGetMeetingRoom()
        {
            return "Tokyo, Paris";
        }

        public async Task<int> DummyAddRoom(int roomid)
        {
            return 0;
        }

    }
}