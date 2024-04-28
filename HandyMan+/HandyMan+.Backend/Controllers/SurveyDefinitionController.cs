using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HandyMan_.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyDefinitionController : GenericController<SurveyDefinitionEntity>
    {
        public SurveyDefinitionController(IGenericUnitOfWork<SurveyDefinitionEntity> unitOfWork) : base(unitOfWork)
        {
        }
    }
}

