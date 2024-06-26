﻿using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Responses;
using HandyMan_.Shered.DTOs;

namespace HandyMan_.Backend.UnitsOfWork.Interfaces
{
    public interface ISubscriptionUnitOfWork
    {
        Task<ActionResponse<IEnumerable<SubscriptionType>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<SubscriptionType>> GetComboAsync();
    }
}
