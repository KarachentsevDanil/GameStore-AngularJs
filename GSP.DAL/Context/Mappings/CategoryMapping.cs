using GSP.DAL.Context.Mappings.Contract;
using GSP.Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.DAL.Context.Mappings
{
    public class CategoryMapping : IMappingContract<Category>
    {
        public void MapEntity(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "core");

            builder.HasKey(x => x.CategoryId);
        }
    }
}
