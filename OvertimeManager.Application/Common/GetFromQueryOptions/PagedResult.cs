using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.Common.GetFromQueryOptions
{
    public class PagedResult<T> 
    {
        public IEnumerable<T> Items { get; set; }
        public int? TotalPages { get; set; }

        public int? ItemsFrom { get; set; }
        public int? ItemsTo { get; set; }
        public int? TotalItemsCount { get; set; }

        public PagedResult(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalCount;
            if (pageSize > 0)
            {
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                ItemsFrom = (pageNumber - 1) * pageSize + 1;
                ItemsTo = Math.Min(pageNumber * pageSize, totalCount);
            }
        }
    }
}
