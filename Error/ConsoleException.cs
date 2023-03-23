using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fileImporter.Error
{
    public class ConsoleException : Exception
    {
        public ConsoleException(string? message) : base(message)
        {
        }
    }
}