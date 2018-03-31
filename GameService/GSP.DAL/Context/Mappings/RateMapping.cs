using GSP.Common.DAL.Mapping;
using GSP.Games.Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Games.DAL.Context.Mappings
{
    public class RateMapping : IMappingContract<Rate>
    {
        public void MapEntity(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("Rates", "core");

            builder.HasKey(x => x.RateId);

            builder.HasOne(x => x.Game).WithMany(x => x.Rates).HasForeignKey(x => x.GameId);
        }
    }
}
