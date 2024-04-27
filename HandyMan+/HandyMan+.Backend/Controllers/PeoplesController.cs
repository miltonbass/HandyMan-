using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HandyMan_.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeoplesController : GenericController<People>
    {
        public PeoplesController(IGenericUnitOfWork<People> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
