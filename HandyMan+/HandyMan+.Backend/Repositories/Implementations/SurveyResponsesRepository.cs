using HandyMan_.Backend.Data;
using HandyMan_.Backend.Helpers;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Responses;
using Microsoft.EntityFrameworkCore;

namespace HandyMan_.Backend.Repositories.Implementations
{

        public class SurveyResponsesRepository : GenericRepository<SurveyResponseDTO>, ISurveyResponsesRepository
        {
            private readonly DataContext _context;

            public SurveyResponsesRepository(DataContext context) : base(context)
            {
                _context = context;
            }

            public async Task<IEnumerable<SurveyResponseDTO>> GetComboAsync()
            {
                return await _context.SurveyResponses
                    .OrderBy(x => x.Id)
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

            public async override Task<ActionResponse<IEnumerable<SurveyResponseDTO>>> GetAsync(PaginationDTO pagination)
            {
                var queryable = _context.SurveyResponses.AsQueryable();

                if (!string.IsNullOrWhiteSpace(pagination.Filter))
                {
                    queryable = queryable.Where(x => x.Id.ToString().ToLower().Contains(pagination.Filter.ToLower()));
                }

                return new ActionResponse<IEnumerable<SurveyResponseDTO>>
                {
                    WasSuccess = true,
                    Result = await queryable
                        .OrderBy(x => x.Id)
                        .Paginate(pagination)
                        .ToListAsync()
                };
            }

        }
    
}
