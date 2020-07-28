
using Moq;

using NUnit.Framework;

using YnabReportsAPI.Transactions.Services;
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
    }
}
