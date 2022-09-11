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
        public async Task<List<OperationDto>> getOpertaionsByAccountId(int accountId)
        {
            try
            {
                List<OperationDto> operations = await _operationService.GetOperationsByAccountId(accountId);
                return operations;
            }
            catch(Exception ex)
            {
                throw new Exception("Could not get operations",ex);
            }
        }





    }
}
