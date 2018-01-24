using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage2HW.Business.Dtos
{
    public class UserRequest
    {
        public UserDto User{ get; set; }
        public List<OwnedCurrency> OwnedCurrencies { get; set; }
        
        
    }

    public class OwnedCurrency
    {
        public string Name { get; set; }
        public double AvailableAmount { get; set; }
    }
}
