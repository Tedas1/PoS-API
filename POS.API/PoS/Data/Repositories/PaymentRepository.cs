using Microsoft.EntityFrameworkCore;
using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;
using System;

namespace PoS.Data.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
