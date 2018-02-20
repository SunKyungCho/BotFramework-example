using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [LuisModel("1460f92e-927c-4081-afbc-b393c28221ff", "2c6b8197c11e48c39b8ebb3277a7c69c")]
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        //public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            //ConfigurationManager.AppSettings["LuisAppId"], 
            //ConfigurationManager.AppSettings["LuisAPIKey"], 
            //domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        //{
        //}
          
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, I don't know what you wanted.");
            context.Wait(MessageReceived);
        }
        [LuisIntent("CheckWeather")]
        public async Task WeatherIntent(IDialogContext context, LuisResult result)
        {
            await this.CheckWeatherLuisResult(context, result);
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "Gretting" with the name of your newly created intent in the following handler
        [LuisIntent("Greeting")]
        public async Task GreetingIntent(IDialogContext context, LuisResult result)
        {
            await this.GreetingLuisResult(context, result);
        }

        [LuisIntent("Cancel")]
        public async Task CancelIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Help")]
        public async Task HelpIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

       private async Task ShowLuisResult(IDialogContext context, LuisResult result) 
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
            context.Wait(MessageReceived);
        }

        private async Task CheckWeatherLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("¹» ¹°¾î~ °Ü¿ïÀÌ´Ï±î ´ç¿¬È÷ Ãä´Ù.");
            context.Wait(MessageReceived);
        }

        private async Task GreetingLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("¾È³ç~ ¹Ý°©´Ù.");
            context.Wait(MessageReceived);
        }
    }
}