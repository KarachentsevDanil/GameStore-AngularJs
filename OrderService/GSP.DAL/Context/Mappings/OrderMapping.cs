using GSP.Common.DAL.Mapping;
using GSP.Orders.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Orders.DAL.Context.Mappings
{
    public class OrderMapping : IMappingContract<Order>
    {
        public void MapEntity(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "core");

            builder.HasKey(x => x.OrderId);

            builder.HasOne(x => x.Payment).WithMany(x => x.Orders).HasForeignKey(t => t.PaymentId);
        }
    }
}