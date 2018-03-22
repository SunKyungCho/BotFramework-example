using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BookingBot
{
    [Serializable]
    public class BasicLuisDialog<T> : LuisDialog<T>
    {
        public string CheckRoom(string date, string time)
        {
            String room = "Seoul과 Paris가 가능합니다";
            return room;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as IMessageActivity;

            // TODO: Put logic for handling user message here

            context.Wait(MessageReceivedAsync);
        }
    }
}