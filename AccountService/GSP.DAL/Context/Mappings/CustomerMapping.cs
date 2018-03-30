using GSP.Accounts.Domain.Customers;
using GSP.Common.DAL.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Accounts.DAL.Context.Mappings
{
    public class CustomerMapping : IMappingContract<Customer>
    {
        public void MapEntity(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "core");
        }
    }
}
