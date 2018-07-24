using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotWeb.Models
{
    public class CreateCertificationAuthorityModel
    {
        public string FriendlyName { get; set; }

        public int ValidityYears { get; set; }
    }
}
