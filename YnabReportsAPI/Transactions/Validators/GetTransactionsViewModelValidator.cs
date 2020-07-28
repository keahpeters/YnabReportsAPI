using System;
using System.Globalization;

using FluentValidation;

using YnabReportsAPI.Transactions.ViewModels;

namespace YnabReportsAPI.Transactions.Validators
{
    public class GetTransactionsViewModelValidator : AbstractValidator<GetTransactionsViewModel>
    {
        public GetTransactionsViewModelValidator()
        {
            RuleFor(queryString => queryString.StartDate)
                .Must(startDate => DateTime.TryParseExact(startDate, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out _))
                .When(queryString => !string.IsNullOrEmpty(queryString.StartDate))
                .WithMessage("Invalid start date format");
        }
    }
}
