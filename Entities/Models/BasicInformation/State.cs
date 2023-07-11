using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.Basic;

namespace Entities.Models.BasicInformation
{
    public class State : BaseEntity<int>
    {
        public string Title { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<City> Cites { get; set; }
    }
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.HasOne(p => p.Country).WithMany(c => c.States).HasForeignKey(p => p.CountryId);
        }
    }
}
