namespace Stage2HW.Business.Services.Interfaces
{
    public interface ICurrencyGenerator
    {
        double GenerateRates(double initialValue, int minValue, int maxValue);
        void RunGenerator();
        event RatesGeneratedHandler NewRatesGeneratedEvent;
    }
}