using System;
using System.ComponentModel.DataAnnotations;

namespace LuisBot.Actions
{
    public class MeetingRoomAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // TODO: Actually validate location using Bing
            return value == null || ((string)value).Length >= 3;
        }

        public string FileNotFoundMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            // TODO: 여기서 회의실 조회하고. 있는 회의실 알려주자. 
            // context 등의 상위값을 가져올수 있나? 확인 필요.
            if (value != null)
            {
                return new ValidationResult(value.ToString());
            }
            else
            {
                return new ValidationResult(value.ToString());
                //return new ValidationResult(ErrorMessage ??
                //                            DefaultErrorMessage);
            }
        }
    }
}

//private const string DefaultFileNotFoundMessage =
//    "Sorry but there is already an image with this name please rename your image";

//private const string DefaultErrorMessage =
//    "Please enter a name for your image";

//public string FileNotFoundMessage { get; set; }

//protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//{
//    if (value != null)
//    {
//        string fileName = value.ToString();
//        if (FileExists(fileName))
//        {
//            return new ValidationResult(FileNotFoundMessage ??
//                                        DefaultFileNotFoundMessage);
//        }
//        else
//        {
//            return ValidationResult.Success;
//        }
//    }
//    else
//    {
//        return new ValidationResult(ErrorMessage ??
//                                    DefaultErrorMessage);
//    }
//}