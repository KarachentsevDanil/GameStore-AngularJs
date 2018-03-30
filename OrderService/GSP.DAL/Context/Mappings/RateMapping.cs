using GSP.DAL.Context.Mappings.Contract;
using GSP.Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.DAL.Context.Mappings
{
    public class RateMapping : IMappingContract<Rate>
    {
        public void MapEntity(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("Rates", "core");

            builder.HasKey(x => x.RateId);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Rates)
                .HasForeignKey(x => x.CustomerId);

            builder.HasOne(x => x.Game).WithMany(x => x.Rates).HasForeignKey(x => x.GameId);
        }
    }
}
