using Account.DTO;
using Account.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService _operationService;
        public OperationsController(IOperationService operationService)
        {
            _operationService = operationService;
        }
        
        //get operations by account Id and pagination requirments for operations history
        [HttpGet("filter")]
        public async Task<List<OperationDto>> getOperationsByFilterPageAsync(int accountId, bool sortByDateDesc, int pageNumber, int numOfRecords)
        {
            try
            {
                List<OperationDto> operations = await _operationService.getOperationsByFilterPageAsync(accountId, sortByDateDesc,pageNumber,numOfRecords);
                return operations;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get operations", ex);
            }
        }


        //for pagination
        [HttpGet("count")]
        public async Task<int> CountOperationsByAccountIdAsync(int accountId)
        {
            try
            {
                return await _operationService.countOperationsByIdAsync(accountId);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get operations", ex);
            }
        }
    }
}
