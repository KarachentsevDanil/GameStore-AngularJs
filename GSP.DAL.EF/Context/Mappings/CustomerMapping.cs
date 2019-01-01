using GSP.DAL.EF.Context.Mappings.Contract;
using GSP.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.DAL.EF.Context.Mappings
{
    public class CustomerMapping : IMappingContract<Customer>
    {
        public void MapEntity(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "core");
        }
    }
}
