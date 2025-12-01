using OvertimeManager.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.Common.GetFromQueryOptions
{
    public class FromQueryOptions
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1; //default value
        public int PageSize { get; set; } = 0; //0 means all items
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
    }
}
