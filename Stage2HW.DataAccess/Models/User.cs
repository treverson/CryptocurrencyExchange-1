﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stage2HW.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public List<Transaction> Transactions { get; set; }
        //public List<Currency> Currencies { get; set; }
    }
}
