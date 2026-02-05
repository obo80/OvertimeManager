using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceType, string resourceIdentifier) 
            : base($"{resourceType} with id: {resourceIdentifier} doesn't exist")
        {
            
        }

        public NotFoundException(string? message) 
            : base(message)
        {
        }
    }
}
