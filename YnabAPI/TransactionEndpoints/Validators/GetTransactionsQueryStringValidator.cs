using System;
using System.Globalization;

using FluentValidation;

using YnabAPI.TransactionEndpoints.Models;

namespace YnabAPI.TransactionEndpoints.Validators
{
    public class GetTransactionsQueryStringValidator : AbstractValidator<GetTransactionsQueryString>
    {
        public GetTransactionsQueryStringValidator()
        {
            RuleFor(queryString => queryString.StartDate)
                .Must(startDate => DateTime.TryParseExact(startDate, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out _))
                .When(queryString => !string.IsNullOrEmpty(queryString.StartDate))
                .WithMessage("Invalid start date format");
        }
    }
}
