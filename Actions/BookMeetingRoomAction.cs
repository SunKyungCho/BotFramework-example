using Microsoft.Cognitive.LUIS.ActionBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LuisBot.Actions
{
    [Serializable]
    [LuisActionBinding("MS.MeetingRoom.Book", FriendlyName = "Register dayoff")]
    public class BookMeetingRoomAction : BaseLuisAction
    {

        [Required(ErrorMessage = "몇시부터 사용할 예정이신가요?")]
        //[Time(ErrorMessage ="asdfasdfasdf")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "몇시간 사용하실 건가요?")]
        //[Time]
        //[LuisActionBindingParam(BuiltinType = BuiltInGeographyTypes.City, Order = 1)]
        [LuisActionBindingParam(CustomType = "week", Order = 2)]
        public string Duration { get; set; }


        [Required(ErrorMessage = "사용할 회의실은 어디인가요?")]
        //[LuisActionBindingParam(BuiltinType = BuiltInGeographyTypes.City, Order = 1)]
        //[LuisActionBindingParam(CustomType = "MeetingRoom", Order = 3)]
        public string MeetinRoom { get; set; }

        public override Task<object> FulfillAsync()
        {
            //return Task.FromResult((object)$"Sorry, there are no {this.DayOffType} your chosen dates ({this.Dayoff})");
            return Task.FromResult((object)$"알겠어 예약 해줄게");
        }
    }
}