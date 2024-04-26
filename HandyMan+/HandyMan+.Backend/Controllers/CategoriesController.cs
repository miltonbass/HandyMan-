using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HandyMan_.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : GenericController<Category>
    {
        public CategoriesController(IGenericUnitOfWork<Category> unitOfWork) : base(unitOfWork)
        {
        }
    }
}