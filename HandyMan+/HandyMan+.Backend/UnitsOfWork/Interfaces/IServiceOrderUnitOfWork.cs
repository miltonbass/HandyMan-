﻿using HandyMan_.Shared.Entities;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Interfaces
{
    public interface IServiceOrderUnitOfWork
    {
        Task<ActionResponse<ServiceOrder>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<ServiceOrder>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<IEnumerable<ServiceOrder>>> GetAsync();
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<IEnumerable<ServiceOrder>> GetComboAsync();
    }
}
