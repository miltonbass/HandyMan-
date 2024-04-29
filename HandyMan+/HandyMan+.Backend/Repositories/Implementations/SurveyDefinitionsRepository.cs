using Microsoft.EntityFrameworkCore;
using HandyMan_.Backend.Data;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Responses;
using HandyMan_.Shered.DTOs;
using HandyMan_.Backend.Helpers;


namespace HandyMan_.Backend.Repositories.Implementations
{
    public class SurveyDefinitionsRepository : GenericRepository<SurveyDefinitionEntity>, ISurveyDefinitionsRepository
    {
        private readonly DataContext _context;

        public SurveyDefinitionsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SurveyDefinitionEntity>> GetComboAsync()
        {
            return await _context.SurveyDefinitions
                .OrderBy(x => x.Title)
                .ToListAsync();
        }



        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.SurveyDefinitions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Title.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public async override Task<ActionResponse<IEnumerable<SurveyDefinitionEntity>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.SurveyDefinitions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Title.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<SurveyDefinitionEntity>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Title)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }
    }
}
