using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OcelotWeb.Models
{
    public static class X509Certificate2Extensions
    {
        public static string ToPem(this X509Certificate2 certificate)
        {
            var base64 = Convert.ToBase64String(certificate.RawData, Base64FormattingOptions.InsertLineBreaks);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("-----BEGIN CERTIFICATE-----");
            builder.AppendLine(base64);
            builder.AppendLine("-----END CERTIFICATE-----");

            return builder.ToString();
        }
    }
}
