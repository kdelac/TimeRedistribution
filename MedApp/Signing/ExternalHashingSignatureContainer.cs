using iText.Kernel.Pdf;
using iText.Signatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Signing
{
    public class ExternalHashingSignatureContainer : ExternalBlankSignatureContainer
    {
        public ExternalHashingSignatureContainer(PdfName filter, PdfName subFilter) : base(filter, subFilter)
        { }

        public override byte[] Sign(Stream data)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            String hashAlgorithm = DigestAlgorithms.SHA256;
            Hash = DigestAlgorithms.Digest(data, DigestAlgorithms.GetMessageDigest(hashAlgorithm));
            return new byte[0];
        }

        public byte[] Hash { get; private set; }
    }
}
