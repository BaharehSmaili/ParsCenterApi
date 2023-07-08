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
    public class CityModel
    {
        public string Title { get; set; }

        public int StateId { get; set; }

        public StateModel State { get; set; }

    }

    public class CityConfiguration : IEntityTypeConfiguration<CityModel>
    {
        public void Configure(EntityTypeBuilder<CityModel> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.HasOne(p => p.State).WithMany(c => c.Cites).HasForeignKey(p => p.StateId);
        }
    }
}
