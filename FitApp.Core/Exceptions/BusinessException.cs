using System;

namespace FitApp.Core.Exceptions
{
    public static class BusinessExceptionCodes
    {
        public static ushort UserIsNotExistException = 1000;
    }
    public abstract class BusinessException : Exception
    {
        public abstract ushort Code { get;}
        protected BusinessException(string message) : base(message) { }
    }
    
    public class UserIsNotExistException : BusinessException
    {
        public UserIsNotExistException(string customerId) : base("User is not exist with customerID = " + customerId) { }
        public override ushort Code => BusinessExceptionCodes.UserIsNotExistException;
    }
}