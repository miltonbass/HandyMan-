using HandyMan_.Backend.Data;
using HandyMan_.Backend.Helpers;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;
using Microsoft.EntityFrameworkCore;

namespace HandyMan_.Backend.Repositories.Implementations
{
    public class ServicesRepository : GenericRepository<Service>, IServicesRepository
    {
        private readonly DataContext _context;

        public ServicesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Service>>> GetAsync()
        {
            var services = await _context.Services
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Service>>
            {
                WasSuccess = true,
                Result = services
            };
        }

        public override async Task<ActionResponse<IEnumerable<Service>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Services
                .Include(c => c.Category)
                .Include(p => p.People)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Service>>
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
            var queryable = _context.Services.AsQueryable();

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
        public override async Task<ActionResponse<Service>> GetAsync(int id)
        {
            var service = await _context.Services
                 .Include(c => c.Category!)
                 .Include(p => p.People)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (service == null)
            {
                return new ActionResponse<Service>
                {
                    WasSuccess = false,
                    Message = "Servicio no existe"
                };
            }

            return new ActionResponse<Service>
            {
                WasSuccess = true,
                Result = service
            };
        }
    }
}
