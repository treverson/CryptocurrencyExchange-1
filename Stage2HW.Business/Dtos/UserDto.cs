using System.Collections.Generic;

namespace Stage2HW.Business.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserNickName { get; set; }
        public string UserPassword { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        public List<Currency> Currencies { get; set; }
    }
}
