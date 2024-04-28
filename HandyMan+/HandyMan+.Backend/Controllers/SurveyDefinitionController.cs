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
    public class SurveyDefinitionController : GenericController<SurveyDefinitionEntity>
    {
        private readonly ISurveyDefinitionsUnitOfWork _surveyDefinitionsUnitOfWork;

        public SurveyDefinitionController(IGenericUnitOfWork<SurveyDefinitionEntity> unitOfWork, ISurveyDefinitionsUnitOfWork surveyDefinitionsUnitOfWork) : base(unitOfWork)
        {
            _surveyDefinitionsUnitOfWork = surveyDefinitionsUnitOfWork;
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _surveyDefinitionsUnitOfWork.GetComboAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _surveyDefinitionsUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _surveyDefinitionsUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }


    }
}

