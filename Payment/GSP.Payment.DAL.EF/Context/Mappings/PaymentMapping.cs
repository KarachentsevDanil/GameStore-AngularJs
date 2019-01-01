using GSP.DAL.EF.Context.Mappings.Contract;
using GSP.Payment.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.Payment.DAL.EF.Context.Mappings
{
    public class PaymentMapping : IMappingContract<PaymentBase>
    {
        public void MapEntity(EntityTypeBuilder<PaymentBase> builder)
        {
            builder.ToTable("Payments", "core");

            builder.HasKey(x => x.PaymentId);
        }
    }
}