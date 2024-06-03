using HandyMan_.Backend.Helpers;
using HandyMan_.Backend.UnitsOfWork.Implementations;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shared.DTOs;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
//*
namespace HandyMan_.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ServicesController : GenericController<Service>
    {
        private readonly string _container;
        private readonly IFileStorage _fileStorage;
        private readonly IServicesUnitOfWork _serviceUnitOfWork;

        public ServicesController(IGenericUnitOfWork<Service> unitOfWork, IServicesUnitOfWork serviceUnitOfWork, IFileStorage fileStorage) : base(unitOfWork)
        {
            _fileStorage = fileStorage;
            _serviceUnitOfWork = serviceUnitOfWork;
            _container = "users";
        }
        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _serviceUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _serviceUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("GetAllServices")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllServices()
        {
            return Ok(await _serviceUnitOfWork.GetAllServices());
        }

        [HttpPost("AddServicePhoto")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddServicePhoto([FromBody] Service service) {
            if (!string.IsNullOrEmpty(service.Photo))
            {
                var photoService = Convert.FromBase64String(service.Photo);
                service.Photo = await _fileStorage.SaveFileAsync(photoService, ".jpg", _container);
            }
            var action = await _serviceUnitOfWork.AddServicePhotoAsync(service);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();

        }

    }
}
