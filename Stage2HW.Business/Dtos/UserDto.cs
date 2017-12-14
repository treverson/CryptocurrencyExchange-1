using System.Collections.Generic;

namespace Stage2HW.Business.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        //public List<Currency> Currencies { get; set; }
    }
}
