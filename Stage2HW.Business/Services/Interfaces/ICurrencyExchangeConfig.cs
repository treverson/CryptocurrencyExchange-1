namespace Stage2HW.Business.Services.Interfaces
{
    public interface ICurrencyExchangeConfig
    {
        int RefreshTime { get; }
        string BitBayBtcPlnAddress { get; }
        string BitBayBccPlnAddress { get; }
        string BitBayEthPlnAddress { get; }
        string BitBayLtcPlnAddress { get; }
        string DataBaseType { get; }
        string ExchangeType { get; }
    }
}