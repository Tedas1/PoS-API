using PoS.Dto;
using PoS.Entities;

namespace PoS.Abstractions.Repositories.EntityRepositories
{
    public interface ITaxRepository : IRepository<Tax>
    {
        Task<bool> AssignTaxToOrder(TaxOrderDto taxOrder, Guid orderId);
    }
}
