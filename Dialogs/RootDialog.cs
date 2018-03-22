using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace BookingBot
{
    [LuisModel("", "")]
    [Serializable]
    public class RootDialog : BasicLuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("MS.MeetingRoom.Book")]
        public async Task MeetingRoom(IDialogContext context, LuisResult result)
        {

            await context.PostAsync("Hi! Try asking me things like 'search hotels in Seattle', 'search hotels near LAX airport' or 'show me the reviews of The Bot Resort'");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("MS.MeetingRoom.CheckReservastion")]
        public async Task CheckReservastion(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            string date = null;
            string time = null;
            if (result.Entities.Count > 0)
            {
                for (int i = 0; i <= result.Entities.Count; i++)
                {
                    if (result.Entities[i].Type == "Date")
                    {
                        date = result.Entities[i].Entity;
                    }
                    else if (result.Entities[i].Type == "Time")
                    {
                        time = result.Entities[i].Entity;
                    }
                }
                String roomCheck = CheckRoom(date, time);
                await context.PostAsync($"{roomCheck}");
            }
            await context.PostAsync($"");

            context.Wait(this.MessageReceived);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }
    }
}