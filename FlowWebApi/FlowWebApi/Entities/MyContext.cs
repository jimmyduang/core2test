using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Entities
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<Flow> flows { set; get; }
        public DbSet<Material> materials { set; get; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=Light;User ID=sa;Password=12345678;");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //指定主键
            //modelBuilder.Entity<Flow>().HasKey(x=>x.Id);
            //modelBuilder.Entity<Flow>().Property(x=>x.Name).IsRequired().HasMaxLength(50);
            //modelBuilder.Entity<Flow>().Property(x => x.Price).IsRequired().HasColumnType("decimal(8,2)");

            modelBuilder.ApplyConfiguration(new FlowConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
        }
    }
}
