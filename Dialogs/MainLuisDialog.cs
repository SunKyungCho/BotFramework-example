using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using BookingBot.Dialogs.SubDialogs;
using Microsoft.Bot.Connector;
using System.Threading;

namespace BookingBot.Dialogs
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class MainLuisDialog : BaseLuisDialog
    {
        public MainLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"], 
            ConfigurationManager.AppSettings["LuisAPIKey"], 
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"]))){
        }
		
		[LuisIntent("None")]
		public async Task None(IDialogContext context, LuisResult result)
		{
			await context.PostAsync("None");
			context.Done<object>(null);
		}

		[LuisIntent("MS.MeetingRoom.Book")]
		public async Task Book(IDialogContext context, LuisResult result)
		{
			await context.Forward(new BookDialog(result, this.service), base.ResumeAfterCallback, new Activity { Text = result.Query }, CancellationToken.None);
		}

		[LuisIntent("MS.MeetingRoom.CheckReservation")]
		public async Task CheckReservation(IDialogContext context, LuisResult result)
		{
			await context.Forward(new CheckReservationDialog(result, this.service), base.ResumeAfterCallback, new Activity { Text = result.Query }, CancellationToken.None);
        }

		private async Task ShowLuisResult(IDialogContext context, LuisResult result)
		{
			await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
			context.Wait(MessageReceived);
		}
	}
}