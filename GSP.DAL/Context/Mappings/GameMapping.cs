using GSP.DAL.Context.Mappings.Contract;
using GSP.Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.DAL.Context.Mappings
{
    public class GameMapping : IMappingContract<Game>
    {
        public void MapEntity(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games", "core");

            builder.HasKey(x => x.GameId);

            builder.HasOne(x => x.Category).WithMany(x => x.Games).HasForeignKey(x => x.CategoryId);
        }
    }
}
