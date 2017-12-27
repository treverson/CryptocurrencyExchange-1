using Moq;
using NUnit.Framework;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace Stage2HW.Tests
{
    [TestFixture]
    public class AccountOperationsTests
    {
        [Test]
        public void GetTransactionsHistory_GetsAnId_IsRunOnce()
        {
            //Arrange
            var transactionsRepositoryMoq = new Mock<ITransactionRepository>();

            //Act
            transactionsRepositoryMoq.Object.GetTransactionsHistory(1);

            //Assert
            transactionsRepositoryMoq.Verify(c => c.GetTransactionsHistory(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void DepositFunds_ValidDepositList_GetTransactionReturnsDepositList()
        {
            //Arrange
            DateTime testDate = new DateTime(2017, 1, 1, 0, 0, 0);
            var transactionsRepositoryMoq = new Mock<ITransactionRepository>();

            int userId = 0;
            User testUser = new User()
            {
                Id = userId,
            };

            var mockDeposit = new Transaction()
            {
                CurrencyName = "PLN",
                Amount = 1500,
                Fiat = 1500,
                Id = 1,
                UserId = testUser.Id,
                TransactionDate = testDate
            };

            var mockDepositTwo = new Transaction()
            {
                CurrencyName = "PLN",
                Amount = 2000,
                Fiat = 2000,
                Id = 2,
                UserId = testUser.Id,
                TransactionDate = testDate
            };

            var testTransactionsList = new List<Transaction>()
            {
                mockDeposit, mockDepositTwo
            };

            transactionsRepositoryMoq.Setup(c => c.GetTransactionsHistory(It.IsAny<int>())).Callback<int>((i) => userId = i).Returns(testTransactionsList);

            //Act
            transactionsRepositoryMoq.Object.GetTransactionsHistory(1);

            //Assert
            transactionsRepositoryMoq.Verify(c => c.GetTransactionsHistory(It.IsAny<int>()), Times.Once());

            Assert.AreEqual(testTransactionsList.Count, 2);
            Assert.AreEqual(testUser.Id, mockDeposit.UserId);
            Assert.AreEqual(testUser.Id, mockDepositTwo.UserId);
        }
    }
}
