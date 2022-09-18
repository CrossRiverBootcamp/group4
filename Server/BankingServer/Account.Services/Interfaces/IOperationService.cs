using Messages;
﻿using Account.DTO;


namespace Account.Services.Interfaces
{
    public interface IOperationService
    {
        Task AddToHistoryTableAsync(TransactionPayload message);
        Task<List<OperationDto>> getOperationsByFilterPageAsync(int accountId, bool sortByDateDesc,int pageNumber,int numOfRecrds);
        Task<int> countOperationsByIdAsync(int accountId);
    }
}
