using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.Common.GetFromQueryOptions
{
    public class FromQueryOptionsValidator : AbstractValidator<FromQueryOptions>
    {
        private int[] allowedPageSizes = new int[] { 0, 5, 10, 20, 50, 100 };//0 means all items
        public FromQueryOptionsValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");
            RuleFor(x => x.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"Page size must be one of the following values: {string.Join(", ", allowedPageSizes)}");
                }
            });

        }
    }
}
