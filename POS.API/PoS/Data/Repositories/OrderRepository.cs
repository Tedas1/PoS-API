using Microsoft.EntityFrameworkCore;
using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;
using System;

namespace PoS.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
