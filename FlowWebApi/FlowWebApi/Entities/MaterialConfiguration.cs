using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Entities
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x=>x.Id);
            entityTypeBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            entityTypeBuilder.HasOne(x => x.flow).WithMany(x => x.Materials).HasForeignKey(x => x.FlowId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
