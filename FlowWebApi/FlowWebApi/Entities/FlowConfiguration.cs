using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Entities
{
    public class FlowConfiguration: IEntityTypeConfiguration<Flow>
    {
        public void Configure(EntityTypeBuilder<Flow> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            entityTypeBuilder.Property(x => x.Price).IsRequired().HasColumnType("decimal(8,2)");
            entityTypeBuilder.Property(x => x.des).HasMaxLength(200);
        }
    }
}
