using GSP.DAL.EF.Context.Mappings.Contract;
using GSP.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.DAL.EF.Context.Mappings
{
    public class PaymentMapping : IMappingContract<Payment>
    {
        public void MapEntity(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments", "core");

            builder.HasKey(x => x.PaymentId);

            builder.HasOne(x => x.Customer).WithMany(x => x.Payments).HasForeignKey(t => t.CustomerId);
        }
    }
}