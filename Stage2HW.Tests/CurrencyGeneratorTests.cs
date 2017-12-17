using NUnit.Framework;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services;
using Stage2HW.Business.Services.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Stage2HW.Tests
{
    [TestFixture]
    public class CurrencyGeneratorTests
    {
        private bool _wasSubscriberMethodCalled = false;

        private double _testBitCoinValue = 0;
        private double _testBitCoinCashValue = 0;
        private double _testEthereumValue = 0;
        private double _testLiteCoinValue = 0;

        private double _btcValueAsParam;
        private double _bccValueAsParam;
        private double _ethValueAsParam;
        private double _ltcValueAsParam;

        private List<Currency> _testCurrenciesList = null;

        [Test]
        public void RunGenerator_OnTimerElapsed_RatesGeneratedEventOccurs()
        {
            //Arrange
            var testGenerator = new CurrencyGenerator();

            //Act
            testGenerator.Run();
            testGenerator.NewExchangeRatesEvent += TestSubscriberMethod;

            Thread.Sleep(1500);

            //Assert
            Assert.IsTrue(_wasSubscriberMethodCalled);
        }

        private void TestSubscriberMethod(NewExchangeRatesEventArgs ratesGeneratedEventArgs)
        {
            _wasSubscriberMethodCalled = true;
        }

        [Test]
        public void RunGenerator_OnEventOccurring_ValidParamatersArPassed()
        {
            //Arrange
            var testGenerator = new CurrencyGenerator();

            //Act
            Assert.IsFalse(_wasSubscriberMethodCalled);
            testGenerator.Run();
            testGenerator.NewExchangeRatesEvent += TestSubscriberMethodForEventArgs;
            Thread.Sleep(1500);

            //Assert
            Assert.IsTrue(_wasSubscriberMethodCalled);
            Assert.IsNotNull(_testCurrenciesList);
            Assert.AreNotEqual(_testBitCoinValue, _btcValueAsParam);
            Assert.AreNotEqual(_testBitCoinCashValue, _bccValueAsParam);
            Assert.AreNotEqual(_testEthereumValue, _ethValueAsParam);
            Assert.AreNotEqual(_testLiteCoinValue, _ltcValueAsParam);
        }

        private void TestSubscriberMethodForEventArgs(NewExchangeRatesEventArgs ratesGeneratedEventArgs)
        {
            _wasSubscriberMethodCalled = true;
            _testCurrenciesList = ratesGeneratedEventArgs.CurrenciesList;
            _btcValueAsParam = ratesGeneratedEventArgs.CurrenciesList.First(n => n.CurrencyName == CurrencyNameEnum.Btc).LastPrice;
            _bccValueAsParam = ratesGeneratedEventArgs.CurrenciesList.First(n => n.CurrencyName == CurrencyNameEnum.Bcc).LastPrice;
            _ethValueAsParam = ratesGeneratedEventArgs.CurrenciesList.First(n => n.CurrencyName == CurrencyNameEnum.Eth).LastPrice;
            _ltcValueAsParam = ratesGeneratedEventArgs.CurrenciesList.First(n => n.CurrencyName == CurrencyNameEnum.Ltc).LastPrice;
        }

        [Test]
        public void GenerateCurrency_InitialValueExceedsThreshold_ReturnThresholdValues()
        {
            //Arrange
            var testGenerator = new CurrencyGenerator();
            const int initialValueHigherThanThreshold = 45000;
            const int initialValueLowerThanThreshold = 9000;
            const int minValue = 10000;
            const int maxValue = 40000;

            //Act
            var resultHigher = testGenerator.GenerateRates(initialValueHigherThanThreshold, minValue, maxValue);

            var resultLower = testGenerator.GenerateRates(initialValueLowerThanThreshold, minValue, maxValue);

            //Assert
            Assert.AreEqual(resultHigher, maxValue);
            Assert.AreEqual(resultLower, minValue);
        }
    }
}
