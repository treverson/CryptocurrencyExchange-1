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
        public void DepositFunds_()
        {
            //Arrange
            var userRepositoryMoq = new Mock<IUserRepository>();

            var operations = new AccountOperations();


            DateTime testDate = new DateTime(2017,1,1,0,0,0);
            
            var testTransaction = new Transaction
            {
                CurrencyName = "PLN",
                Amount = 250,
                Fiat = 250,
                Id = 1,
                UserId = 1,
                TransactionDate = testDate,
                ExchangeRate = 0
            };

            List<Transaction> testList = new List<Transaction>()
            {
                testTransaction,
            };

            var testUser = new User
            {
                Id = 1,
                Transactions = testList,
            };

            userRepositoryMoq.Setup(x => x.RegisterTransaction(testTransaction));
            userRepositoryMoq.Setup(x => x.GetTransactionsHistory(testUser.Id)).Returns(new List<Transaction>
            {
                new Transaction()
                {
                    Amount = 250,
                    Fiat = 250,
                    UserId = 1,
                }
            });

            //Act

            var result = userRepositoryMoq.Object.GetTransactionsHistory(testUser.Id).SingleOrDefault();


            //Assert

            Assert.AreEqual(testTransaction.Amount, result.Amount);
            Assert.AreEqual(testTransaction.Fiat, result.Fiat);
        }
    }
}
