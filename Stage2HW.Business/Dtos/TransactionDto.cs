using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage2HW.Business.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CurrencyName { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
    }
}
