using GSP.DAL.EF.Context.Mappings.Contract;
using GSP.Game.Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Game.DAL.EF.Context.Mappings
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