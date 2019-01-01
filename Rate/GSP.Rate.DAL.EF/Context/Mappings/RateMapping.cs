using GSP.DAL.EF.Context.Mappings.Contract;
using GSP.Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Rate.DAL.EF.Context.Mappings
{
    public class RateMapping : IMappingContract<RateBase>
    {
        public void MapEntity(EntityTypeBuilder<RateBase> builder)
        {
            builder.ToTable("Rates", "core");

            builder.HasKey(x => x.RateId);
        }
    }
}