using Spire.Pdf;
using Spire.Pdf.Security;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SecretToken
{
    class Program
    {
        static void Main(string[] args)
        {
            double timevalidityMin = 1.2;
            Console.WriteLine(GenerateSignToken(timevalidityMin));
        }

        public static string GenerateSignToken(double timeValidityMin)
        {
            string equalsSign = ":=";
            string timeCreated = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
            string tokenTimeInfo = "validityTimeMinutes" + equalsSign + timeValidityMin + ";" +
            "timeCreated" + equalsSign + timeCreated;
            string signature = SignData(tokenTimeInfo);
            string secureToken = tokenTimeInfo + ";" + "signature" + equalsSign + signature;
            return Base64UrlEncode(secureToken);
        }
        public static string SignData(string stringToSign)
        {

            X509Certificate2 cert = new X509Certificate2("C:\\Users\\CSVarazdin\\Documents\\GitHub\\TimeRedistribution\\MedApp\\SecretToken\\mediion.com.p12", "Cert1f1kat");

            PdfDocument doc = new PdfDocument(@"C:\Users\CSVarazdin\Documents\GitHub\TimeRedistribution\MedApp\SecretToken\blepositioning_thesis_corbacho.pdf");
            PdfPageBase page = doc.Pages[0];

            PdfCertificate certi = new PdfCertificate(cert);


            PdfSignature signature = new PdfSignature(doc, page, certi, "demo");


            byte[] dataToSign = Encoding.UTF8.GetBytes(stringToSign);
            RSA privKey = cert.GetRSAPrivateKey();
            var a = privKey.SignData(dataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            bool ver = privKey.VerifyData(dataToSign, a, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            Console.WriteLine(ver);
            return Convert.ToBase64String(a).Replace('+', '-').Replace('/',
            '_').Replace("=", "");
        }
        private static string Base64UrlEncode(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes).Replace('+', '-').Replace('/',
            '_').Replace("=", "");
        }
    }
}
