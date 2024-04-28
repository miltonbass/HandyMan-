using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HandyMan_.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : GenericController<SubscriptionType>
    {
        public SubscriptionsController(IGenericUnitOfWork<SubscriptionType> unitOfWork) : base(unitOfWork)
        {
        }
    }
}

