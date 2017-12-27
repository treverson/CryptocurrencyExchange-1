using Stage2HW.Business.Services.Enums;

namespace Stage2HW.Business.Dtos
{
    public class Currency
    {
        public CurrencyNameEnum CurrencyName { get; set; }
        public double LastPrice { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
    }
}
