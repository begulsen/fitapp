using System;

namespace FitApp.Api.Exceptions
{
    public class ApiException
    {
        public static class BadRequestExceptionCodes
        {
            public static ushort ValueCannotBeNullOrEmptyException = 4000;
            public static ushort UserIdIsNotValidException = 4001;
            public static ushort UserIdIsNotExistException = 4002;
            public static ushort TrainingNameIsNotExistException = 4003;
            public static ushort SetIdIsNotExistException = 4004;
            public static ushort NutritionIdIsNotExistException = 4005;
            public static ushort MealNutritionIdIsNotExistException = 4006;
            public static ushort MealNameAlreadyExist = 4007;
            public static ushort MenuMustHave7Days = 4008;
            public static ushort MediaMaxSizeNotValidException = 4009;
            public static ushort MediaMinSizeNotValidException = 4010;
            public static ushort MediaExtensionNotValidException = 4011;
            public static ushort UserImageIsNotExistException = 4012;
            public static ushort MenuAlreadyExist = 4013;
            public static ushort ActivityCannotExistException = 4014;
            public static ushort UserPrivateTrainingIsNotExistException = 4015;
            public static ushort ImageCountCannotBeMoreThanThreeException = 4016;
            public static ushort UserPrivateDietIsNotExistException = 4017;
            public static ushort UserExist = 4018;
            public static ushort UserNotExist = 4019;
        }
        
        public abstract class BadRequestException : Exception
        {
            protected BadRequestException(string message) : base(message) { }
            public abstract ushort Code { get; }
        }
        
        public abstract class ConflictException : Exception
        {
            protected ConflictException(string message) : base(message) { }
            public abstract ushort Code { get; }
        }
        
        public class ValueCannotBeNullOrEmptyException : BadRequestException
        {
            public ValueCannotBeNullOrEmptyException(string value) : base(value + " cannot be null or empty!") { }
            public override ushort Code => BadRequestExceptionCodes.ValueCannotBeNullOrEmptyException;
        }
        
        public class UserIdIsNotValidException : BadRequestException
        {
            public UserIdIsNotValidException(string value) : base(value + " is not valid!") { }
            public override ushort Code => BadRequestExceptionCodes.UserIdIsNotValidException;
        }
        
        public class UserIdIsNotExistException : BadRequestException
        {
            public UserIdIsNotExistException(string value) : base(value + " is not exist!") { }
            public override ushort Code => BadRequestExceptionCodes.UserIdIsNotExistException;
        }
        
        public class UserExist : ConflictException
        {
            public UserExist(string value) : base(value + " is exist!") { }
            public override ushort Code => BadRequestExceptionCodes.UserExist;
        }
        
        public class UserNotExist : ConflictException
        {
            public UserNotExist(string value) : base(value + " is not exist!") { }
            public override ushort Code => BadRequestExceptionCodes.UserNotExist;
        }
        
        public class SetIdIsNotExistException : BadRequestException
        {
            public SetIdIsNotExistException(string value) : base(value + " is not exist!") { }
            public override ushort Code => BadRequestExceptionCodes.SetIdIsNotExistException;
        }
        
        public class ActivityCannotExistException : BadRequestException
        {
            public ActivityCannotExistException(Guid value) : base("Activity is not exist with this id = " + value) { }
            public override ushort Code => BadRequestExceptionCodes.ActivityCannotExistException;
        }
        
        
        public class NutritionIdIsNotExistException : BadRequestException
        {
            public NutritionIdIsNotExistException(string value) : base(value + " is not exist!") { }
            public override ushort Code => BadRequestExceptionCodes.NutritionIdIsNotExistException;
        }
        
        public class MealNutritionIdIsNotExistException : BadRequestException
        {
            public MealNutritionIdIsNotExistException(string value) : base(value + " is not exist!") { }
            public override ushort Code => BadRequestExceptionCodes.MealNutritionIdIsNotExistException;
        }

        public class TrainingNameIsNotExistException : BadRequestException
        {
            public TrainingNameIsNotExistException(string trainingName) : base(trainingName + " is not exist!") { }
            public override ushort Code => BadRequestExceptionCodes.TrainingNameIsNotExistException;
        }
        
        public class MealNameAlreadyExist : ConflictException
        {
            public MealNameAlreadyExist(string mealName) : base(mealName + " is already exist!") { }
            public override ushort Code => BadRequestExceptionCodes.MealNameAlreadyExist;
        }

        public class MenuMustHave7Days : BadRequestException
        {
            public MenuMustHave7Days(int dayCount) : base("WeekDays must be 7. DayCount is " + dayCount) { }
            public override ushort Code => BadRequestExceptionCodes.MenuMustHave7Days;
        }
        
        public class MenuAlreadyExist : BadRequestException
        {
            public MenuAlreadyExist(string menuName) : base("Menu already exist with name = " + menuName) { }
            public override ushort Code => BadRequestExceptionCodes.MenuAlreadyExist;
        }
        
        public class MediaMaxSizeNotValidException : BadRequestException
        {
            public MediaMaxSizeNotValidException() : base("Media size is not valid! Maximum 10MB is allowed.") { }
            public override ushort Code => BadRequestExceptionCodes.MediaMaxSizeNotValidException;
        }
        
        public class MediaMinSizeNotValidException : BadRequestException
        {
            public MediaMinSizeNotValidException() : base("Media size is not valid! Minimum 10KB is allowed.") { }
            public override ushort Code => BadRequestExceptionCodes.MediaMinSizeNotValidException;
        }

        public class MediaExtensionNotValidException : BadRequestException
        {
            public MediaExtensionNotValidException() : base("Media extension is not valid! Jpeg and png is allowed.") { }
            public override ushort Code => BadRequestExceptionCodes.MediaExtensionNotValidException;
        }
        
        public class UserImageIsNotExistException : BadRequestException
        {
            public UserImageIsNotExistException() : base("User image did not exist!") { }
            public override ushort Code => BadRequestExceptionCodes.UserImageIsNotExistException;
        }
        
        public class ImageCountCannotBeMoreThanThreeException : BadRequestException
        {
            public ImageCountCannotBeMoreThanThreeException() : base("Image count cannot be more than three!") { }
            public override ushort Code => BadRequestExceptionCodes.ImageCountCannotBeMoreThanThreeException;
        }

        public class UserPrivateTrainingIsNotExistException : BadRequestException
        {
            public UserPrivateTrainingIsNotExistException(Guid userId) : base("User private training data is not exist with this customer id = " + userId) { }
            public override ushort Code => BadRequestExceptionCodes.UserPrivateTrainingIsNotExistException;
        }
        
        public class UserPrivateDietIsNotExistException : BadRequestException
        {
            public UserPrivateDietIsNotExistException(Guid userId) : base("User private diet data is not exist with this customer id = " + userId) { }
            public override ushort Code => BadRequestExceptionCodes.UserPrivateDietIsNotExistException;
        }
    }
}