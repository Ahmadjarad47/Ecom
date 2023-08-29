using Ecom.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Data.Config
{
    public class ProdectConfigration : IEntityTypeConfiguration<Prodect>
    {
        public void Configure(EntityTypeBuilder<Prodect> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(x => x.Category).WithMany()
                .HasForeignKey(x => x.CategoryId);

            builder.HasData(new Prodect
            {
                Id = 1,
                Name = "Prodect1",
                Description = "Description",
                Price=200,CategoryId=2,
                PictuerProdect="https://"
            }, new Prodect
            {
                Id = 2,
                Name = "Prodect2",
                Description = "Description",
                Price=2001,
                CategoryId=1,
                PictuerProdect = "https://"
            });
        }
    }
}
