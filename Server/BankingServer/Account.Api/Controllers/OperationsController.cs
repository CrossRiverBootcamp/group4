using Account.DTO;
using Account.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public async Task<List<OperationDto>> getOpertaionsByAccountIdAsync(int accountId, bool sortByDateDesc)
        {
            try
            {
                List<OperationDto> operations = await _operationService.GetOperationsByAccountIdAsync(accountId,sortByDateDesc);
                return operations;
            }
            catch(Exception ex)
            {
                throw new Exception("Could not get operations",ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<List<OperationDto>> getOpertaionsByFilterPageAsync(int accountId, bool sortByDateDesc, int pageNumber, int numOfRecrds)
        {
            try
            {
                List<OperationDto> operations = await _operationService.getOpertaionsByFilterPage(accountId, sortByDateDesc,pageNumber,numOfRecrds);
                return operations;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get operations", ex);
            }
        }



    }
}
