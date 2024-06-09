using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandyMan_.Backend.Controllers
{
        [ApiController]
    
    
        [Route("api/[controller]")]
        public class SurveyResponsesController : GenericController<SurveyResponseDTO>
        {
            private readonly ISurveyResponsesUnitOfWork _surveyResponsesUnitOfWork;

            public SurveyResponsesController(IGenericUnitOfWork<SurveyResponseDTO> unitOfWork, ISurveyResponsesUnitOfWork surveyResponsesUnitOfWork) : base(unitOfWork)
            {
                _surveyResponsesUnitOfWork = surveyResponsesUnitOfWork;
            }

            [AllowAnonymous]
            [HttpGet("combo")]
            public async Task<IActionResult> GetComboAsync()
            {
                return Ok(await _surveyResponsesUnitOfWork.GetComboAsync());
            }

            [HttpGet]
            public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
            {
                var response = await _surveyResponsesUnitOfWork.GetAsync(pagination);
                if (response.WasSuccess)
                {
                    return Ok(response.Result);
                }
                return BadRequest();
            }

            [HttpGet("totalPages")]
            public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
            {
                var action = await _surveyResponsesUnitOfWork.GetTotalPagesAsync(pagination);
                if (action.WasSuccess)
                {
                    return Ok(action.Result);
                }
                return BadRequest();
            }


        }
}
