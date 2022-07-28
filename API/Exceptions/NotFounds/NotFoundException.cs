using System;

namespace API.Extensions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(object value)
        {
            Value = value;
        }

        public int Status { get; set; } = 404;

        public object Value { get; set; }
    }
}