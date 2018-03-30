using GSP.Common.DAL.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Game.DAL.Context.Mappings
{
    public class GameMapping : IMappingContract<Games.Domain.Games.Game>
    {
        public void MapEntity(EntityTypeBuilder<Games.Domain.Games.Game> builder)
        {
            builder.ToTable("Games", "core");

            builder.HasKey(x => x.GameId);

            builder.HasOne(x => x.Category).WithMany(x => x.Games).HasForeignKey(x => x.CategoryId);
        }
    }
}
