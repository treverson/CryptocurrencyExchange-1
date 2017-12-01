using NUnit.Framework;
using Stage2HW.Business.Services;
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

        [Test]
        public void RunGenerator_OnTimerElapsed_RatesGeneratedEventOccurs()
        {
            //Arrange
            var testGenerator = new CurrencyGenerator();

            //Act
            testGenerator.RunGenerator();
            testGenerator.NewRatesGeneratedEvent += TestSubscriberMethod;

            Thread.Sleep(1500);

            //Assert
            Assert.IsTrue(_wasSubscriberMethodCalled);
        }

        private void TestSubscriberMethod(RatesGeneratedEventArgs ratesGeneratedEventArgs)
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
            testGenerator.RunGenerator();
            testGenerator.NewRatesGeneratedEvent += TestSubscriberMethodForEventArgs;
            Thread.Sleep(1500);

            //Assert
            Assert.IsTrue(_wasSubscriberMethodCalled);
            Assert.AreNotEqual(_testBitCoinValue, _btcValueAsParam);
            Assert.AreNotEqual(_testBitCoinCashValue, _bccValueAsParam);
            Assert.AreNotEqual(_testEthereumValue, _ethValueAsParam);
            Assert.AreNotEqual(_testLiteCoinValue, _ltcValueAsParam);
        }

        private void TestSubscriberMethodForEventArgs(RatesGeneratedEventArgs ratesGeneratedEventArgs)
        {
            _wasSubscriberMethodCalled = true;
            _btcValueAsParam = ratesGeneratedEventArgs.BitCoinCashValue;
            _bccValueAsParam = ratesGeneratedEventArgs.BitCoinCashValue;
            _ethValueAsParam = ratesGeneratedEventArgs.EthereumValue;
            _ltcValueAsParam = ratesGeneratedEventArgs.LiteCoinValue;
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
            var resultHigher = testGenerator.GenerateCryptoCurrency(initialValueHigherThanThreshold, minValue, maxValue);

            var resultLower = testGenerator.GenerateCryptoCurrency(initialValueLowerThanThreshold, minValue, maxValue);

            //Assert
            Assert.AreEqual(resultHigher, maxValue);
            Assert.AreEqual(resultLower, minValue);
        }
    }
}
