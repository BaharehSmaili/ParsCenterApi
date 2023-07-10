﻿using Entities.Models.Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.BasicInformation
{
    public class Country : BaseEntity<int>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string TitleEn { get; set; }

        public ICollection<State> States { get; set; }
    }
}