using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Entities.User;
using System.Linq.Expressions;

namespace OvertimeManager.Application.Common.GetFromQueryOptions
{
    public class FromQueryOptionsHandler<T>(FromQueryOptions queryOptions)
    {
        private readonly FromQueryOptions _queryOptions = queryOptions;

        private IQueryable<T> returnQuery;
        public int SearchPhraseFilteredItems { get; set; }

        public (IQueryable<T>, int) GetAppliedQueryWithTotalItemsCount(IQueryable<T> inputQuery)
        {
            var queryAplied = ApplyQueryOptions(inputQuery);

            var totalItemsCount = SearchPhraseFilteredItems;
            var items =   queryAplied;

            var result = (items, totalItemsCount);
            return result;
        }

        public IQueryable<T> ApplyQueryOptions(IQueryable<T> inputQuery)
        {

            if (_queryOptions is not null)
            {
                if (inputQuery is IQueryable<OvertimeRequest> overtimeQuery)
                    returnQuery = ApplyQueryOptions(overtimeQuery);

                if (inputQuery is IQueryable<CompensationRequest> compensationQuery)
                    returnQuery = ApplyQueryOptions(compensationQuery);

                if (inputQuery is IQueryable<Employee> employeeQuery)
                    returnQuery = ApplyQueryOptions(employeeQuery);

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

 

        private IQueryable<T> ApplyQueryOptions(IQueryable<OvertimeRequest> query)
        {
            var requestQuery = ApplySearchPhrase(query);
            SearchPhraseFilteredItems = requestQuery.Count();
            requestQuery = ApplySorting(requestQuery);
            var paginatedQuery = ApplyPagination((IQueryable<T>)requestQuery);
            return paginatedQuery;
        }

        private IQueryable<T> ApplyQueryOptions(IQueryable<CompensationRequest> query)
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

        private IQueryable<OvertimeRequest> ApplySearchPhrase(IQueryable<OvertimeRequest> queryable)
        {
            // Apply search phrase filtering
            if (!string.IsNullOrEmpty(_queryOptions.SearchPhrase))
            {
                var lowerSearchPhrase = _queryOptions.SearchPhrase.ToLower();
                queryable = queryable.Where(r => r.RequestedForEmployee!.Email.ToLower().Contains(lowerSearchPhrase)||
                                                 r.Status.ToLower().Contains(lowerSearchPhrase)||
                                                 r.CreatedForDay.ToString().ToLower().Contains(lowerSearchPhrase)||
                                                 r.Name.ToLower().Contains(lowerSearchPhrase)||
                                                 r.BusinessJustificationReason.ToLower().Contains(lowerSearchPhrase));
            }
            return queryable;
        }

        private IQueryable<CompensationRequest> ApplySearchPhrase(IQueryable<CompensationRequest> queryable)
        {
            // Apply search phrase filtering
            if (!string.IsNullOrEmpty(_queryOptions.SearchPhrase))
            {
                var lowerSearchPhrase = _queryOptions.SearchPhrase.ToLower();
                queryable = queryable.Where(r => r.RequestedForEmployee!.Email.ToLower().Contains(lowerSearchPhrase) ||
                                                 r.Status.ToLower().Contains(lowerSearchPhrase)||
                                                 r.CreatedForDay.ToString().ToLower().Contains(lowerSearchPhrase));
            }
            return queryable;
        }

        #endregion


        #region Sorting
        private IQueryable<Employee> ApplySorting(IQueryable<Employee> inputQuery)
        {
            var defaultQuery = inputQuery.OrderBy(a => a.LastName);
            if (!string.IsNullOrEmpty(_queryOptions.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<Employee, object>>>
                {
                    {nameof (Employee.Email).ToLower(), e => e.Email },
                    {nameof (Employee.FirstName).ToLower(), e => e.FirstName },
                    {nameof (Employee.LastName).ToLower(), e => e.LastName },
                    {nameof (Employee.Id), e => e.Id },
                    {nameof (Employee.RoleId), e => e.RoleId },
                    {nameof (Employee.OvertimeSummary.TakenOvertime).ToLower(), e => e.OvertimeSummary.TakenOvertime },
                    {nameof (Employee.OvertimeSummary.SettledOvertime).ToLower(), e => e.OvertimeSummary.SettledOvertime },
                    {nameof (Employee.OvertimeSummary.UnsettledOvertime).ToLower(), e => e.OvertimeSummary.UnsettledOvertime }
                };
                if (columnSelector.ContainsKey(_queryOptions.SortBy.ToLower()))
                {
                    var selectedColumn = columnSelector[_queryOptions.SortBy.ToLower()];
                    var outputQuery = _queryOptions.SortDirection == SortDirection.ASC || _queryOptions.SortDirection is null
                        ? inputQuery.OrderBy(selectedColumn)
                        : inputQuery.OrderByDescending(selectedColumn);
                    return outputQuery;
                }
            }
            return defaultQuery;
        }

        private IQueryable<OvertimeRequest> ApplySorting(IQueryable<OvertimeRequest> inputQuery)
        {
            //default sorting by newest
            var defaultQuery = inputQuery.OrderByDescending(r => r.CreatedForDay);
            if (!string.IsNullOrEmpty(_queryOptions.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<OvertimeRequest, object>>>
                {
                    {nameof (OvertimeRequest.Status).ToLower(), r => r.Status },
                    {nameof (OvertimeRequest.CreatedForDay).ToLower(), r => r.CreatedForDay },
                    {nameof (OvertimeRequest.CreatedAt).ToLower(), r => r.CreatedAt },
                    {nameof (OvertimeRequest.RequestedForEmployee.LastName).ToLower(), r => r.RequestedForEmployee!.LastName },
                };
                if (columnSelector.ContainsKey(_queryOptions.SortBy.ToLower()))
                {
                    var selectedColumn = columnSelector[_queryOptions.SortBy.ToLower()];
                    var outputQuery = _queryOptions.SortDirection == SortDirection.ASC || _queryOptions.SortDirection is null
                        ? inputQuery.OrderBy(selectedColumn)
                        : inputQuery.OrderByDescending(selectedColumn);
                    return outputQuery;
                }
            }
            return defaultQuery;
        }

        private IQueryable<CompensationRequest> ApplySorting(IQueryable<CompensationRequest> inputQuery)
        {

            //default sorting by newest
            var defaultQuery = inputQuery.OrderByDescending(r => r.CreatedForDay);

            if (!string.IsNullOrEmpty(_queryOptions.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<CompensationRequest, object>>>
                {
                    {nameof (CompensationRequest.Status).ToLower(), r => r.Status },
                    {nameof (CompensationRequest.CreatedForDay).ToLower(), r => r.CreatedForDay },
                    {nameof (CompensationRequest.CreatedAt).ToLower(), r => r.CreatedAt},
                    {nameof (CompensationRequest.RequestedForEmployee.LastName).ToLower(), r => r.RequestedForEmployee!.LastName},
                };
                if (columnSelector.ContainsKey(_queryOptions.SortBy.ToLower()))
                    {
                        var selectedColumn = columnSelector[_queryOptions.SortBy.ToLower()];
                        var outputQuery = _queryOptions.SortDirection == SortDirection.ASC || _queryOptions.SortDirection is null
                            ? inputQuery.OrderBy(selectedColumn)
                            : inputQuery.OrderByDescending(selectedColumn);
                    return outputQuery;
                    }
            }
            return defaultQuery;
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
