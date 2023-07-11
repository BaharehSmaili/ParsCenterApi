using Entities.Models.Basic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.BasicInformation
{
    public class City : BaseEntity<int>
    {
        public string Title { get; set; }

        public int StateId { get; set; }

        public State State { get; set; }

    }


    //بجای استفاده از تگ های انوتیشن از کانفیگورها استفاده می کنیم
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.HasOne(p => p.State).WithMany(c => c.Cites).HasForeignKey(p => p.StateId);
        }
    }
}
