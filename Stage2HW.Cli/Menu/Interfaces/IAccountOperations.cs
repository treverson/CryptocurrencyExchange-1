﻿namespace Stage2HW.Cli.Menu.Interfaces
{
    internal interface IAccountOperations
    {
        void DepositFunds();
        void WithdrawFunds();
        void ViewHistory();
        void BuyCurrencies();
        void SellCurrencies();
        void SaveHistory();
    }
}