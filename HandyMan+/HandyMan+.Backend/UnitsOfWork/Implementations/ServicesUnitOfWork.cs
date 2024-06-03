using HandyMan_.Backend.Repositories.Implementations;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Implementations
{
    public class ServicesUnitOfWork : GenericUnitOfWork<Service>, IServicesUnitOfWork
    {
        private readonly IServicesRepository _serviceRepository;

        public ServicesUnitOfWork(IGenericRepository<Service> repository, IServicesRepository serviceRepository) : base(repository)
        {
            _serviceRepository = serviceRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Service>>> GetAsync() => await _serviceRepository.GetAsync();
        public override async Task<ActionResponse<IEnumerable<Service>>> GetAsync(PaginationDTO pagination) => await _serviceRepository.GetAsync(pagination);
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _serviceRepository.GetTotalPagesAsync(pagination);
        public override async Task<ActionResponse<Service>> GetAsync(int id) => await _serviceRepository.GetAsync(id);

        public Task<IEnumerable<Service>> GetAllServices() => _serviceRepository.GetAllServices();

        public Task<ActionResponse<Service>> AddServicePhotoAsync(Service Service) => _serviceRepository.AddServicePhotoAsync(Service);
    }
}
