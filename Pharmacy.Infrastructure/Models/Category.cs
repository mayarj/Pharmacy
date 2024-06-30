﻿using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models
{
    public partial class Category
    {
        public Category()
        {
            Medicines = new HashSet<Medicine>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}
