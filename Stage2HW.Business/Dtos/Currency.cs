using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage2HW.Business.Dtos
{
    public class Currency
    {
        //    public int Id { get; set; }
        public string Name { get; set; }
        
        public double Bid { get; set; }
        public double Ask { get; set; }

        //    public List<UserDto> Users { get; set; }
    }
}
