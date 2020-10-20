using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Signing
{
    class MyExternalSignatureContainer : IExternalSignatureContainer
    {
        protected X509Certificate[] chain;
        protected byte[] signedHash;
        protected byte[] hash;

        public MyExternalSignatureContainer(byte[] signedHash, X509Certificate[] chain, byte[] hash)
        {
            this.signedHash = signedHash;
            this.chain = chain;
            this.hash = hash;
        }

        public byte[] Sign(Stream inputStream)
        {
            try
            {
                String hashAlgorithm = DigestAlgorithms.SHA256;
                PdfPKCS7 sgn = new PdfPKCS7(null, chain, hashAlgorithm, false);
                byte[] hashh = DigestAlgorithms.Digest(inputStream, hashAlgorithm);
                sgn.SetExternalDigest(signedHash, null, "RSA");

                return sgn.GetEncodedPKCS7(hashh, PdfSigner.CryptoStandard.CMS, null,
                    null, null);
            }
            catch (IOException ioe)
            {
                throw new Exception(ioe.Message);
            }
        }

        public void ModifySigningDictionary(PdfDictionary signDic)
        {
        }
    }
}
