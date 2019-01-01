using GSP.DAL.EF.Context.Mappings.Contract;
using GSP.Order.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Order.DAL.EF.Context.Mappings
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