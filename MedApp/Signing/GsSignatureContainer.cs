using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using iText.Kernel.Pdf;
using iText.Signatures;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Tsp;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace Signing
{
    public class GsSignatureContainer : IExternalSignatureContainer
    {
        private PdfDictionary sigDic;

        /**Api credentials. Key*/
        private readonly String apiKey = "API key";

        /**Api credentials. secret*/
        private readonly String apiSecret = "API secret";

        private readonly X509Certificate2Collection _clientCertificates;

        public GsSignatureContainer(X509Certificate2Collection clientCertificates, PdfName filter,
            PdfName subFilter)
        {
            sigDic = new PdfDictionary();
            sigDic.Put(PdfName.Filter, filter);
            sigDic.Put(PdfName.SubFilter, subFilter);
            this._clientCertificates = clientCertificates;
        }

        public byte[] Sign(Stream data)
        {
            //get JSON access token
            JObject access = ApiConnect.Login(_clientCertificates, apiKey, apiSecret);

            //get JSON with id/certificate/ocsp response
            JObject identity = ApiConnect.Identity(_clientCertificates, access);
            String cert = (String)identity.GetValue("signing_cert");
            String id = (String)identity.GetValue("id");
            String oc1 = (String)identity.GetValue("ocsp_response");
            JObject path = ApiConnect.CertificatePath(_clientCertificates, access);
            String ca = (String)path.GetValue("path");


            X509Certificate[] chain = CreateChain(cert, ca);


            //OCSP
            byte[] oc2 = Convert.FromBase64String(oc1);
            OcspResp ocspResp = new OcspResp(oc2);

            BasicOcspResp basicResp = (BasicOcspResp)ocspResp.GetResponseObject();
            byte[] oc = basicResp.GetEncoded();
            Collection ocspCollection = new Collection();
            ocspCollection.Add(oc);
            String hashAlgorithm = DigestAlgorithms.SHA256;
            PdfPKCS7 sgn = new PdfPKCS7(null, chain, hashAlgorithm, false);

            byte[] hash = DigestAlgorithms.Digest(data, DigestAlgorithms.GetMessageDigest(hashAlgorithm));

            byte[] sh = sgn.GetAuthenticatedAttributeBytes(hash, PdfSigner.CryptoStandard.CADES, ocspCollection,
                null);

            //create sha256 message digest
            using (SHA256 sha256 = SHA256.Create())
            {
                sh = sha256.ComputeHash(sh);
            }

            //create hex encoded sha256 message digest
            String hexencodedDigest = Hex.ToHexString(sh);

            JObject signed = ApiConnect.Sign(_clientCertificates, id, hexencodedDigest, access);
            String sig = (String)signed.GetValue("signature");

            //decode hex signature
            byte[] dsg = Hex.Decode(sig);

            //include signature on PDF
            sgn.SetExternalDigest(dsg, null, "RSA");

            //create TimeStamp Client
            ITSAClient tsc = new GSTSAClient(_clientCertificates, access);

            return sgn.GetEncodedPKCS7(hash, PdfSigner.CryptoStandard.CADES, tsc, ocspCollection, null);
        }

        public Collection getOCSP(String ocspResponse)
        {
            byte[] oc2 = Convert.FromBase64String(ocspResponse);
            OcspResp ocspResp = new OcspResp(oc2);
            BasicOcspResp basicResp = (BasicOcspResp)ocspResp.GetResponseObject();
            Collection ocspCollection = new Collection();
            ocspCollection.Add(basicResp.GetEncoded());
            return ocspCollection;
        }

        public void ModifySigningDictionary(PdfDictionary signDic)
        {
            signDic.PutAll(sigDic);
        }

        private static X509Certificate[] CreateChain(String cert, String ca)
        {
            X509Certificate[] chainy = new X509Certificate[2];

            X509CertificateParser parser = new X509CertificateParser();

            chainy[0] = new X509Certificate(parser.ReadCertificate(Encoding.UTF8.GetBytes(cert))
                .CertificateStructure);
            chainy[1] = new X509Certificate(parser.ReadCertificate(Encoding.UTF8.GetBytes(ca))
                .CertificateStructure);

            return chainy;
        }

        /** Timestamp client for GlobalSign API*/
        class GSTSAClient : ITSAClient
        {
            public static int DEFAULTTOKENSIZE = 4096;
            public static String DEFAULTHASHALGORITHM = "SHA-256";
            private JObject accessToken;

            private readonly X509Certificate2Collection _clientCertificates;

            public GSTSAClient(X509Certificate2Collection clientCertificates, JObject accessToken)
            {
                this.accessToken = accessToken;
                this._clientCertificates = clientCertificates;
            }

            public IDigest GetMessageDigest()
            {
                return new Sha256Digest();
            }

            public byte[] GetTimeStampToken(byte[] imprint)
            {
                TimeStampRequestGenerator tsqGenerator = new TimeStampRequestGenerator();
                tsqGenerator.SetCertReq(true);

                BigInteger nonce = BigInteger.ValueOf((long)(new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds);

                TimeStampRequest request = tsqGenerator.Generate(new DerObjectIdentifier(
                        DigestAlgorithms.GetAllowedDigest(DEFAULTHASHALGORITHM)),
                    imprint, nonce);

                JObject time = ApiConnect.Timestamp(_clientCertificates,
                    Hex.ToHexString(request.GetMessageImprintDigest()), accessToken);
                String tst = (String)time.GetValue("token");
                byte[] token = Base64.Decode(tst);

                CmsSignedData cms = new CmsSignedData(token);

                TimeStampToken tstToken = new TimeStampToken(cms);
                return tstToken.GetEncoded();
            }

            public int GetTokenSizeEstimate()
            {
                return DEFAULTTOKENSIZE;
            }
        }
    }
}
