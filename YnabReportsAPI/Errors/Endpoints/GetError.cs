
using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Mvc;

namespace YnabReportsAPI.Errors.Endpoints
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GetError : BaseEndpoint
    {
        [HttpGet("/error")]
        public IActionResult Handle()
        {
            return this.Problem();
        }
    }
}
