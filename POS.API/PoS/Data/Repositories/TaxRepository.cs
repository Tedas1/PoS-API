using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;
using PoS.Dto;


namespace PoS.Data.Repositories
{
    public class TaxRepository : Repository<Tax>, ITaxRepository
    {
        private readonly ITaxOrderRepository _taxOrderRepository;

        public TaxRepository(
            IApplicationDbContext context,
            ITaxOrderRepository taxOrderRepository
        ) : base(context)
        {
            _taxOrderRepository = taxOrderRepository;
        }

        public async Task<bool> AssignTaxToOrder(TaxOrderDto taxOrder, Guid orderId)
        {
            Tax? tax = await Get(x => x.TaxId == taxOrder.TaxId);
            if (tax == null) return false;

            await _taxOrderRepository.Create(new TaxOrder(tax.TaxId, orderId));

            await _taxOrderRepository.Save();

            return true;

        }
    }
}
