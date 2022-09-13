using Messages;
﻿using Account.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface IOperationService
    {
        Task AddToHistoryTableAsync(TransactionPayload message);
        Task<List<OperationDto>> GetOperationsByAccountIdAsync(int id,bool sortByDateDesc);
        Task<List<OperationDto>> getOpertaionsByFilterPage(int accountId, bool sortByDateDesc,int pageNumber,int numOfRecrds);

    }
}
