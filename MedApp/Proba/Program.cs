using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;
using SecretToken;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

namespace Proba
{
    class Program
    {
        private static readonly string certificate = "base64Certificate=MIIIPzCCBiegAwIBAgIQV" +
            "QPDYm1sN8UAAAAAXaWiWjANBgkqhkiG9w0BAQsFADBIMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluY" +
            "W5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBDQSAyMDE0MB4XDTIwMDgxMjA2MTAwM" +
            "VoXDTIyMDgxMjA2MTAwMVowgc0xCzAJBgNVBAYTAkhSMTkwNwYDVQQKDDBPUMSGQSBCT0xOSUNBIFpBQ" +
            "k9LIEkgQk9MTklDQSBIUlZBVFNLSUggVkVURVJBTkExFjAUBgNVBGETDUhSMzQ5MzgxNTg1OTkxDjAMB" +
            "gNVBAcTBVpBQk9LMQ8wDQYDVQQEEwZQQVJMQUoxEjAQBgNVBCoTCVZBTEVOVElOQTEZMBcGA1UEAxMQV" +
            "kFMRU5USU5BIFBBUkxBSjEbMBkGA1UEBRMSSFIyMDcwMzU5OTUzOS4yLjM1MIIBIjANBgkqhkiG9w0BA" +
            "QEFAAOCAQ8AMIIBCgKCAQEAtXS2gIscB%2B7pWjw9wUV8totcjBeUWPILEdzaHbjcs9eZlE2WIHPs5De" +
            "Ev3T11NfKTdcwGHX1tpR9EE5tcgbvM%2FeUPEbKT7lzKmbO1WqyI%2BbCLN8LgqhfC%2BfRLQBaAEl%2" +
            "BNLbXjjce6Jhze3RLI1amtXbP311A1DCmamIFtBrX8JQUtsmc90kWPHQeFagmdku%2FVlHdBeVrhE%2F" +
            "9jmE7Wx%2FEXQI3Y1AgNY6fDicHxqbUqoJPvIZD7X8gJ77NKCFH5qQV1wYZ%2Bdvk%2FIs2%2Fk57C9n" +
            "AgxlYKpsxp%2Fzc7eB2QL2895aoLo%2BKzSEA2X6OI%2Fa7y4MTE8G%2FCzTl6Vc38r76nXHy9wIDAQA" +
            "Bo4IDnTCCA5kwDgYDVR0PAQH%2FBAQDAgZAMIHMBgNVHSAEgcQwgcEwgbMGCSt8iFAFIAwGAjCBpTBMB" +
            "ggrBgEFBQcCARZAaHR0cHM6Ly93d3cuZmluYS5oci9yZWd1bGF0aXZhLWRva3VtZW50aS1pLXBvdHZyZ" +
            "GUtby1zdWtsYWRub3N0aTBVBggrBgEFBQcCARZJaHR0cHM6Ly93d3cuZmluYS5oci9lbi9sZWdpc2xhd" +
            "Glvbi1kb2N1bWVudHMtYW5kLWNvbmZvcm1hbmNlLWNlcnRpZmljYXRlczAJBgcEAIvsQAEAMH0GCCsGA" +
            "QUFBwEBBHEwbzAoBggrBgEFBQcwAYYcaHR0cDovL2RlbW8yMDE0LW9jc3AuZmluYS5ocjBDBggrBgEFB" +
            "QcwAoY3aHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY2VydGlmaWthdGkvZGVtbzIwMTRfc3ViX2NhLmNlc" +
            "jCBowYIKwYBBQUHAQMEgZYwgZMwCAYGBACORgEBMHIGBgQAjkYBBTBoMDIWLGh0dHBzOi8vZGVtby1wa" +
            "2kuZmluYS5oci9wZHMvUERTUUMxLTAtZW4ucGRmEwJlbjAyFixodHRwczovL2RlbW8tcGtpLmZpbmEua" +
            "HIvcGRzL1BEU1FDMS0wLWhyLnBkZhMCaHIwEwYGBACORgEGMAkGBwQAjkYBBgEwLAYDVR0RBCUwI4Ehd" +
            "mFsZW50aW5hLnBhcmxhakBib2xuaWNhLXphYm9rLmhyMIIBGAYDVR0fBIIBDzCCAQswgaaggaOggaCGK" +
            "Gh0dHA6Ly9kZW1vLXBraS5maW5hLmhyL2NybC9kZW1vMjAxNC5jcmyGdGxkYXA6Ly9kZW1vLWxkYXAuZ" +
            "mluYS5oci9jbj1GaW5hJTIwRGVtbyUyMENBJTIwMjAxNCxvPUZpbmFuY2lqc2thJTIwYWdlbmNpamEsY" +
            "z1IUj9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0JTNCYmluYXJ5MGCgXqBcpFowWDELMAkGA1UEBhMCS" +
            "FIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gQ0EgMjAxN" +
            "DEOMAwGA1UEAxMFQ1JMMjAwHwYDVR0jBBgwFoAUO4RaFPXFPOFIO13RJzV71WW8DiowHQYDVR0OBBYEF" +
            "NdrwjBEWTFv578Vd%2BLgQi1V3ZtOMAkGA1UdEwQCMAAwDQYJKoZIhvcNAQELBQADggIBAK1tLzip%2B" +
            "WwndW0QfivX312qkxDebVY7Dq8I5hDJODiVCLWju8UzsaHhOYFgtht8YndVRvgZGEzSss1O1Wv%2FX%2" +
            "FzPbi1dGC4QcBPZQmTGZw5UunIWSr0E82Es0uuj6ZwHM8qS7nmWP1Jg4zNghgKs8ryjJnzB4WgAv0fnx" +
            "QqbGC7I2dzC%2BUPRgohQ%2Bw7J3fWua3FY4Xfd4OEuf%2BUEe97wxlcWCVFVua%2FVWsmfY79R0gw48" +
            "Mgk52fC5HY2gQ0WswSL0nhm4qcWxdX2pMEf%2BH5JtvIfbL2OwIp3KirFf61hFhnqkxVogU6wN09IOBN" +
            "2p43mW2C9XX%2FoC0Ckz%2Bo5BGIUoHjp2QA3c6ag8RlPltyzzEvI4kwnL5igYPxugQfuA6u3piCyDsc" +
            "%2BZ6gKtM4C543HFANFMFvga3gvQNONoJvevyK6bslyWyIwFotTCxntfHxTr%2FR40IXxV1JiRYb1BiC" +
            "YHh5c56XTePNSShF5BfD1Qw5uH5PNuyEN69NkiE0BeWoisq8fWJoUGsaVjasa8QzegMpfhObp22H8qA7" +
            "T50G0GtXRsN6tG5Wol3zeOTu6zw1j%2F5wTR6ipXJT3hgSy9GPVhhov7fQhNplAGKdDIEnaPjijdcYzp" +
            "KyF%2FJwXfP0r6I4bWmMwACbcJVpip3k%2FC7sXTq%2BDsn9yZosHIWzxlHLy%2BY%2BM";

        private static readonly string signature = "base64Signature=iEo1%2FrYXLx4%2FZKYJE4pHA" +
            "JBdq6e0pi3gQhEsSnLnDFnM2V2upC7GNLTw2NHV0ETq0op3qFCVL4GMPmQhFlCZ%2F%2FvOh9vo1XRwl" +
            "rCZczprIv3uGsZ3YlNjZMKAZn6wm2fpecmTHycXqPTg%2FXEccVoGKg4gXsSUZmkqDRWt2QlT1ZhJNWk" +
            "VrSa%2Fb4lGtLdRd7eFV4kAr3FBP%2F7brzhqDwnI12KVmq9sYKyTvCsHckhDQ%2BQKgMPc4dcIw1VL2" +
            "ln%2FgMstuQaXktZBTLBQfCVT3TOx7EXLC4xxOW3cXWY3ZSMrSV%2F2Do9RVDJU04NRFY9UAHcEUVEhW" +
            "RQe0Z2wRSMS9aIRIw%3D%3D";

        private static readonly string chain = "base64CertificateChain=MIIFejCCA2KgAwIBAgINAN" +
            "tffOEAAAAAUygcnzANBgkqhkiG9w0BAQsFADBIMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaW" +
            "pza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBSb290IENBMB4XDTE0MDMxODA5NDUwMFoXDT" +
            "M0MDMxODEwMTUwMFowSDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMR" +
            "owGAYDVQQDExFGaW5hIERlbW8gUm9vdCBDQTCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAJ" +
            "fL3DlwWc7i4sNbDhuF1FhxptBX3JtkXil04uXgzRmfqukZ4jSCTpd9Vdc1xizes0Yxt9oxtaxIDKWLtn" +
            "Q7lp6tuyon7%2B5h5Z4%2FUZ6d3CTKps%2F2F9UgYAzG1KXYIiUrGDhvJJEHhRG3wHcaagyR4YGv0kMm" +
            "lzw%2FEmp3tNZVmHe4NEN0XCzCQp6Zn8DGofo3KuhfvEi5OkSGhR0kPSs7aEX654SQgp%2F%2Bruzmjt" +
            "ItowGbjEM%2F1Q1pD8GDe53VSpMrkRm%2FtYzwGLwkAKwGJ%2FhynrhBxv6BmENFebhE0q72%2B%2BPk" +
            "T4FsN%2F5wMPFaClrzUj6wBoToruIeIMnut5xd%2FVnkkN5wN8o4tzHTku3ODotFF8Kb%2F%2FkwmsRq" +
            "xEvwi3Dz4G70xYA8%2Beysb0LsuO1MNmrxz2oYOd2lG1iXEQeEV1GRFM9IH25w7%2FiMBOIDX46HhrfC" +
            "jqhVuWIALBodnIu9eaid4PAfHCTxOyXb6n5kE5e0K87cd9RVRZ7KglHyfTqLSbF9Jd7BqNy8bzBOc8hp" +
            "pVCbkW0C%2BucqUI6T2QsbyW81I1sOC5IpN6U3ADEWgG12w4pdgBEoJXbrIjMlM01STfGe3cp1KjJqkx" +
            "0dpPGYoObZ%2Fvw23q2O7OGuNsLJuaAS3TN4UMSgC6GxOmElotlFmchKlIE4FclrcWXOqUWkbRY5Sp0f" +
            "AgMBAAGjYzBhMA4GA1UdDwEB%2FwQEAwIBBjAPBgNVHRMBAf8EBTADAQH%2FMB8GA1UdIwQYMBaAFF9v" +
            "WznJf0Hm5pEV%2BqG2tbLnglXVMB0GA1UdDgQWBBRfb1s5yX9B5uaRFfqhtrWy54JV1TANBgkqhkiG9w" +
            "0BAQsFAAOCAgEAkiUD6hj4ZnPEhkNt2gnj4zlxed5DR%2BWOWTW3oen41rebQvbtH8lRsS2IYsLKR97T" +
            "NzFIo5Aanrk9AA468L%2F5q4lvebXeCpKLbrX5mKyT3k%2BmWzyNFR%2FuMmDT7mFOP7iBMvIt3UsXdo" +
            "ZuTLwWpGzbfieBlQHk%2F87XA2bcaKWKwMXefPqt0g96QB59b3r1CsKasWuGzEqoK9BKuMhFElC0XzK1" +
            "9HdFmV6AUt1aEqw5bRduoMjpEg37p7ZjQl6VCnMWwTjy1Ttw%2B1H9f30dDRO%2FSgjibblOKrCpayxH" +
            "nyULe2mteZAjX5OMaAEBtIYtWS%2FXt4lcqsRkA%2FerQsTFw2ED7fj7Ftbi8k7W73RTqcM%2BPuN1H6" +
            "o0XmNU7TkgH9fgPnlw9E80EFQWVoVFOm0eMVfhuc24XHYwI5aixsZj0mU4MMmbbeSW3n5CzL0uWiaOcV" +
            "CWgEoO8nkWhXnxkT2ASIxXnNThhY%2FTHPBaZDSS9T3puuqTm6jNXH%2FL8ZyEN8mR7iYKtPhLbIjo2S" +
            "UFkgNT4TYi8i7bH3YkEC013EE4SBPO%2Frl9ZPAUjn7TPlPr7Kf15AdQZmyGD9itpBVutnKj799daeP2" +
            "PeGG9J7Rz%2Bo%2BZ%2FjpkOdd6wFuP1XlTAR3h7luhzlvQT2Chrujth0zDlUR%2FqQVkKTOEark0byD" +
            "jwMgKuvuQRk%3D%3BMIIG9zCCBN%2BgAwIBAgINAOceyVkAAAAAUygd5DANBgkqhkiG9w0BAQsFADBIM" +
            "QswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgR" +
            "GVtbyBSb290IENBMB4XDTE0MDMyNTA0NDU0N1oXDTI0MDMyNTA1MTU0N1owSDELMAkGA1UEBhMCSFIxH" +
            "TAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gQ0EgMjAxNDCCA" +
            "iIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAMBi387JXbuIneYvs%2FNdvheVKb7RGrX5vuIrxaH" +
            "w1iZNGru0T0pHwr%2FLE6BcFSLCJ5yc1Pr%2BrAjcTM9tQJAubZbLYx%2BOmZOJhHfCGbYtm35qpsVwF" +
            "%2FhhCdNf5xfxLD2EdpSuM49xRy1%2F5C06qcA8o0GSyVBUUSaM6P4mI5t6zcUY1B1CW8aslv0AWIduv" +
            "wMLeRXGEfO0jikiWF%2Bt1NcWI9toF%2BO%2BUoO7V%2B9893O%2B1PSaKuid7ZIA%2FOaBXsaIIpAHM" +
            "sECIQ7ZZTpvexd%2FJSF6z62fRhBvWKUsHWCOckjp40nqtAgPqN8rVz0zzD%2BCJB44P5h%2FNgvaeP5" +
            "HdAEnrcA8p5MxLaqZ%2BMZQAkLHNKNxsHuHNwETBDM%2FOq9eDCdG0UZW6YXm2DRzTagVaDduNKvqXoo" +
            "Qej8QEycoONo5c9atFUZCTiBFS8wf6Hjp%2Fpl0UlfiHdUxayuhV2VDagQHlzMgnZFs3fww50r%2BcYE" +
            "JXasBKoOVybT1ZDLz66eKOxfsyCHJfZhtkgM38TIEtsdfjcERjexWrYviAHj5Qz1dByacZGiDiB93dIV" +
            "sX5jCnaRXZYdwbtD7EJGjkONntDO8EqIa%2BNwpH2DJyYz9FGtAR%2F5PhCusvbCu1jSJ31zpZTcX9do" +
            "Tqj9EcBNQOtwuTZEPakn0COGod9uW%2F9ah6UozKMvnJRU9AgMBAAGjggHeMIIB2jAOBgNVHQ8BAf8EB" +
            "AMCAQYwEgYDVR0TAQH%2FBAgwBgEB%2FwIBADBRBgNVHSAESjBIMEYGByt8iFAFAQEwOzA5BggrBgEFB" +
            "QcCARYtaHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY3BzL2NwZGVtb3Jvb3QxLTAucGRmMH4GCCsGAQUFB" +
            "wEBBHIwcDAoBggrBgEFBQcwAYYcaHR0cDovL2RlbW8yMDE0LW9jc3AuZmluYS5ocjBEBggrBgEFBQcwA" +
            "oY4aHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY2VydGlmaWthdGkvZGVtbzIwMTRfcm9vdF9jYS5jZXIwg" +
            "aAGA1UdHwSBmDCBlTAyoDCgLoYsaHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY3JsL0RlbW9Sb290MjAxN" +
            "C5jcmwwX6BdoFukWTBXMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExG" +
            "jAYBgNVBAMTEUZpbmEgRGVtbyBSb290IENBMQ0wCwYDVQQDEwRDUkwxMB8GA1UdIwQYMBaAFF9vWznJf" +
            "0Hm5pEV%2BqG2tbLnglXVMB0GA1UdDgQWBBQ7hFoU9cU84Ug7XdEnNXvVZbwOKjANBgkqhkiG9w0BAQs" +
            "FAAOCAgEANVRLUsyhXTHuaMYDRbyqIb2hLwm2VZzEyMgUYWNc%2B6WGhYKCcR0fADJco4%2FeAQ7rTMM" +
            "5aSR%2FiAfkdC%2FDPpIWQt3IcnvBJxBZWRzYZ2DN7jHOj1GatAY%2BbE8ELGaZ%2FTiWcmePZEazemJ" +
            "1oCMwpejqIQcnEpJ%2F51YGbMVCwLuEJ7I5%2BMVKbI6199ebSuLkxEt3wU1CUbEhScP0AICbxMEOnNY" +
            "L37AXvhP3LSPJIc0xVM9xG26qMYDdtwsRjb5sxuca6Nso8newK9t3PU2khF%2FQcfu2OY1jPLdCVO8uN" +
            "FvVCWpWb7fVPd3%2FTaM3rfaoDbFg%2FFBJGUZjEo0nwmblUDYMH5VAWcFmG%2FsOhkoSEtMyWMR7UL5" +
            "PQEeT9Uxy708tsVgYZaG%2BG4bo6ZCGrEY%2FrDXoMneVAOrHIfBmyNLmfPqJLdrln%2Bu5Nt%2Bn1y5" +
            "2nfWbeuxLHNghdFA7g%2F%2FlJI2Wpk9Uc%2Bp5g%2B7XjTT6llUMZBBFiuuFTZtdtyF33JabUyr2QMC" +
            "Y6zkA1yxx8M4aoXavVlIPOegkFsgw9ohfHJlV5xB870JVkXd3RCVn%2FYPfA9GATfY1eF0hm6cCOMn16" +
            "yq%2FSW%2FMjk0MA%2B%2BxiRkcUtyDQQZ7LZod%2FXBIC5fTde%2BOWUDNQpJ%2BKMDZpZWpDiQTZMl" +
            "3xVlAQ%2BoDGvzuzoLcNykYzj5wLFQ%3D%3BMIIIPzCCBiegAwIBAgIQVQPDYm1sN8UAAAAAXaWiWjAN" +
            "BgkqhkiG9w0BAQsFADBIMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamEx" +
            "GjAYBgNVBAMTEUZpbmEgRGVtbyBDQSAyMDE0MB4XDTIwMDgxMjA2MTAwMVoXDTIyMDgxMjA2MTAwMVow" +
            "gc0xCzAJBgNVBAYTAkhSMTkwNwYDVQQKDDBPUMSGQSBCT0xOSUNBIFpBQk9LIEkgQk9MTklDQSBIUlZB" +
            "VFNLSUggVkVURVJBTkExFjAUBgNVBGETDUhSMzQ5MzgxNTg1OTkxDjAMBgNVBAcTBVpBQk9LMQ8wDQYD" +
            "VQQEEwZQQVJMQUoxEjAQBgNVBCoTCVZBTEVOVElOQTEZMBcGA1UEAxMQVkFMRU5USU5BIFBBUkxBSjEb" +
            "MBkGA1UEBRMSSFIyMDcwMzU5OTUzOS4yLjM1MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA" +
            "tXS2gIscB%2B7pWjw9wUV8totcjBeUWPILEdzaHbjcs9eZlE2WIHPs5DeEv3T11NfKTdcwGHX1tpR9EE" +
            "5tcgbvM%2FeUPEbKT7lzKmbO1WqyI%2BbCLN8LgqhfC%2BfRLQBaAEl%2BNLbXjjce6Jhze3RLI1amtX" +
            "bP311A1DCmamIFtBrX8JQUtsmc90kWPHQeFagmdku%2FVlHdBeVrhE%2F9jmE7Wx%2FEXQI3Y1AgNY6f" +
            "DicHxqbUqoJPvIZD7X8gJ77NKCFH5qQV1wYZ%2Bdvk%2FIs2%2Fk57C9nAgxlYKpsxp%2Fzc7eB2QL28" +
            "95aoLo%2BKzSEA2X6OI%2Fa7y4MTE8G%2FCzTl6Vc38r76nXHy9wIDAQABo4IDnTCCA5kwDgYDVR0PAQ" +
            "H%2FBAQDAgZAMIHMBgNVHSAEgcQwgcEwgbMGCSt8iFAFIAwGAjCBpTBMBggrBgEFBQcCARZAaHR0cHM6" +
            "Ly93d3cuZmluYS5oci9yZWd1bGF0aXZhLWRva3VtZW50aS1pLXBvdHZyZGUtby1zdWtsYWRub3N0aTBV" +
            "BggrBgEFBQcCARZJaHR0cHM6Ly93d3cuZmluYS5oci9lbi9sZWdpc2xhdGlvbi1kb2N1bWVudHMtYW5k" +
            "LWNvbmZvcm1hbmNlLWNlcnRpZmljYXRlczAJBgcEAIvsQAEAMH0GCCsGAQUFBwEBBHEwbzAoBggrBgEF" +
            "BQcwAYYcaHR0cDovL2RlbW8yMDE0LW9jc3AuZmluYS5ocjBDBggrBgEFBQcwAoY3aHR0cDovL2RlbW8t" +
            "cGtpLmZpbmEuaHIvY2VydGlmaWthdGkvZGVtbzIwMTRfc3ViX2NhLmNlcjCBowYIKwYBBQUHAQMEgZYw" +
            "gZMwCAYGBACORgEBMHIGBgQAjkYBBTBoMDIWLGh0dHBzOi8vZGVtby1wa2kuZmluYS5oci9wZHMvUERT" +
            "UUMxLTAtZW4ucGRmEwJlbjAyFixodHRwczovL2RlbW8tcGtpLmZpbmEuaHIvcGRzL1BEU1FDMS0wLWhy" +
            "LnBkZhMCaHIwEwYGBACORgEGMAkGBwQAjkYBBgEwLAYDVR0RBCUwI4EhdmFsZW50aW5hLnBhcmxhakBi" +
            "b2xuaWNhLXphYm9rLmhyMIIBGAYDVR0fBIIBDzCCAQswgaaggaOggaCGKGh0dHA6Ly9kZW1vLXBraS5m" +
            "aW5hLmhyL2NybC9kZW1vMjAxNC5jcmyGdGxkYXA6Ly9kZW1vLWxkYXAuZmluYS5oci9jbj1GaW5hJTIw" +
            "RGVtbyUyMENBJTIwMjAxNCxvPUZpbmFuY2lqc2thJTIwYWdlbmNpamEsYz1IUj9jZXJ0aWZpY2F0ZVJl" +
            "dm9jYXRpb25MaXN0JTNCYmluYXJ5MGCgXqBcpFowWDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFu" +
            "Y2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gQ0EgMjAxNDEOMAwGA1UEAxMFQ1JMMjAw" +
            "HwYDVR0jBBgwFoAUO4RaFPXFPOFIO13RJzV71WW8DiowHQYDVR0OBBYEFNdrwjBEWTFv578Vd%2BLgQi" +
            "1V3ZtOMAkGA1UdEwQCMAAwDQYJKoZIhvcNAQELBQADggIBAK1tLzip%2BWwndW0QfivX312qkxDebVY7" +
            "Dq8I5hDJODiVCLWju8UzsaHhOYFgtht8YndVRvgZGEzSss1O1Wv%2FX%2FzPbi1dGC4QcBPZQmTGZw5U" +
            "unIWSr0E82Es0uuj6ZwHM8qS7nmWP1Jg4zNghgKs8ryjJnzB4WgAv0fnxQqbGC7I2dzC%2BUPRgohQ%2" +
            "Bw7J3fWua3FY4Xfd4OEuf%2BUEe97wxlcWCVFVua%2FVWsmfY79R0gw48Mgk52fC5HY2gQ0WswSL0nhm" +
            "4qcWxdX2pMEf%2BH5JtvIfbL2OwIp3KirFf61hFhnqkxVogU6wN09IOBN2p43mW2C9XX%2FoC0Ckz%2B" +
            "o5BGIUoHjp2QA3c6ag8RlPltyzzEvI4kwnL5igYPxugQfuA6u3piCyDsc%2BZ6gKtM4C543HFANFMFvg" +
            "a3gvQNONoJvevyK6bslyWyIwFotTCxntfHxTr%2FR40IXxV1JiRYb1BiCYHh5c56XTePNSShF5BfD1Qw" +
            "5uH5PNuyEN69NkiE0BeWoisq8fWJoUGsaVjasa8QzegMpfhObp22H8qA7T50G0GtXRsN6tG5Wol3zeOT" +
            "u6zw1j%2F5wTR6ipXJT3hgSy9GPVhhov7fQhNplAGKdDIEnaPjijdcYzpKyF%2FJwXfP0r6I4bWmMwAC" +
            "bcJVpip3k%2FC7sXTq%2BDsn9yZosHIWzxlHLy%2BY%2BM";


        static void Main(string[] args)
        {
            AttachSignature();
            SignPdf(certificate, signature);
            Console.ReadLine();
        }

        public static byte[] AttachSignature()
        {
            byte[] Hash = null;

            PdfReader reader = new PdfReader(@"C:\\Users\CSVarazdin\Desktop\Kristijan_Delač_Ugovor.PDF");
            FileStream fout = new FileStream(@"C:\\Users\CSVarazdin\Desktop\prep.pdf", FileMode.Create);

            StampingProperties sp = new StampingProperties();
            sp.UseAppendMode();

            PdfSigner pdfSigner = new PdfSigner(reader, fout, sp);
            pdfSigner.SetFieldName("Signature");

            PdfSignatureAppearance appearance = pdfSigner.GetSignatureAppearance();
            appearance.SetPageNumber(1);

            int estimatedSize = 12000;
            ExternalHashingSignatureContainer container = new ExternalHashingSignatureContainer(PdfName.Adobe_PPKLite, PdfName.Adbe_pkcs7_detached);
            pdfSigner.SignExternalContainer(container, estimatedSize);
            Hash = container.Hash;
            return Hash;
        }       

        public static void SignPdf(string certificate, string signature)
        {            
            byte[] signatureBytes = ConvertToBytes(signature);
            byte[] certificateBytes = ConvertToBytes(certificate);
            byte[] ch = ConvertToBytes(chain);


            var datasplited = chain.Split("=");
            var a = HttpUtility.UrlDecode(datasplited[1]);
            Console.WriteLine(a);

            X509Certificate x509Certificate = new X509CertificateParser().ReadCertificate(certificateBytes);

            SignerIdentifier sid = new SignerIdentifier(new IssuerAndSerialNumber(x509Certificate.IssuerDN, x509Certificate.SerialNumber));
            AlgorithmIdentifier digAlgorithm = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha256);
            Attributes authenticatedAttributes = null;
            AlgorithmIdentifier digEncryptionAlgorithm = new AlgorithmIdentifier(Org.BouncyCastle.Asn1.Pkcs.PkcsObjectIdentifiers.Sha256WithRsaEncryption);
            Asn1OctetString encryptedDigest = new DerOctetString(signatureBytes);
            Attributes unauthenticatedAttributes = null;
            SignerInfo signerInfo = new SignerInfo(sid, digAlgorithm, authenticatedAttributes, digEncryptionAlgorithm, encryptedDigest, unauthenticatedAttributes);
            Asn1EncodableVector digestAlgs = new Asn1EncodableVector();
            digestAlgs.Add(signerInfo.DigestAlgorithm);
            Asn1Set digestAlgorithms = new DerSet(digestAlgs);
            ContentInfo contentInfo = new ContentInfo(CmsObjectIdentifiers.Data, null);
            Asn1EncodableVector certs = new Asn1EncodableVector();
            certs.Add(x509Certificate.CertificateStructure.ToAsn1Object());
            Asn1Set certificates = new DerSet(certs);
            Asn1EncodableVector signerInfs = new Asn1EncodableVector();
            signerInfs.Add(signerInfo);
            Asn1Set signerInfos = new DerSet(signerInfs);
            SignedData signedData = new SignedData(digestAlgorithms, contentInfo, certificates, null, signerInfos);

            contentInfo = new ContentInfo(CmsObjectIdentifiers.SignedData, signedData);

            byte[] Signature = contentInfo.GetDerEncoded();

            using (PdfReader reader = new PdfReader(@"C:\\Users\CSVarazdin\Desktop\prep.pdf"))
            using (PdfDocument document = new PdfDocument(reader))
            using (FileStream fout = new FileStream(@"C:\\Users\CSVarazdin\Desktop\signed.pdf", FileMode.Create))
            {
                PdfSigner.SignDeferred(document, "Signature", fout, new ExternalPrecalculatedSignatureContainer(Signature));                
            }
            File.Delete(@"C:\\Users\CSVarazdin\Desktop\prep.pdf");
        }

        public static byte[] ConvertToBytes(string data)
        {           
            var datasplited = data.Split("=");           
            var base64string = HttpUtility.UrlDecode(datasplited[1]);
            base64string = base64string.Replace("=;", "");

            return Convert.FromBase64String(base64string);
        }

        private class ExternalPrecalculatedSignatureContainer : ExternalBlankSignatureContainer
        {
            public ExternalPrecalculatedSignatureContainer(byte[] cms) : base(new PdfDictionary())
            {
                Cms = cms;
            }

            public override byte[] Sign(Stream data)
            {
                return Cms;
            }

            public byte[] Cms { get; private set; }
        }
    }
}
