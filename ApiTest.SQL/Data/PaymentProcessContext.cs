using ApiTest.SQL.DBModels;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.SQL.Data
{
    public class PaymentProcessContext : DbContext
    {
        public PaymentProcessContext(DbContextOptions
                                    <PaymentProcessContext> options) : base(options)
        {
        }
        public DbSet<Payment> Payment { get; set; }
    }
}
