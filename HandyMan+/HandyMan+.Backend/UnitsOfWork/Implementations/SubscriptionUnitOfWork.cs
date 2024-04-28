using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Responses;
using HandyMan_.Shered.DTOs;

namespace HandyMan_.Backend.UnitsOfWork.Implementations
{
    public class SubscriptionUnitOfWork : GenericUnitOfWork<SubscriptionType>, ISubscriptionUnitOfWork
    {
        private readonly ISubscriptionTypeRepository _subscriptionTypeRepository;

        public SubscriptionUnitOfWork(IGenericRepository<SubscriptionType> repository, ISubscriptionTypeRepository subscriptionTypeRepository) : base(repository)
        {
            _subscriptionTypeRepository = subscriptionTypeRepository;
        }

        public override async Task<ActionResponse<IEnumerable<SubscriptionType>>> GetAsync(PaginationDTO pagination) => await _subscriptionTypeRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _subscriptionTypeRepository.GetTotalPagesAsync(pagination);

        public async Task<IEnumerable<SubscriptionType>> GetComboAsync() => await _subscriptionTypeRepository.GetComboAsync();
    }
}
