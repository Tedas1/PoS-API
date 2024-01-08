using PoS.Dto;
using PoS.Entities;

namespace PoS.Abstractions.Repositories.EntityRepositories
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task<InvoiceDto> GetOrderInvoiceAsync(Order order);
    }
}
