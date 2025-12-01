using Microsoft.AspNet.Identity;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Entities.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.Common.GetFromQueryOptions
{
    public class QueryHandler<T>
    {
        private readonly FromQueryOptions _queryOptions;
        private IQueryable<T> returnQuery;
        public int SearchPhraseFilteredItems { get; set; }

        public QueryHandler(FromQueryOptions queryOptions)
        {
            _queryOptions = queryOptions;
        }


        public IQueryable<T> ApplyQueryOptions(IQueryable<T> inputQuery)
        {

            if (_queryOptions is not null)
            {
                if (inputQuery is IQueryable<OvertimeRequestBase> requstQuery)
                {
                    returnQuery = ApplyQueryOptions(requstQuery);
                }
                if (inputQuery is IQueryable<Employee> employeeQuery)
                {
                    returnQuery = ApplyQueryOptions(employeeQuery);
                }
                return returnQuery;
            }
            else
            {
                return inputQuery;
            }

        }

        private IQueryable<T> ApplyQueryOptions(IQueryable<Employee> query)
        {
            var employeeQuery = ApplySearchPhrase(query);
            SearchPhraseFilteredItems = employeeQuery.Count();
            employeeQuery = ApplySorting(employeeQuery);
            var paginatedQuery = ApplyPagination((IQueryable<T>)employeeQuery);
            return paginatedQuery;
        }

 

        private IQueryable<T> ApplyQueryOptions(IQueryable<OvertimeRequestBase> query)
        {
            var requestQuery = ApplySearchPhrase(query);
            SearchPhraseFilteredItems = requestQuery.Count();
            requestQuery = ApplySorting(requestQuery);
            var paginatedQuery = ApplyPagination((IQueryable<T>)requestQuery);
            return paginatedQuery;
        }





        #region Search Phrase
        private IQueryable<Employee> ApplySearchPhrase(IQueryable<Employee> queryable)
        {
            // Apply search phrase filtering
            if (!string.IsNullOrEmpty(_queryOptions.SearchPhrase))
            {
                var lowerSearchPhrase = _queryOptions.SearchPhrase.ToLower();
                queryable = queryable.Where(e => e.Email.ToLower().Contains(lowerSearchPhrase) ||
                                                 e.FirstName.ToLower().Contains(lowerSearchPhrase) ||
                                                 e.LastName.ToLower().Contains(lowerSearchPhrase));
            }
            return queryable;
        }

        private IQueryable<OvertimeRequestBase> ApplySearchPhrase(IQueryable<OvertimeRequestBase> queryable)
        {
            // Apply search phrase filtering
            if (!string.IsNullOrEmpty(_queryOptions.SearchPhrase))
            {
                var lowerSearchPhrase = _queryOptions.SearchPhrase.ToLower();
                queryable = queryable.Where(r => r.RequestedForEmployee!.Email.ToLower().Contains(lowerSearchPhrase)||
                                                 r.Status.ToLower().Contains(lowerSearchPhrase));
            }
            return queryable;
        }

        #endregion


        #region Sorting
        private IQueryable<Employee> ApplySorting(IQueryable<Employee> inputQuery)
        {
            var outputQuery = inputQuery;
            if (!string.IsNullOrEmpty(_queryOptions.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<Employee, object>>>
                {
                    {nameof (Employee.Email), e => e.Email },
                    {nameof (Employee.FirstName), e => e.FirstName },
                    {nameof (Employee.LastName), e => e.LastName },
                    {nameof (Employee.Id), e => e.Id },
                    {nameof (Employee.RoleId), e => e.RoleId },
                    {nameof (Employee.OvertimeSummary.TakenOvertime), e => e.OvertimeSummary.TakenOvertime },
                    {nameof (Employee.OvertimeSummary.SettledOvertime), e => e.OvertimeSummary.SettledOvertime },
                    {nameof (Employee.OvertimeSummary.UnsettledOvertime), e => e.OvertimeSummary.UnsettledOvertime }
                };
                var selectedColumn = columnSelector[_queryOptions.SortBy];
                outputQuery = _queryOptions.SortDirection == SortDirection.ASC || _queryOptions.SortDirection is null
                    ? inputQuery.OrderBy(selectedColumn)
                    : inputQuery.OrderByDescending(selectedColumn);

            }
            //default sorting by employee last name
            else
            {
                outputQuery = inputQuery.OrderBy(a => a.LastName);
            }
            return outputQuery;
        }

        private IQueryable<OvertimeRequestBase> ApplySorting(IQueryable<OvertimeRequestBase> inputQuery)
        {
            var outputQuery = inputQuery;
            if (!string.IsNullOrEmpty(_queryOptions.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<OvertimeRequestBase, object>>>
                {
                    {nameof (OvertimeRequestBase.Status), r => r.Status },
                    {nameof (OvertimeRequestBase.CreatedForDay), r => r.CreatedForDay },
                    {nameof (OvertimeRequestBase.CreatedAt), r => r.CreatedAt },
                    {nameof (OvertimeRequestBase.RequestedForEmployee.LastName), r => r.RequestedForEmployee!.LastName },
                };
                var selectedColumn = columnSelector[_queryOptions.SortBy];
                outputQuery = _queryOptions.SortDirection == SortDirection.ASC || _queryOptions.SortDirection is null
                    ? inputQuery.OrderBy(selectedColumn)
                    : inputQuery.OrderByDescending(selectedColumn);

            }
            //default sorting by newest
            else
            {
                outputQuery = inputQuery.OrderByDescending(r => r.CreatedForDay);
            }
            return outputQuery;
        }

        #endregion

        #region Pagination
        private IQueryable<T> ApplyPagination(IQueryable<T> queryable)
        {
            // Apply pagination
            if (_queryOptions.PageSize > 0)
            {
                queryable = queryable
                    .Skip((_queryOptions.PageNumber - 1) * _queryOptions.PageSize)
                    .Take(_queryOptions.PageSize);
            }
            return queryable;
        }
        #endregion  
    }
}
