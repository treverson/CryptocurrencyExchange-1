using Moq;
using NUnit.Framework;
using Stage2HW.Cli.Menu.MenuOptions;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stage2HW.Tests
{
    [TestFixture]
    public class AccountOperationsTests
    {
        [Test]
        public void DepositFunds_ValidDepositList_GetTransactionReturnsDepositList()
        {
            //Arrange
            DateTime testDate = new DateTime(2017, 1, 1, 0, 0, 0);
            var userRepositoryMoq = new Mock<IUserRepository>();

            int userId = 0;

            User testUser = new User()
            {
                Id = userId,
            };

            var mockTransaction = new Transaction()
            {
                CurrencyName = "PLN",
                Amount = 1500,
                Fiat = 1500,
                Id = 1,
                UserId = testUser.Id,
                TransactionDate = testDate
            };

            var mockTransactionTwo = new Transaction()
            {
                CurrencyName = "PLN",
                Amount = 2000,
                Fiat = 2000,
                Id = 1,
                UserId = testUser.Id,
                TransactionDate = testDate
            };

            var testTransactionsList = new List<Transaction>()
            {
                mockTransaction, mockTransactionTwo
            };

            userRepositoryMoq.Setup(c => c.GetTransactionsHistory(It.IsAny<int>())).Callback<int>((i) => userId = i).Returns(testTransactionsList);

            //Act
            userRepositoryMoq.Object.GetTransactionsHistory(1);

            //Assert
            userRepositoryMoq.Verify(c => c.GetTransactionsHistory(It.IsAny<int>()), Times.Once());

            Assert.AreEqual(testTransactionsList.Count, 2);
            Assert.AreEqual(testUser.Id, mockTransaction.UserId);
        }
    }
}
