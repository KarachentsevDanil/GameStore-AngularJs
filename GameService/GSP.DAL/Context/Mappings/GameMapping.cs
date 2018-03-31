using GSP.Common.DAL.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Games.DAL.Context.Mappings
{
    public class GameMapping : IMappingContract<Domain.Games.Game>
    {
        public void MapEntity(EntityTypeBuilder<Domain.Games.Game> builder)
        {
            builder.ToTable("Games", "core");

            builder.HasKey(x => x.GameId);

            builder.HasOne(x => x.Category).WithMany(x => x.Games).HasForeignKey(x => x.CategoryId);
        }
    }
}
