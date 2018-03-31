using GSP.Common.DAL.Mapping;
using GSP.Orders.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Orders.DAL.Context.Mappings
{
    public class OrderGameMapping : IMappingContract<OrderGame>
    {
        public void MapEntity(EntityTypeBuilder<OrderGame> builder)
        {
            builder.ToTable("OrderGames", "core");

            builder.HasKey(x => x.OrderGameId);

            builder.HasOne(x => x.Order).WithMany(x => x.Games).HasForeignKey(x => x.OrderId);
        }
    }
}
