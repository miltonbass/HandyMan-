using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Shared.DTOs;

namespace HandyMan_.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : GenericController<SubscriptionType>
    {
        private readonly ISubscriptionUnitOfWork  _subscriptionUnitOfWork;

        public SubscriptionsController(IGenericUnitOfWork<SubscriptionType> unitOfWork, ISubscriptionUnitOfWork subscriptionUnitOfWork) : base(unitOfWork)
        {
            _subscriptionUnitOfWork = subscriptionUnitOfWork;
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _subscriptionUnitOfWork.GetComboAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _subscriptionUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _subscriptionUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

    }
}

