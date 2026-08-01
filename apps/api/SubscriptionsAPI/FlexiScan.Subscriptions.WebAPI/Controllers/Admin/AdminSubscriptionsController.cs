using FlexiScan.Subscriptions.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexiScan.Subscriptions.WebAPI.Controllers.Admin
{
    [ApiController]
    [Route("/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminSubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionPlanService _subscriptionPlanService;
        public AdminSubscriptionsController(ISubscriptionPlanService subscriptionPlanService)
        {
            _subscriptionPlanService = subscriptionPlanService;
        }

        [Route("sync-plans")]
        [HttpPost]
        public async Task<IActionResult> SyncStripePlansWithDbAsync()
        {
            await _subscriptionPlanService.UpdateSubscriptionPlans();

            return Ok();
        }
    }
}
