using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.basicInformation
{
    public class cityModel
    {
        public string title { get; set; }

        public int fk_state { get; set; }

        public stateModel state { get; set; }

    }

    public class cityConfiguration : IEntityTypeConfiguration<cityModel>
    {
        public void Configure(EntityTypeBuilder<cityModel> builder)
        {
            builder.Property(p => p.title).IsRequired().HasMaxLength(100);
            builder.HasOne(p => p.state).WithMany(c => c.cites).HasForeignKey(p => p.fk_state);
        }
    }
}
