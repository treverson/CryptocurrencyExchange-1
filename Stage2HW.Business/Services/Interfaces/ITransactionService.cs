﻿using System.Collections.Generic;
using Stage2HW.Business.Dtos;

namespace Stage2HW.Business.Services.Interfaces
{
    public interface ITransactionService
    {
        List<TransactionDto> GetTransactionHistory(int activeUserId);
        void RegisterTransaction(TransactionDto transaction);
    }
}