using System;

namespace Firm_Control
{
    class ListException : Exception
    {
        public ListException()
        { }
        public ListException(string msg) : base(msg)
        { }
    }

    class NoElement : ListException
    {
        public NoElement(string msg) : base(msg)
        { }
    }
}