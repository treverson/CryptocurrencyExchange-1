namespace Stage2HW.Business.Services.Interfaces
{
    public interface ICurrencyGenerator
    {
        double GenerateCryptoCurrency(double initialValue, int minValue, int maxValue);
        void RunGenerator();
        event RatesGeneratedHandler NewRatesEvent;
    }
}