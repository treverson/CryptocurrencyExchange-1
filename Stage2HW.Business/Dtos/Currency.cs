using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stage2HW.Business.Services.Enums;

namespace Stage2HW.Business.Dtos
{
    public class Currency
    {
        //    public int Id { get; set; }
        public CurrencyNameEnum CurrencyName { get; set; }
        
       // public double Bid { get; set; }
        public double Last { get; set; }

        //    public List<UserDto> Users { get; set; }
    }
}
