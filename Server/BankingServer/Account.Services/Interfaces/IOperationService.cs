using Messages;
﻿using Account.DTO;


namespace Account.Services.Interfaces
{
    public interface IOperationService
    {
        Task AddToHistoryTableAsync(TransactionPayload message);
        Task<List<OperationDto>> GetOperationsByAccountIdAsync(int id,bool sortByDateDesc);
        Task<List<OperationDto>> getOpertaionsByFilterPage(int accountId, bool sortByDateDesc,int pageNumber,int numOfRecrds);
        Task<int> countOpertaionsById(int accountId);
    }
}
