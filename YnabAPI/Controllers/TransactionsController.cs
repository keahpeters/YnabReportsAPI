using Microsoft.AspNetCore.Mvc;

namespace YnabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [HttpGet]
        [Route("budgets/{budgetId}/transactions")]
        public IActionResult Get(string budgetId)
        {
            return this.Ok();
        }
    }
}
