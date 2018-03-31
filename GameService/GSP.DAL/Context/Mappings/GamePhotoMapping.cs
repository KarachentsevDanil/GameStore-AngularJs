using GSP.Common.DAL.Mapping;
using GSP.Games.Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Games.DAL.Context.Mappings
{
    public class GamePhotoMapping : IMappingContract<GamePhoto>
    {
        public void MapEntity(EntityTypeBuilder<GamePhoto> builder)
        {
            builder.ToTable("GamePhotos", "core");

            builder.HasKey(x => x.GamePhotoId);

            builder.HasOne(x => x.Game).WithMany(x => x.Photos).HasForeignKey(x => x.GameId);
        }
    }
}
