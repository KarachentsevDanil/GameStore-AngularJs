﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSP.DAL.Context.Mappings.Contract
{
    public interface IMappingContract<T> where T : class
    {
        void MapEntity(EntityTypeBuilder<T> builder);
    }
}
