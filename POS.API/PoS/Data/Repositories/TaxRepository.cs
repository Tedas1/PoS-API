using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;
using PoS.Dto;


namespace PoS.Data.Repositories
{
    public class TaxRepository : Repository<Tax>, ITaxRepository
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITaxOrderRepository _taxOrderRepository;

        public TaxRepository(
            IApplicationDbContext context,
            IOrderRepository orderRepository,
            ITaxOrderRepository taxOrderRepository
        ) : base(context)
        {
            _orderRepository = orderRepository;
            _taxOrderRepository = taxOrderRepository;
        }

        public async Task<bool> AssignTaxToOrder(TaxOrderDto taxOrder, Guid orderId)
        {
            Order? order = await _orderRepository.Get(x => x.Id == orderId);
            if (order == null) return false;

            Tax? tax = await Get(x => x.TaxId == taxOrder.TaxId);
            if (tax == null) return false;

            await _taxOrderRepository.Create(new TaxOrder(tax.TaxId, order.Id));

            await _taxOrderRepository.Save();

            return true;

        }
    }
}
