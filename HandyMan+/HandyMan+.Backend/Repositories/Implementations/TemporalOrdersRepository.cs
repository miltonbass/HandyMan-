using HandyMan_.Backend.Data;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;
using Microsoft.EntityFrameworkCore;

namespace HandyMan_.Backend.Repositories.Implementations
{
    public class TemporalOrdersRepository : GenericRepository<TemporalOrder>, ITemporalOrdersRepository
    {
        private readonly DataContext _context;
        private readonly IUsersRepository _usersRepository;

        public TemporalOrdersRepository(DataContext context, IUsersRepository usersRepository) : base(context)
        {
            _context = context;
            _usersRepository = usersRepository;
        }

        public async Task<ActionResponse<TemporalOrder>> PutFullAsync(TemporalOrder temporalOrder)
        {
            var currentTemporalOrder = await _context.TemporalOrders.FirstOrDefaultAsync(x => x.Id == temporalOrder.Id);
            if (currentTemporalOrder == null)
            {
                return new ActionResponse<TemporalOrder>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            

            _context.Update(currentTemporalOrder);
            await _context.SaveChangesAsync();
            return new ActionResponse<TemporalOrder>
            {
                WasSuccess = true,
                Result = currentTemporalOrder
            };
        }

        public override async Task<ActionResponse<TemporalOrder>> GetAsync(int id)
        {
            var temporalOrder = await _context.TemporalOrders
                .Include(ts => ts.User!)
                .Include(ts => ts.Service!)
                .ThenInclude(p => p.Category!)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (temporalOrder == null)
            {
                return new ActionResponse<TemporalOrder>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ActionResponse<TemporalOrder>
            {
                WasSuccess = true,
                Result = temporalOrder
            };
        }

        public async Task<ActionResponse<TemporalOrder>> AddFullAsync(string email, TemporalOrder temporalOrder)
        {
            var service = _context.Services.FirstOrDefaultAsync(x => x.Id == temporalOrder.ServiceId);
            
            if (service == null)
            {
                return new ActionResponse<TemporalOrder>
                {
                    WasSuccess = false,
                    Message = "Servicio no existe"
                };
            }

            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
            {
                return new ActionResponse<TemporalOrder>
                {
                    WasSuccess = false,
                    Message = "Usuario no existe"
                };
            }

            temporalOrder.UserId = user.Id;
            temporalOrder.ServiceId = service.Id;
            try
            {
                _context.Add(temporalOrder);
                await _context.SaveChangesAsync();
                return new ActionResponse<TemporalOrder>
                {
                    WasSuccess = true,
                    Result = temporalOrder
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<TemporalOrder>
                {
                    WasSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email)
        {
            var temporalOrders = await _context.TemporalOrders
                .Include(to => to.User!)
                .Include(to => to.Service!)
                .ThenInclude(pc => pc.Category)
                .Where(x => x.User!.Email == email)
                .ToListAsync();

            return new ActionResponse<IEnumerable<TemporalOrder>>
            {
                WasSuccess = true,
                Result = temporalOrders
            };
        }

        public async Task<ActionResponse<int>> GetCountAsync(string email)
        {
            var count = await _context.TemporalOrders.Where(x => x.User!.Email == email).CountAsync();

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = (int)count
            };
        }

        public async Task<IEnumerable<TemporalOrder>> GetAllRequest()
        {
            return await _context.TemporalOrders
            .Include(ts => ts.User!)
                .Include(ts => ts.Service!)
                .ThenInclude(p => p.Category!).ToListAsync();

        }

    }
}