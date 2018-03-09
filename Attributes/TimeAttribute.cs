using System;
using System.ComponentModel.DataAnnotations;

namespace LuisBot.Actions
{
    public class TimeAttribute : ValidationAttribute
    {
        private const string DefaultTimeErrorMessage = "정확한 시간정보를 다시 한번 입력해 주세요.";
        private const string DefaultErrorMessage = "몇시부터 사용하실 건가요?";

        public string TimeErrorMessage { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (CheckValidTime(value.ToString()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(TimeErrorMessage ?? DefaultTimeErrorMessage);
                }
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? DefaultErrorMessage);
            }
        }

        protected bool CheckValidTime(object value)
        {
            var str = value.ToString();

            System.Text.RegularExpressions.Regex searchTerm =
            new System.Text.RegularExpressions.Regex(@"(오전[ ]?[0-9]+시)");

            var matches = searchTerm.Matches(str);

            if (matches.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}