using GSP.Common.DAL.Mapping;
using GSP.Orders.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Orders.DAL.Context.Mappings
{
    public class PaymentMapping : IMappingContract<Payment>
    {
        public void MapEntity(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments", "core");

            builder.HasKey(x => x.PaymentId);
        }
    }
}