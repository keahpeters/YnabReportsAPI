using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation.TestHelper;

using NUnit.Framework;

using YnabReportsAPI.Transactions.Validators;

namespace YnabReportsAPI.UnitTests.Transactions.Validators
{
    [TestFixture]
    public class GetTransactionsViewModelValidatorTests
    {
        private GetTransactionsViewModelValidator validator;

        [SetUp]
        public void SetUp()
        {
            this.validator = new GetTransactionsViewModelValidator();
        }

        [Test]
        public void ShouldNotHaveErrorWhenStartDateIsNull()
        {
            this.validator.ShouldNotHaveValidationErrorFor(model => model.StartDate, (string)null);
        }

        [Test]
        public void ShouldNotHaveErrorWhenStartDateIsInCorrectFormat()
        {
            this.validator.ShouldNotHaveValidationErrorFor(model => model.StartDate, "2020-01-01");
        }

        [Test]
        public void ShouldHaveErrorWhenStartDateDoesNotExist()
        {
            this.validator.ShouldHaveValidationErrorFor(model => model.StartDate, "2020-13-01");
        }

        [Test]
        public void ShouldHaveErrorWhenStartDateIsIncorrectFormat()
        {
            this.validator.ShouldHaveValidationErrorFor(model => model.StartDate, "01-01-2020");
        }
    }
}
