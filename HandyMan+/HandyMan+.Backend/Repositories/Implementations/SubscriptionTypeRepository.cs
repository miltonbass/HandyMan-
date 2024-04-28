using Microsoft.EntityFrameworkCore;
using HandyMan_.Backend.Data;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Responses;
using HandyMan_.Shered.DTOs;
using Orders.Backend.Helpers;


namespace HandyMan_.Backend.Repositories.Implementations
{
    public class SubscriptionTypeRepository : GenericRepository<SubscriptionType>, ISubscriptionTypeRepository
    {
        private readonly DataContext _context;

        public SubscriptionTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubscriptionType>> GetComboAsync()
        {
            return await _context.SubscriptionTypes
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public override async Task<ActionResponse<IEnumerable<SubscriptionType>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.SubscriptionTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<SubscriptionType>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.SubscriptionTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
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
