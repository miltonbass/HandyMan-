using HandyMan_.Backend.Data;
using HandyMan_.Backend.Helpers;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;
using Microsoft.EntityFrameworkCore;

namespace HandyMan_.Backend.Repositories.Implementations
{
    public class PeopleRepository : GenericRepository<People>, IPeopleRepository
    {
        private readonly DataContext _context;

        public PeopleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<People>> GetComboAsync()
        {
            return await _context.Peoples
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override async Task<ActionResponse<IEnumerable<People>>> GetAsync()
        {
            var peoples = await _context.Peoples
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<People>>
            {
                WasSuccess = true,
                Result = peoples
            };
        }

        public override async Task<ActionResponse<IEnumerable<People>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Peoples
                .Include(c => c.City)
                .Include(pt => pt.PeopleType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<People>>
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
            var queryable = _context.Peoples.AsQueryable();

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
        public override async Task<ActionResponse<People>> GetAsync(int id)
        {
            var people = await _context.Peoples
                 .Include(c => c.City)
                 .Include(pt => pt.PeopleType)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (people == null)
            {
                return new ActionResponse<People>
                {
                    WasSuccess = false,
                    Message = "Persona no existe"
                };
            }

            return new ActionResponse<People>
            {
                WasSuccess = true,
                Result = people
            };
        }
    }
}
