using GSP.DAL.EF.Context.Mappings.Contract;
using GSP.Order.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Order.DAL.EF.Context.Mappings
{
    public class OrderMapping : IMappingContract<OrderBase>
    {
        public void MapEntity(EntityTypeBuilder<OrderBase> builder)
        {
            builder.ToTable("Orders", "core");

            builder.HasKey(x => x.OrderId);
        }
    }
}