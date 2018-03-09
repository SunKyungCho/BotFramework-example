using System;
using System.ComponentModel.DataAnnotations;

namespace LuisBot.Actions
{
    public class DateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // TODO: Actually validate location using Bing
            return value == null || ((string)value).Length >= 3;
        }
    }
}