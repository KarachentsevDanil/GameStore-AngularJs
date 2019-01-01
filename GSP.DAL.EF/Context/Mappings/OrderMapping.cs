using GSP.DAL.EF.Context.Mappings.Contract;
using GSP.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.DAL.EF.Context.Mappings
{
    public class OrderMapping : IMappingContract<Order>
    {
        public void MapEntity(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "core");

            builder.HasKey(x => x.OrderId);

            builder.HasOne(x => x.Customer).WithMany(x => x.Orders).HasForeignKey(x => x.CustomerId);

            builder.HasOne(x => x.Payment).WithMany(x => x.Orders).HasForeignKey(t => t.PaymentId);
        }
    }
}