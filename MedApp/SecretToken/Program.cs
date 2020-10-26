using iText.Kernel.Pdf;
using iText.Signatures;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Asn1;

namespace SecretToken
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(PdfSigner()); 
        }

        //public static string GenerateSignToken(double timeValidityMin)
        //{
        //    string equalsSign = ":=";
        //    string timeCreated = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
        //    string tokenTimeInfo = "validityTimeMinutes" + equalsSign + timeValidityMin + ";" +
        //    "timeCreated" + equalsSign + timeCreated;
        //    string signature = SignData(tokenTimeInfo);
        //    string secureToken = tokenTimeInfo + ";" + "signature" + equalsSign + signature;
        //    return Base64UrlEncode(secureToken);
        //}
        //public static string SignData(string stringToSign)
        //{
        //    X509Certificate2 cert = new X509Certificate2("C:\\Users\\CSVarazdin\\Documents\\GitHub\\TimeRedistribution\\MedApp\\SecretToken\\mediion.com.p12", "Cert1f1kat");

        //    PdfDocument doc = new PdfDocument(@"C:\Users\CSVarazdin\Documents\GitHub\TimeRedistribution\MedApp\SecretToken\blepositioning_thesis_corbacho.pdf");
        //    PdfPageBase page = doc.Pages[0];

        //    PdfCertificate certi = new PdfCertificate(cert);


        //    PdfSignature signature = new PdfSignature(doc, page, certi, "demo");


        //    byte[] dataToSign = Encoding.UTF8.GetBytes(stringToSign);
        //    RSA privKey = cert.GetRSAPrivateKey();
        //    var a = privKey.SignData(dataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        //    bool ver = privKey.VerifyData(dataToSign, a, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        //    Console.WriteLine(ver);
        //    return Convert.ToBase64String(a).Replace('+', '-').Replace('/',
        //    '_').Replace("=", "");
        //}
        ////private static string Base64UrlEncode(string input)
        ////{
        ////    var inputBytes = Encoding.UTF8.GetBytes(input);
        ////    return Convert.ToBase64String(inputBytes).Replace('+', '-').Replace('/',
        ////    '_').Replace("=", "");
        ////}

        //public static byte[] PdfSigner()
        //{
        //    byte[] Hash = null;

        //    PdfReader reader = new PdfReader(@"C:\\Users\CSVarazdin\Desktop\Kristijan_Delač_Ugovor.PDF");
        //    FileStream fout = new FileStream(@"C:\\Users\CSVarazdin\Documents\GitHub\TimeRedistribution\MedApp\SecretToken\prep.pdf", FileMode.Create);

        //        StampingProperties sp = new StampingProperties();
        //        sp.UseAppendMode();

        //        PdfSigner pdfSigner = new PdfSigner(reader, fout, sp);
        //        pdfSigner.SetFieldName("Signature");

        //        PdfSignatureAppearance appearance = pdfSigner.GetSignatureAppearance();
        //        appearance.SetPageNumber(1);

        //        int estimatedSize = 12000;
        //        ExternalHashingSignatureContainer container = new ExternalHashingSignatureContainer(PdfName.Adobe_PPKLite, PdfName.Adbe_pkcs7_detached);
        //        pdfSigner.SignExternalContainer(container, estimatedSize);
        //        Hash = container.Hash;
        //     return Hash;
        //}

        //public static void SignPdf()
        //{
        //    byte[] signatureBytes = Convert.FromBase64String("base64Signature=iEo1%2FrYXLx4%2FZKYJE4pHA" +
        //        "JBdq6e0pi3gQhEsSnLnDFnM2V2upC7GNLTw2NHV0ETq0op3qFCVL4GMPmQhFlCZ%2F%2FvOh9vo1XRwlrCZczp" +
        //        "rIv3uGsZ3YlNjZMKAZn6wm2fpecmTHycXqPTg%2FXEccVoGKg4gXsSUZmkqDRWt2QlT1ZhJNWkVrSa%2Fb4lGt" +
        //        "LdRd7eFV4kAr3FBP%2F7brzhqDwnI12KVmq9sYKyTvCsHckhDQ%2BQKgMPc4dcIw1VL2ln%2FgMstuQaXktZBT" +
        //        "LBQfCVT3TOx7EXLC4xxOW3cXWY3ZSMrSV%2F2Do9RVDJU04NRFY9UAHcEUVEhWRQe0Z2wRSMS9aIRIw%3D%3D");


        //    byte[] certificateBytes = Convert.FromBase64String("base64Certificate=MIIIPzCCBiegAwIBAgIQV" +
        //        "QPDYm1sN8UAAAAAXaWiWjANBgkqhkiG9w0BAQsFADBIMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWp" +
        //        "za2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBDQSAyMDE0MB4XDTIwMDgxMjA2MTAwMVoXDTIyMDgxM" +
        //        "jA2MTAwMVowgc0xCzAJBgNVBAYTAkhSMTkwNwYDVQQKDDBPUMSGQSBCT0xOSUNBIFpBQk9LIEkgQk9MTklDQSB" +
        //        "IUlZBVFNLSUggVkVURVJBTkExFjAUBgNVBGETDUhSMzQ5MzgxNTg1OTkxDjAMBgNVBAcTBVpBQk9LMQ8wDQYDV" +
        //        "QQEEwZQQVJMQUoxEjAQBgNVBCoTCVZBTEVOVElOQTEZMBcGA1UEAxMQVkFMRU5USU5BIFBBUkxBSjEbMBkGA1U" +
        //        "EBRMSSFIyMDcwMzU5OTUzOS4yLjM1MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtXS2gIscB%2B7" +
        //        "pWjw9wUV8totcjBeUWPILEdzaHbjcs9eZlE2WIHPs5DeEv3T11NfKTdcwGHX1tpR9EE5tcgbvM%2FeUPEbKT7l" +
        //        "zKmbO1WqyI%2BbCLN8LgqhfC%2BfRLQBaAEl%2BNLbXjjce6Jhze3RLI1amtXbP311A1DCmamIFtBrX8JQUtsm" +
        //        "c90kWPHQeFagmdku%2FVlHdBeVrhE%2F9jmE7Wx%2FEXQI3Y1AgNY6fDicHxqbUqoJPvIZD7X8gJ77NKCFH5qQ" +
        //        "V1wYZ%2Bdvk%2FIs2%2Fk57C9nAgxlYKpsxp%2Fzc7eB2QL2895aoLo%2BKzSEA2X6OI%2Fa7y4MTE8G%2FCzT" +
        //        "l6Vc38r76nXHy9wIDAQABo4IDnTCCA5kwDgYDVR0PAQH%2FBAQDAgZAMIHMBgNVHSAEgcQwgcEwgbMGCSt8iFA" +
        //        "FIAwGAjCBpTBMBggrBgEFBQcCARZAaHR0cHM6Ly93d3cuZmluYS5oci9yZWd1bGF0aXZhLWRva3VtZW50aS1pL" +
        //        "XBvdHZyZGUtby1zdWtsYWRub3N0aTBVBggrBgEFBQcCARZJaHR0cHM6Ly93d3cuZmluYS5oci9lbi9sZWdpc2x" +
        //        "hdGlvbi1kb2N1bWVudHMtYW5kLWNvbmZvcm1hbmNlLWNlcnRpZmljYXRlczAJBgcEAIvsQAEAMH0GCCsGAQUFB" +
        //        "wEBBHEwbzAoBggrBgEFBQcwAYYcaHR0cDovL2RlbW8yMDE0LW9jc3AuZmluYS5ocjBDBggrBgEFBQcwAoY3aHR" +
        //        "0cDovL2RlbW8tcGtpLmZpbmEuaHIvY2VydGlmaWthdGkvZGVtbzIwMTRfc3ViX2NhLmNlcjCBowYIKwYBBQUHA" +
        //        "QMEgZYwgZMwCAYGBACORgEBMHIGBgQAjkYBBTBoMDIWLGh0dHBzOi8vZGVtby1wa2kuZmluYS5oci9wZHMvUER" +
        //        "TUUMxLTAtZW4ucGRmEwJlbjAyFixodHRwczovL2RlbW8tcGtpLmZpbmEuaHIvcGRzL1BEU1FDMS0wLWhyLnBkZ" +
        //        "hMCaHIwEwYGBACORgEGMAkGBwQAjkYBBgEwLAYDVR0RBCUwI4EhdmFsZW50aW5hLnBhcmxhakBib2xuaWNhLXp" +
        //        "hYm9rLmhyMIIBGAYDVR0fBIIBDzCCAQswgaaggaOggaCGKGh0dHA6Ly9kZW1vLXBraS5maW5hLmhyL2NybC9kZ" +
        //        "W1vMjAxNC5jcmyGdGxkYXA6Ly9kZW1vLWxkYXAuZmluYS5oci9jbj1GaW5hJTIwRGVtbyUyMENBJTIwMjAxNCx" +
        //        "vPUZpbmFuY2lqc2thJTIwYWdlbmNpamEsYz1IUj9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0JTNCYmluYXJ5M" +
        //        "GCgXqBcpFowWDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExF" +
        //        "GaW5hIERlbW8gQ0EgMjAxNDEOMAwGA1UEAxMFQ1JMMjAwHwYDVR0jBBgwFoAUO4RaFPXFPOFIO13RJzV71WW8D" +
        //        "iowHQYDVR0OBBYEFNdrwjBEWTFv578Vd%2BLgQi1V3ZtOMAkGA1UdEwQCMAAwDQYJKoZIhvcNAQELBQADggIBA" +
        //        "K1tLzip%2BWwndW0QfivX312qkxDebVY7Dq8I5hDJODiVCLWju8UzsaHhOYFgtht8YndVRvgZGEzSss1O1Wv%2" +
        //        "FX%2FzPbi1dGC4QcBPZQmTGZw5UunIWSr0E82Es0uuj6ZwHM8qS7nmWP1Jg4zNghgKs8ryjJnzB4WgAv0fnxQq" +
        //        "bGC7I2dzC%2BUPRgohQ%2Bw7J3fWua3FY4Xfd4OEuf%2BUEe97wxlcWCVFVua%2FVWsmfY79R0gw48Mgk52fC5" +
        //        "HY2gQ0WswSL0nhm4qcWxdX2pMEf%2BH5JtvIfbL2OwIp3KirFf61hFhnqkxVogU6wN09IOBN2p43mW2C9XX%2F" +
        //        "oC0Ckz%2Bo5BGIUoHjp2QA3c6ag8RlPltyzzEvI4kwnL5igYPxugQfuA6u3piCyDsc%2BZ6gKtM4C543HFANFM" +
        //        "Fvga3gvQNONoJvevyK6bslyWyIwFotTCxntfHxTr%2FR40IXxV1JiRYb1BiCYHh5c56XTePNSShF5BfD1Qw5uH" +
        //        "5PNuyEN69NkiE0BeWoisq8fWJoUGsaVjasa8QzegMpfhObp22H8qA7T50G0GtXRsN6tG5Wol3zeOTu6zw1j%2F" +
        //        "5wTR6ipXJT3hgSy9GPVhhov7fQhNplAGKdDIEnaPjijdcYzpKyF%2FJwXfP0r6I4bWmMwACbcJVpip3k%2FC7s" +
        //        "XTq%2BDsn9yZosHIWzxlHLy%2BY%2BM");

        //    X509Certificate x509Certificate = new X509CertificateParser().ReadCertificate(certificateBytes);

        //    SignerIdentifier sid = new SignerIdentifier(new IssuerAndSerialNumber(x509Certificate.IssuerDN, x509Certificate.SerialNumber));
        //    AlgorithmIdentifier digAlgorithm = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha256);
        //    Attributes authenticatedAttributes = null;
        //    AlgorithmIdentifier digEncryptionAlgorithm = new AlgorithmIdentifier(Org.BouncyCastle.Asn1.Pkcs.PkcsObjectIdentifiers.Sha256WithRsaEncryption);
        //    Asn1OctetString encryptedDigest = new DerOctetString(signatureBytes);
        //    Attributes unauthenticatedAttributes = null;
        //    SignerInfo signerInfo = new SignerInfo(sid, digAlgorithm, authenticatedAttributes, digEncryptionAlgorithm, encryptedDigest, unauthenticatedAttributes);

        //    Asn1EncodableVector digestAlgs = new Asn1EncodableVector();
        //    digestAlgs.Add(signerInfo.DigestAlgorithm);
        //    Asn1Set digestAlgorithms = new DerSet(digestAlgs);
        //    ContentInfo contentInfo = new ContentInfo(CmsObjectIdentifiers.Data, null);
        //    Asn1EncodableVector certs = new Asn1EncodableVector();
        //    certs.Add(x509Certificate.CertificateStructure.ToAsn1Object());
        //    Asn1Set certificates = new DerSet(certs);
        //    Asn1EncodableVector signerInfs = new Asn1EncodableVector();
        //    signerInfs.Add(signerInfo);
        //    Asn1Set signerInfos = new DerSet(signerInfs);
        //    SignedData signedData = new SignedData(digestAlgorithms, contentInfo, certificates, null, signerInfos);

        //    contentInfo = new ContentInfo(CmsObjectIdentifiers.SignedData, signedData);

        //    byte[] Signature = contentInfo.GetDerEncoded();
        //}
    }
}
