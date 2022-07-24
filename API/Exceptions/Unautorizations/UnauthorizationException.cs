using System;

namespace API.Exceptions.Unautorizations
{
    public class UnauthorizationException : Exception
    {
        public UnauthorizationException()
        {
            
        }
        public UnauthorizationException(object value)
        {
            Value = value;
        }
        public int Status { get; set; } = 401;

        public object Value { get; set; }
    }
}