using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Signatures;
using System;
using System.IO;

namespace Signing
{
    class Program
    {
        static void Main(string[] args)
        {
            PdfReader reader = new PdfReader(SRC);
            using (FileStream os = new FileStream(DEST, FileMode.OpenOrCreate))
            {
                StampingProperties stampingProperties = new StampingProperties();
                //For any signature in the Pdf  but the first one, you need to use appendMode
                //        stampingProperties.useAppendMode();
                PdfSigner pdfSigner = new PdfSigner(reader, os, stampingProperties);
                //you can modify the signature appearance 
                PdfSignatureAppearance appearance = pdfSigner.GetSignatureAppearance();
                appearance.SetPageRect(new Rectangle(36, 508, 254, 200));
                appearance.SetPageNumber(1);
                appearance.SetLayer2FontSize(14f);
                appearance.SetReason("Test signing");
                IExternalSignatureContainer external = new GsSignatureContainer(
                    PdfName.Adobe_PPKLite,
                    PdfName.Adbe_pkcs7_detached);
                pdfSigner.SignExternalContainer(external, 8049);
            }
        }
    }
}
