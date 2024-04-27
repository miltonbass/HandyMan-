using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HandyMan_.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : GenericController<City>
    {
        public CitiesController(IGenericUnitOfWork<City> unitOfWork) : base(unitOfWork)
        {
        }
    }
}