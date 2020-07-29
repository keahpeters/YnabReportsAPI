﻿
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using Moq;

using NUnit.Framework;

using YnabReportsAPI.Transactions.Models;
using YnabReportsAPI.Transactions.Services;
using YnabReportsAPI.YnabAPI.Exceptions;
using YnabReportsAPI.YnabAPI.ExternalModels;
using YnabReportsAPI.YnabAPI.Services;

namespace YnabReportsAPI.UnitTests.Transactions.Services
{
    [TestFixture]
    public class TransactionServiceTests
    {
        private Mock<IYnabService> ynabService;

        private TransactionService transactionService;

        [SetUp]
        public void SetUp()
        {
            this.ynabService = new Mock<IYnabService>();
            this.transactionService = new TransactionService(this.ynabService.Object);
        }

        [Test]
        public void GetTransactions_YnabServiceReturnsNullTransactions_ThrowException()
        {
            Assert.ThrowsAsync<YnabResponseException>(() => this.transactionService.GetTransactions("testBudgetId", null));
        }

        [Test]
        public async Task GetTransactions_YnabServiceReturnsTransactions_NonSplitTransactionsAreReturned()
        {
            List<YnabTransaction> ynabTransaction = new List<YnabTransaction>
            {
                new YnabTransaction { Date = new DateTime(2020, 1, 1), Amount = 10500, CategoryName = "Category1", Approved = true, SubTransactions = new List<YnabTransaction>() },
                new YnabTransaction { Date = new DateTime(2020, 1, 2), Amount = 50000, CategoryName = "Category2", Approved = true, SubTransactions = new List<YnabTransaction>() },
            };

            this.ynabService.Setup(y => y.GetTransactions(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new YnabResponse { Data = new YnabData { Transactions = ynabTransaction } });

            IEnumerable<Transaction> result = await this.transactionService.GetTransactions("testBudgetId", null);

            List<Transaction> expectedResult = new List<Transaction>
            {
                new Transaction(new DateTime(2020, 1, 1), 10.5M, "Category1"),
                new Transaction(new DateTime(2020, 1, 2), 50M, "Category2"),
            };

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetTransactions_YnabServiceReturnsUnapprovedTransactions_UnapprovedNonSplitTransactionsAreNotReturned()
        {
            List<YnabTransaction> ynabTransaction = new List<YnabTransaction>
            {
                new YnabTransaction { Date = new DateTime(2020, 1, 1), Amount = 10500, CategoryName = "Category1", Approved = true, SubTransactions = new List<YnabTransaction>() },
                new YnabTransaction { Date = new DateTime(2020, 1, 2), Amount = 50000, CategoryName = "Category2", Approved = false, SubTransactions = new List<YnabTransaction>() },
            };

            this.ynabService.Setup(y => y.GetTransactions(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new YnabResponse { Data = new YnabData { Transactions = ynabTransaction } });

            IEnumerable<Transaction> result = await this.transactionService.GetTransactions("testBudgetId", null);

            List<Transaction> expectedResult = new List<Transaction>
            {
                new Transaction(new DateTime(2020, 1, 1), 10.5M, "Category1"),
            };

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
