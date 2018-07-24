using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OcelotWeb.Models
{
    public class CertificatesManager
    {
        public const int RSAMinimumKeySizeInBits = 2048;

        public X509Certificate2 CreateCertificationAuthorityCertificate(DateTimeOffset notBefore, DateTimeOffset notAfter, string subjectName, string friendlyName)
        {
            var subject = new X500DistinguishedName(subjectName);
            var extensions = new List<X509Extension>();

            var keyUsage = new X509KeyUsageExtension(X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.CrlSign, critical: true);

            var basicConstraints = new X509BasicConstraintsExtension(
                certificateAuthority: true,
                hasPathLengthConstraint: false,
                pathLengthConstraint: 3,
                critical: true);

            extensions.Add(basicConstraints);
            extensions.Add(keyUsage);

            var certificate = CreateSelfSignedCertificate(subject, extensions, notBefore, notAfter);
            certificate.FriendlyName = friendlyName;

            return certificate;
        }

        private X509Certificate2 CreateSelfSignedCertificate(X500DistinguishedName subject, IEnumerable<X509Extension> extensions, DateTimeOffset notBefore, DateTimeOffset notAfter)
        {
            var key = CreateKeyMaterial(RSAMinimumKeySizeInBits);

            var request = new CertificateRequest(subject, key, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            foreach (var extension in extensions)
            {
                request.CertificateExtensions.Add(extension);
            }

            // https://tools.ietf.org/html/rfc5280#section-4.2.1.2
            // Conforming CAs MUST mark this extension as non-critical.
            var subjectKeyIdentifierExtension = new X509SubjectKeyIdentifierExtension(request.PublicKey, X509SubjectKeyIdentifierHashAlgorithm.Sha1, critical: false);
            request.CertificateExtensions.Add(subjectKeyIdentifierExtension);

            return request.CreateSelfSigned(notBefore, notAfter);
        }

        private static RSA CreateKeyMaterial(int minimumKeySize)
        {
            var rsa = RSA.Create(minimumKeySize);
            if (rsa.KeySize < minimumKeySize)
            {
                throw new InvalidOperationException($"Failed to create a key with a size of {minimumKeySize} bits");
            }

            return rsa;
        }
    }
}
