﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Entities
{
    public class EntitiePatient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get;  set; } = string.Empty;
        public string Address { get;  set; } = string.Empty;
        public decimal PhoneNumber { get;  set; }

        

    }
}


