using GSP.DAL.Context.Mappings.Contract;
using GSP.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.DAL.Context.Mappings
{
    public class OrderGameMapping : IMappingContract<OrderGame>
    {
        public void MapEntity(EntityTypeBuilder<OrderGame> builder)
        {
            builder.ToTable("OrderGames", "core");

            builder.HasKey(x => x.OrderGameId);

            builder.HasOne(x => x.Game).WithMany(x => x.Orders).HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.Order).WithMany(x => x.Games).HasForeignKey(x => x.OrderId);
        }
    }
}
