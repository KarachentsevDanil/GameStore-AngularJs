using System;
using System.Collections.Generic;
using System.Text;
using GSP.DAL.Context.Mappings.Contract;
using GSP.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.DAL.Context.Mappings
{
    public class OrderMapping : IMappingContract<Order>
    {
        public void MapEntity(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "core");

            builder.HasKey(x => x.OrderId);

            builder.HasOne(x => x.Customer).WithMany(x => x.Orders).HasForeignKey(x => x.CustomerId);
        }
    }
}
