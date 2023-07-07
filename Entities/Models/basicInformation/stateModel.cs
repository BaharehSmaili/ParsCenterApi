using Entities.Models.basic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.basicInformation
{
    public class stateModel : baseEntity<int>
    {
        public string title { get; set; }

        public int fk_country { get; set; }

        public countryModel country { get; set; }

        public ICollection<cityModel> cites { get; set; }
    }
    public class stateConfiguration : IEntityTypeConfiguration<stateModel>
    {
        public void Configure(EntityTypeBuilder<stateModel> builder)
        {
            builder.Property(p => p.title).IsRequired().HasMaxLength(100);
            builder.HasOne(p => p.country).WithMany(c => c.states).HasForeignKey(p => p.fk_country);
        }
    }
}
