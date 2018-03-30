using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Common.DAL.Mapping
{
    public interface IMappingContract<T> where T : class
    {
        void MapEntity(EntityTypeBuilder<T> builder);
    }
}