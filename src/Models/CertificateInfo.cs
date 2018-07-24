using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotWeb.Models
{
    public class CertificateInfo
    {
        public string SubjectName { get; set; }

        public string FriendlyName { get; set; }

        public string PEM { get; set; }
    }
}
