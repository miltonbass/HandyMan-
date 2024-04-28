﻿using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Responses;
using Orders.Shared.DTOs;

namespace HandyMan_.Backend.Repositories.Interfaces
{
    public interface ISubscriptionTypeRepository
    {
        Task<ActionResponse<IEnumerable<SubscriptionType>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<SubscriptionType>> GetComboAsync();
    }
}
