using HandyMan_.Backend.Data;
using HandyMan_.Backend.Helpers;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;
using Microsoft.EntityFrameworkCore;

namespace HandyMan_.Backend.Repositories.Implementations
{
    public class ServiceOrderRepository : GenericRepository<ServiceOrder>, IServiceOrderRepository
    {
        private readonly DataContext _context;

        public ServiceOrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<ServiceOrder>>> GetAsync()
        {
            var service_order = await _context.ServiceOrders
                .OrderBy(x => x.State)
                .ToListAsync();
            return new ActionResponse<IEnumerable<ServiceOrder>>
            {
                WasSuccess = true,
                Result = service_order
            };
        }

        public override async Task<ActionResponse<IEnumerable<ServiceOrder>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.ServiceOrders
                .Include(c => c.State)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.State.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<ServiceOrder>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.State)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.ServiceOrders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.State.ToLower().Contains(pagination.Filter.ToLower()));
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
