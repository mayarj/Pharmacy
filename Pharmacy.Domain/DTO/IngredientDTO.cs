﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Entities
{
    public  class IngredientDTO
    {
        public int Id { get;  set; }
        public string Name { get;  set; } = string.Empty;
        public string? Description { get; set; }
    }
}
