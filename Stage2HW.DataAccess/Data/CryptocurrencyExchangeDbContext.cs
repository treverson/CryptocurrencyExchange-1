﻿using Stage2HW.DataAccess.Models;
using System.Configuration;
using System.Data.Entity;

namespace Stage2HW.DataAccess.Data
{
    public class CryptocurrencyExchangeDbContext : DbContext
    {
        public CryptocurrencyExchangeDbContext() : base(GetConnectionString())
        {}

        public DbSet<User> UsersDbSet { get; set; }
        public DbSet<SqlTransaction> TransactionsDbSet { get; set; }
             
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["CryptocurrencyExchangeDb"].ConnectionString;
        }
    }
}
