using Pharmacy.Web.VWModels.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Web.VWModels.Account
{
    public class ProfileVWModel
    {
        public string Email { get; set; }
        public DetailsPatientVWModel Patient { get; set; }
    }
}
