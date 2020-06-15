
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Signatures;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using System.IO;
using System.Threading.Tasks;

namespace MedAppServices
{
    public class SignService : ISignService
    {
        public void SignDocument(SigningDocument signingDocument)
        {
            string KEYSTORE = $"{signingDocument.CertificatDestination}";
            char[] PASSWORD = $"{signingDocument.Password}".ToCharArray();

            Pkcs12Store pk12 = new Pkcs12Store(new FileStream(KEYSTORE,
            FileMode.Open, FileAccess.Read), PASSWORD);
            string alias = null;
            foreach (object a in pk12.Aliases)
            {
                alias = ((string)a);
                if (pk12.IsKeyEntry(alias))
                {
                    break;
                }
            }

            ICipherParameters pk = pk12.GetKey(alias).Key;

            X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[ce.Length];
            for (int k = 0; k < ce.Length; ++k)
            {
                chain[k] = ce[k].Certificate;
            }

            var f = signingDocument.File.OpenReadStream();
            string DEST = $"{signingDocument.destinationSave}\\Signed{signingDocument.File.FileName}";

            PdfReader p = new PdfReader(f);
            PdfSigner signer = new PdfSigner(p, new FileStream(DEST, FileMode.Create),
            new StampingProperties());

            PdfSignatureAppearance appearance = signer.GetSignatureAppearance();
            appearance.SetLocation(signingDocument.Location)
                .SetPageRect(new Rectangle(425, 0, 150, 75))
                .SetPageNumber(1);
            signer.SetFieldName("MyFieldName");

            IExternalSignature pks = new PrivateKeySignature(pk, DigestAlgorithms.SHA256);

            signer.SignDetached(pks, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
        }
    }
}
