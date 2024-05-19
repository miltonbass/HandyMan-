using Microsoft.EntityFrameworkCore;
using HandyMan_.Backend.Data;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Responses;
using HandyMan_.Shered.DTOs;
using HandyMan_.Backend.Helpers;
using HandyMan_.Shered.Entities;


namespace HandyMan_.Backend.Repositories.Implementations
{
    public class ServiceOrderRepository : GenericRepository<ServiceOrder>, IServiceOrderRepository
    {
        private readonly DataContext _context;

        public ServiceOrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceOrder>> GetComboAsync()
        {
            return await _context.ServiceOrders
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public override async Task<ActionResponse<IEnumerable<ServiceOrder>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.ServiceOrders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Detail.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<ServiceOrder>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Id)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.ServiceOrders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Detail.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

    }
}
