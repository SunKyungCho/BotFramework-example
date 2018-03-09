using System;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using LuisBot.Actions;
using Microsoft.Cognitive.LUIS.ActionBinding.Bot;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Dialogs;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class RootDialog : LuisActionDialog<object>
    {
        public RootDialog() : base(
            new Assembly[] { typeof(BookMeetingRoomAction).Assembly },
            (action, context) =>
            {
                //if (action is RegisterAction)
                //{
                //    (action as RegisterAction).Dayoff = DateTime.Today;
                //}
            },
            new LuisService(new LuisModelAttribute(
                ConfigurationManager.AppSettings["LuisAppId"],
                ConfigurationManager.AppSettings["LuisAPIKey"],
                domain: ConfigurationManager.AppSettings["LuisAPIHostName"])
))
        {
        }
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"미안해 그게 무슨말인지 모르겠어");
            context.Wait(MessageReceived);
        }

        [LuisIntent("FindHotels")]
        [LuisIntent("TimeInPlace")]
        public async Task TestIntentActionResultHandlerAsync(IDialogContext context, object actionResult)
        {
            // we know these actions return a string for their related intents,
            // although you could have individual handlers for each intent
            var message = context.MakeMessage();

            message.Text = actionResult != null ? actionResult.ToString() : "Cannot resolve your query";

            await context.PostAsync(message);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "Gretting" with the name of your newly created intent in the following handler
        [LuisIntent("MS.MeetingRoom.Book")]
        //public async Task GreetingIntent(IDialogContext context, LuisResult result)
        public async Task GreetingIntent(IDialogContext context, object actionResult)
        {
            var message = context.MakeMessage();
            message.Text = actionResult != null ? actionResult.ToString() : "무슨말인지 모르겠어";
            await context.PostAsync(message);
            //await this.ShowLuisResult(context, result);
        }
        [LuisIntent("Queryjet.DayOff.Register")]
        public async Task IntentActionResultHandlerAsync(IDialogContext context, object actionResult)
        {
            //// we know these actions return a string for their related intents,
            //// although you could have individual handlers for each intent
            //await context.Forward(new RegisterDialog(result), base.ResumeAfterCallback, new Activity { Text = result.Query }, CancellationToken.None);
            var message = context.MakeMessage();
            message.Text = actionResult != null ? actionResult.ToString() : "무슨말인지 모르겠어";
            await context.PostAsync(message);
        }

        [LuisIntent("Queryjet.DayOff.Suggestion")]
        public async Task CancelIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"휴가 사용할래?");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Queryjet.DayOff.Check")]
        public async Task HelpIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
            context.Wait(MessageReceived);
        }
    }
}