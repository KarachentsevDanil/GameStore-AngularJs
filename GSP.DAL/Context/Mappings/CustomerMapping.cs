using GSP.DAL.Context.Mappings.Contract;
using GSP.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.DAL.Context.Mappings
{
    public class CustomerMapping : IMappingContract<Customer>
    {
        public void MapEntity(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", "core");
        }
    }
}
