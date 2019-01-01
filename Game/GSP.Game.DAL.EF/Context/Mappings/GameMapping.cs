using GSP.DAL.EF.Context.Mappings.Contract;
using GSP.Game.Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Game.DAL.EF.Context.Mappings
{
    public class GameMapping : IMappingContract<GameBase>
    {
        public void MapEntity(EntityTypeBuilder<GameBase> builder)
        {
            builder.ToTable("Games", "core");

            builder.HasKey(x => x.GameId);

            builder.HasOne(x => x.Category).WithMany(x => x.Games).HasForeignKey(x => x.CategoryId);
        }
    }
}