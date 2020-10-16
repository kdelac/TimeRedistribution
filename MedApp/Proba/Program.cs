using iText.Kernel.Geom;
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
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using cer = System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using System.Runtime.InteropServices.ComTypes;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Proba
{
    class Program
    {
        private static readonly string certificate = "base64Certificate=MIIICzCCBfOgAwIBAgIRAPOHXo1LAwpEAAAAAF2lsacwDQYJKoZIhvcNAQELBQAwSDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gQ0EgMjAxNDAeFw0yMDEwMTMwNjMzMDVaFw0yMjEwMTMwNjMzMDVaMIGsMQswCQYDVQQGEwJIUjEbMBkGA1UEChMSQVZJT04gTUVESUEgRC5PLk8uMRYwFAYDVQRhEw1IUjc3MTgyMDI0NjU2MQ8wDQYDVQQHEwZaQUdSRUIxEDAOBgNVBAQMB1pSSUxJxIYxDzANBgNVBCoTBk1BVEVKQTEXMBUGA1UEAwwOTUFURUpBIFpSSUxJxIYxGzAZBgNVBAUTEkhSODU5ODc2OTM4NDMuMS4zNTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAJ8NXK9K3taWQWOCvLoYE3uzm5Ml1VOMW5sZjtTbNWzGPscwCvfcS7tUrjap5O9Kv7yiO0jAtXFnWJHexuGPUwKwh%2B%2BgkcBjcY0DTn9BLStgdaGXBkE%2BUUP0s%2Bg9MDiUco3Y%2BKHT%2BmnCSalieSlb9%2BP4sTA3VBAJCPJ4wtEWhmqxImSM3nzV%2Fasp6uk7djT%2B9QeD1IVjRfbgEj2UAI70CGO4k0WZVsoLRxyoRFnJKbBl6gCUXLNjGGTwqaUB9JFTSpkQ0N1mhZmNJUguD4o8OJXoeDkgJHhqOnjXp6T1yMb41oMWqu%2F%2BCF5FF%2F%2BoiWZDOLp53QmMByMFQTEe1EIqaMkCAwEAAaOCA4kwggOFMA4GA1UdDwEB%2FwQEAwIGQDCBzAYDVR0gBIHEMIHBMIGzBgkrfIhQBSAMBgIwgaUwTAYIKwYBBQUHAgEWQGh0dHBzOi8vd3d3LmZpbmEuaHIvcmVndWxhdGl2YS1kb2t1bWVudGktaS1wb3R2cmRlLW8tc3VrbGFkbm9zdGkwVQYIKwYBBQUHAgEWSWh0dHBzOi8vd3d3LmZpbmEuaHIvZW4vbGVnaXNsYXRpb24tZG9jdW1lbnRzLWFuZC1jb25mb3JtYW5jZS1jZXJ0aWZpY2F0ZXMwCQYHBACL7EABADB9BggrBgEFBQcBAQRxMG8wKAYIKwYBBQUHMAGGHGh0dHA6Ly9kZW1vMjAxNC1vY3NwLmZpbmEuaHIwQwYIKwYBBQUHMAKGN2h0dHA6Ly9kZW1vLXBraS5maW5hLmhyL2NlcnRpZmlrYXRpL2RlbW8yMDE0X3N1Yl9jYS5jZXIwgaMGCCsGAQUFBwEDBIGWMIGTMAgGBgQAjkYBATByBgYEAI5GAQUwaDAyFixodHRwczovL2RlbW8tcGtpLmZpbmEuaHIvcGRzL1BEU1FDMS0wLWVuLnBkZhMCZW4wMhYsaHR0cHM6Ly9kZW1vLXBraS5maW5hLmhyL3Bkcy9QRFNRQzEtMC1oci5wZGYTAmhyMBMGBgQAjkYBBjAJBgcEAI5GAQYBMBgGA1UdEQQRMA%2BBDW16cmlsaWNAY3MuaHIwggEYBgNVHR8EggEPMIIBCzCBpqCBo6CBoIYoaHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY3JsL2RlbW8yMDE0LmNybIZ0bGRhcDovL2RlbW8tbGRhcC5maW5hLmhyL2NuPUZpbmElMjBEZW1vJTIwQ0ElMjAyMDE0LG89RmluYW5jaWpza2ElMjBhZ2VuY2lqYSxjPUhSP2NlcnRpZmljYXRlUmV2b2NhdGlvbkxpc3QlM0JiaW5hcnkwYKBeoFykWjBYMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBDQSAyMDE0MQ4wDAYDVQQDEwVDUkwyMjAfBgNVHSMEGDAWgBQ7hFoU9cU84Ug7XdEnNXvVZbwOKjAdBgNVHQ4EFgQU8Zzmxt1qOZccPbkjdE8%2F9X%2Bc0MMwCQYDVR0TBAIwADANBgkqhkiG9w0BAQsFAAOCAgEABvRIhDdfahCD4QIhNe%2F2yrOtnBbU19K5zZ9FQYfaTJdOBp8mInzyzDqGjLrv9wWvaFq4DE%2FxRjLQV3EHD3MYWZvi2eyJ9%2Faw7Q%2FhFqLi9kDBm94%2B%2Bx28BlNY%2FtmH%2BEUKzpvGntUhywFPPr3IGGQE2LLdb1Zw6Ur9mAcxq7mfRqJBt3goXxqurWT0fTCaUJ95Fgrc17rdgcME99%2Fm3CEemts62gvRn7PTRO2XRGZlWA%2F5gt%2FPTtX%2BaFUszLDq%2FpWbYgdAxtdIUrrmAkUkfpUM9dXS1YoVYNYItuulAbMCIRaBILsMdu6AKsS4MDz5x4Bqpo97KsWjwuFyhI3NSQ02%2FFKWlOjf6UVoOOJ3JYPFO92S2NsjxNZ9r%2FMw6EJlJwNkb9cGVmVyy2ZA8UcyIe3GqlPy55UcAAkxiy6qz9fC2OR9aBArkvqBvsrDMHjr3oILJoYTxOJS9MKZ5mmJ0IFUZBK9FHJTYY%2Bfo3AV%2Fy0p38LaHxqWY9wnZ%2FT%2BEOwOCa9rwo5Jd8rpHtuY9WqYoscvRvkpzZJDK4xXDGnXibyAn0SBESd7OxGZwiX2dLaQWcU4rzHkQWcTRb3e37NORi4gRl4%2BNjos70HXcXH%2BQHQFsXuYyltEjMIo6TWzh5%2B9etrw17CHBuno2u678EsRG1Xr1VRCt8SpOdakWYJ%2BiH%2BlodE%3D";
        private static readonly string signature = "base64Signature=IlExMcPFivUfJDBkYTbA3G9X2f8vKv9nkaynZFLIJR9RbufQfLtZzARKhZ3Dr%2F9wI3hkaT5YgyfIiRLikiabV1HFojiK2ESqfz1gWV5b%2Fjz8We78kzx3arpk1LZb%2BjLv4EvjIvPcM9%2FQN7hQdeM3owdCWdSYe%2F4zLG4Qhe39%2FvZQlipv%2BE3F%2FOMhsAenwcGPi1k9SQ5ChtTlHJq5qv2vg8gcuFxgM1s9oebzk6HYRdxDaqVx%2BLjsOS%2BgpzBop5OcplX%2FJLW2Ta3DOXMQlgC4k6mkBAo%2BOrhDQvSdkq6H4qfjsxMoS2lsDoN%2BtQQjHXRCgdOcMQg0bPA%2BtG9qFmIixQ%3D%3D";
        private static readonly string chain = "base64CertificateChain=MIIFejCCA2KgAwIBAgINANtffOEAAAAAUygcnzANBgkqhkiG9w0BAQsFADBIMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBSb290IENBMB4XDTE0MDMxODA5NDUwMFoXDTM0MDMxODEwMTUwMFowSDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gUm9vdCBDQTCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAJfL3DlwWc7i4sNbDhuF1FhxptBX3JtkXil04uXgzRmfqukZ4jSCTpd9Vdc1xizes0Yxt9oxtaxIDKWLtnQ7lp6tuyon7%2B5h5Z4%2FUZ6d3CTKps%2F2F9UgYAzG1KXYIiUrGDhvJJEHhRG3wHcaagyR4YGv0kMmlzw%2FEmp3tNZVmHe4NEN0XCzCQp6Zn8DGofo3KuhfvEi5OkSGhR0kPSs7aEX654SQgp%2F%2BruzmjtItowGbjEM%2F1Q1pD8GDe53VSpMrkRm%2FtYzwGLwkAKwGJ%2FhynrhBxv6BmENFebhE0q72%2B%2BPkT4FsN%2F5wMPFaClrzUj6wBoToruIeIMnut5xd%2FVnkkN5wN8o4tzHTku3ODotFF8Kb%2F%2FkwmsRqxEvwi3Dz4G70xYA8%2Beysb0LsuO1MNmrxz2oYOd2lG1iXEQeEV1GRFM9IH25w7%2FiMBOIDX46HhrfCjqhVuWIALBodnIu9eaid4PAfHCTxOyXb6n5kE5e0K87cd9RVRZ7KglHyfTqLSbF9Jd7BqNy8bzBOc8hppVCbkW0C%2BucqUI6T2QsbyW81I1sOC5IpN6U3ADEWgG12w4pdgBEoJXbrIjMlM01STfGe3cp1KjJqkx0dpPGYoObZ%2Fvw23q2O7OGuNsLJuaAS3TN4UMSgC6GxOmElotlFmchKlIE4FclrcWXOqUWkbRY5Sp0fAgMBAAGjYzBhMA4GA1UdDwEB%2FwQEAwIBBjAPBgNVHRMBAf8EBTADAQH%2FMB8GA1UdIwQYMBaAFF9vWznJf0Hm5pEV%2BqG2tbLnglXVMB0GA1UdDgQWBBRfb1s5yX9B5uaRFfqhtrWy54JV1TANBgkqhkiG9w0BAQsFAAOCAgEAkiUD6hj4ZnPEhkNt2gnj4zlxed5DR%2BWOWTW3oen41rebQvbtH8lRsS2IYsLKR97TNzFIo5Aanrk9AA468L%2F5q4lvebXeCpKLbrX5mKyT3k%2BmWzyNFR%2FuMmDT7mFOP7iBMvIt3UsXdoZuTLwWpGzbfieBlQHk%2F87XA2bcaKWKwMXefPqt0g96QB59b3r1CsKasWuGzEqoK9BKuMhFElC0XzK19HdFmV6AUt1aEqw5bRduoMjpEg37p7ZjQl6VCnMWwTjy1Ttw%2B1H9f30dDRO%2FSgjibblOKrCpayxHnyULe2mteZAjX5OMaAEBtIYtWS%2FXt4lcqsRkA%2FerQsTFw2ED7fj7Ftbi8k7W73RTqcM%2BPuN1H6o0XmNU7TkgH9fgPnlw9E80EFQWVoVFOm0eMVfhuc24XHYwI5aixsZj0mU4MMmbbeSW3n5CzL0uWiaOcVCWgEoO8nkWhXnxkT2ASIxXnNThhY%2FTHPBaZDSS9T3puuqTm6jNXH%2FL8ZyEN8mR7iYKtPhLbIjo2SUFkgNT4TYi8i7bH3YkEC013EE4SBPO%2Frl9ZPAUjn7TPlPr7Kf15AdQZmyGD9itpBVutnKj799daeP2PeGG9J7Rz%2Bo%2BZ%2FjpkOdd6wFuP1XlTAR3h7luhzlvQT2Chrujth0zDlUR%2FqQVkKTOEark0byDjwMgKuvuQRk%3D%3BMIIG9zCCBN%2BgAwIBAgINAOceyVkAAAAAUygd5DANBgkqhkiG9w0BAQsFADBIMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBSb290IENBMB4XDTE0MDMyNTA0NDU0N1oXDTI0MDMyNTA1MTU0N1owSDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gQ0EgMjAxNDCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAMBi387JXbuIneYvs%2FNdvheVKb7RGrX5vuIrxaHw1iZNGru0T0pHwr%2FLE6BcFSLCJ5yc1Pr%2BrAjcTM9tQJAubZbLYx%2BOmZOJhHfCGbYtm35qpsVwF%2FhhCdNf5xfxLD2EdpSuM49xRy1%2F5C06qcA8o0GSyVBUUSaM6P4mI5t6zcUY1B1CW8aslv0AWIduvwMLeRXGEfO0jikiWF%2Bt1NcWI9toF%2BO%2BUoO7V%2B9893O%2B1PSaKuid7ZIA%2FOaBXsaIIpAHMsECIQ7ZZTpvexd%2FJSF6z62fRhBvWKUsHWCOckjp40nqtAgPqN8rVz0zzD%2BCJB44P5h%2FNgvaeP5HdAEnrcA8p5MxLaqZ%2BMZQAkLHNKNxsHuHNwETBDM%2FOq9eDCdG0UZW6YXm2DRzTagVaDduNKvqXooQej8QEycoONo5c9atFUZCTiBFS8wf6Hjp%2Fpl0UlfiHdUxayuhV2VDagQHlzMgnZFs3fww50r%2BcYEJXasBKoOVybT1ZDLz66eKOxfsyCHJfZhtkgM38TIEtsdfjcERjexWrYviAHj5Qz1dByacZGiDiB93dIVsX5jCnaRXZYdwbtD7EJGjkONntDO8EqIa%2BNwpH2DJyYz9FGtAR%2F5PhCusvbCu1jSJ31zpZTcX9doTqj9EcBNQOtwuTZEPakn0COGod9uW%2F9ah6UozKMvnJRU9AgMBAAGjggHeMIIB2jAOBgNVHQ8BAf8EBAMCAQYwEgYDVR0TAQH%2FBAgwBgEB%2FwIBADBRBgNVHSAESjBIMEYGByt8iFAFAQEwOzA5BggrBgEFBQcCARYtaHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY3BzL2NwZGVtb3Jvb3QxLTAucGRmMH4GCCsGAQUFBwEBBHIwcDAoBggrBgEFBQcwAYYcaHR0cDovL2RlbW8yMDE0LW9jc3AuZmluYS5ocjBEBggrBgEFBQcwAoY4aHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY2VydGlmaWthdGkvZGVtbzIwMTRfcm9vdF9jYS5jZXIwgaAGA1UdHwSBmDCBlTAyoDCgLoYsaHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY3JsL0RlbW9Sb290MjAxNC5jcmwwX6BdoFukWTBXMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBSb290IENBMQ0wCwYDVQQDEwRDUkwxMB8GA1UdIwQYMBaAFF9vWznJf0Hm5pEV%2BqG2tbLnglXVMB0GA1UdDgQWBBQ7hFoU9cU84Ug7XdEnNXvVZbwOKjANBgkqhkiG9w0BAQsFAAOCAgEANVRLUsyhXTHuaMYDRbyqIb2hLwm2VZzEyMgUYWNc%2B6WGhYKCcR0fADJco4%2FeAQ7rTMM5aSR%2FiAfkdC%2FDPpIWQt3IcnvBJxBZWRzYZ2DN7jHOj1GatAY%2BbE8ELGaZ%2FTiWcmePZEazemJ1oCMwpejqIQcnEpJ%2F51YGbMVCwLuEJ7I5%2BMVKbI6199ebSuLkxEt3wU1CUbEhScP0AICbxMEOnNYL37AXvhP3LSPJIc0xVM9xG26qMYDdtwsRjb5sxuca6Nso8newK9t3PU2khF%2FQcfu2OY1jPLdCVO8uNFvVCWpWb7fVPd3%2FTaM3rfaoDbFg%2FFBJGUZjEo0nwmblUDYMH5VAWcFmG%2FsOhkoSEtMyWMR7UL5PQEeT9Uxy708tsVgYZaG%2BG4bo6ZCGrEY%2FrDXoMneVAOrHIfBmyNLmfPqJLdrln%2Bu5Nt%2Bn1y52nfWbeuxLHNghdFA7g%2F%2FlJI2Wpk9Uc%2Bp5g%2B7XjTT6llUMZBBFiuuFTZtdtyF33JabUyr2QMCY6zkA1yxx8M4aoXavVlIPOegkFsgw9ohfHJlV5xB870JVkXd3RCVn%2FYPfA9GATfY1eF0hm6cCOMn16yq%2FSW%2FMjk0MA%2B%2BxiRkcUtyDQQZ7LZod%2FXBIC5fTde%2BOWUDNQpJ%2BKMDZpZWpDiQTZMl3xVlAQ%2BoDGvzuzoLcNykYzj5wLFQ%3D%3BMIIICzCCBfOgAwIBAgIRAPOHXo1LAwpEAAAAAF2lsacwDQYJKoZIhvcNAQELBQAwSDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gQ0EgMjAxNDAeFw0yMDEwMTMwNjMzMDVaFw0yMjEwMTMwNjMzMDVaMIGsMQswCQYDVQQGEwJIUjEbMBkGA1UEChMSQVZJT04gTUVESUEgRC5PLk8uMRYwFAYDVQRhEw1IUjc3MTgyMDI0NjU2MQ8wDQYDVQQHEwZaQUdSRUIxEDAOBgNVBAQMB1pSSUxJxIYxDzANBgNVBCoTBk1BVEVKQTEXMBUGA1UEAwwOTUFURUpBIFpSSUxJxIYxGzAZBgNVBAUTEkhSODU5ODc2OTM4NDMuMS4zNTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAJ8NXK9K3taWQWOCvLoYE3uzm5Ml1VOMW5sZjtTbNWzGPscwCvfcS7tUrjap5O9Kv7yiO0jAtXFnWJHexuGPUwKwh%2B%2BgkcBjcY0DTn9BLStgdaGXBkE%2BUUP0s%2Bg9MDiUco3Y%2BKHT%2BmnCSalieSlb9%2BP4sTA3VBAJCPJ4wtEWhmqxImSM3nzV%2Fasp6uk7djT%2B9QeD1IVjRfbgEj2UAI70CGO4k0WZVsoLRxyoRFnJKbBl6gCUXLNjGGTwqaUB9JFTSpkQ0N1mhZmNJUguD4o8OJXoeDkgJHhqOnjXp6T1yMb41oMWqu%2F%2BCF5FF%2F%2BoiWZDOLp53QmMByMFQTEe1EIqaMkCAwEAAaOCA4kwggOFMA4GA1UdDwEB%2FwQEAwIGQDCBzAYDVR0gBIHEMIHBMIGzBgkrfIhQBSAMBgIwgaUwTAYIKwYBBQUHAgEWQGh0dHBzOi8vd3d3LmZpbmEuaHIvcmVndWxhdGl2YS1kb2t1bWVudGktaS1wb3R2cmRlLW8tc3VrbGFkbm9zdGkwVQYIKwYBBQUHAgEWSWh0dHBzOi8vd3d3LmZpbmEuaHIvZW4vbGVnaXNsYXRpb24tZG9jdW1lbnRzLWFuZC1jb25mb3JtYW5jZS1jZXJ0aWZpY2F0ZXMwCQYHBACL7EABADB9BggrBgEFBQcBAQRxMG8wKAYIKwYBBQUHMAGGHGh0dHA6Ly9kZW1vMjAxNC1vY3NwLmZpbmEuaHIwQwYIKwYBBQUHMAKGN2h0dHA6Ly9kZW1vLXBraS5maW5hLmhyL2NlcnRpZmlrYXRpL2RlbW8yMDE0X3N1Yl9jYS5jZXIwgaMGCCsGAQUFBwEDBIGWMIGTMAgGBgQAjkYBATByBgYEAI5GAQUwaDAyFixodHRwczovL2RlbW8tcGtpLmZpbmEuaHIvcGRzL1BEU1FDMS0wLWVuLnBkZhMCZW4wMhYsaHR0cHM6Ly9kZW1vLXBraS5maW5hLmhyL3Bkcy9QRFNRQzEtMC1oci5wZGYTAmhyMBMGBgQAjkYBBjAJBgcEAI5GAQYBMBgGA1UdEQQRMA%2BBDW16cmlsaWNAY3MuaHIwggEYBgNVHR8EggEPMIIBCzCBpqCBo6CBoIYoaHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY3JsL2RlbW8yMDE0LmNybIZ0bGRhcDovL2RlbW8tbGRhcC5maW5hLmhyL2NuPUZpbmElMjBEZW1vJTIwQ0ElMjAyMDE0LG89RmluYW5jaWpza2ElMjBhZ2VuY2lqYSxjPUhSP2NlcnRpZmljYXRlUmV2b2NhdGlvbkxpc3QlM0JiaW5hcnkwYKBeoFykWjBYMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBDQSAyMDE0MQ4wDAYDVQQDEwVDUkwyMjAfBgNVHSMEGDAWgBQ7hFoU9cU84Ug7XdEnNXvVZbwOKjAdBgNVHQ4EFgQU8Zzmxt1qOZccPbkjdE8%2F9X%2Bc0MMwCQYDVR0TBAIwADANBgkqhkiG9w0BAQsFAAOCAgEABvRIhDdfahCD4QIhNe%2F2yrOtnBbU19K5zZ9FQYfaTJdOBp8mInzyzDqGjLrv9wWvaFq4DE%2FxRjLQV3EHD3MYWZvi2eyJ9%2Faw7Q%2FhFqLi9kDBm94%2B%2Bx28BlNY%2FtmH%2BEUKzpvGntUhywFPPr3IGGQE2LLdb1Zw6Ur9mAcxq7mfRqJBt3goXxqurWT0fTCaUJ95Fgrc17rdgcME99%2Fm3CEemts62gvRn7PTRO2XRGZlWA%2F5gt%2FPTtX%2BaFUszLDq%2FpWbYgdAxtdIUrrmAkUkfpUM9dXS1YoVYNYItuulAbMCIRaBILsMdu6AKsS4MDz5x4Bqpo97KsWjwuFyhI3NSQ02%2FFKWlOjf6UVoOOJ3JYPFO92S2NsjxNZ9r%2FMw6EJlJwNkb9cGVmVyy2ZA8UcyIe3GqlPy55UcAAkxiy6qz9fC2OR9aBArkvqBvsrDMHjr3oILJoYTxOJS9MKZ5mmJ0IFUZBK9FHJTYY%2Bfo3AV%2Fy0p38LaHxqWY9wnZ%2FT%2BEOwOCa9rwo5Jd8rpHtuY9WqYoscvRvkpzZJDK4xXDGnXibyAn0SBESd7OxGZwiX2dLaQWcU4rzHkQWcTRb3e37NORi4gRl4%2BNjos70HXcXH%2BQHQFsXuYyltEjMIo6TWzh5%2B9etrw17CHBuno2u678EsRG1Xr1VRCt8SpOdakWYJ%2BiH%2BlodE%3D";

        private static readonly string temp = @"C:\\Users\CSVarazdin\Desktop\prep2.pdf";
        private static readonly string src = @"C:\\Users\CSVarazdin\Desktop\dummy.PDF";
        private static readonly string dest = @"C:\\Users\CSVarazdin\Desktop\signed.pdf";
        private static byte[] extSignature;

        static void Main(string[] args)
        {
            //Otkomentirati prilikom stvaranja sazetka

            //var a = AttachSignature();   
            //Console.WriteLine(Convert.ToBase64String(a));   

            //CreateSignature(temp, dest, "Signature", null, GetChains());

            
            
            //Otkomentirati prilikom potpisivanja, nakon što se potpis stavi
            //SignPdf(certificate, signature);
            //Console.ReadLine();
        }

        public static X509Certificate[] GetChains()
        {
            var b = chain.Split("=");
            var e = HttpUtility.UrlDecode(b[1]);
            var c = e.Split(";");
            X509Certificate[] chains = new X509Certificate[c.Length];
            for (int i = 0; i < c.Length; i++)
            {
                X509CertificateParser parser = new X509CertificateParser();
                var aaa = parser.ReadCertificate(Convert.FromBase64String(c[i]));
                chains[i] = aaa;
            }

            return chains;
        }

        public static byte[] AttachSignature()
        {
            byte[] Hash = null;

            PdfReader reader = new PdfReader(src);
            FileStream fout = new FileStream(temp, FileMode.Create);

            StampingProperties sp = new StampingProperties();
            sp.UseAppendMode();

            PdfSigner pdfSigner = new PdfSigner(reader, fout, sp);
            pdfSigner.SetFieldName("Signature");

            PdfSignatureAppearance appearance = pdfSigner.GetSignatureAppearance();
            appearance.SetPageNumber(1);
            appearance.SetPageRect(new Rectangle(100, 100));
            appearance.SetLocation("Varazdin");

            int estimatedSize = 8192;
            ExternalHashingSignatureContainer container = new ExternalHashingSignatureContainer(PdfName.Adobe_PPKLite, PdfName.Adbe_pkcs7_detached);

            IExternalSignatureContainer external = new ExternalBlankSignatureContainer(PdfName.Adobe_PPKLite,
                PdfName.Adbe_pkcs7_detached);
            pdfSigner.SignExternalContainer(container, estimatedSize);
            Hash = container.Hash;
            extSignature = Hash;
            return Hash;
        }

        public static void SignPdf(string certificate, string signature)
        {
            byte[] signatureBytes = ConvertToBytes(signature);
            byte[] certificateBytes = ConvertToBytes(certificate);

            var acsd = Encoding.Default.GetString(signatureBytes);

            var chains = GetChains();

            var datasplited = chain.Split("=");
            var a = HttpUtility.UrlDecode(datasplited[1]);
            var b = a.Split(";");
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
            for (int i = 0; i < chains.Length; i++)
            {
                certs.Add(chains[i].CertificateStructure.ToAsn1Object());
            }
            Asn1Set certificates = new DerSet(certs);
            Asn1EncodableVector signerInfs = new Asn1EncodableVector();
            signerInfs.Add(signerInfo);
            Asn1Set signerInfos = new DerSet(signerInfs);
            SignedData signedData = new SignedData(digestAlgorithms, contentInfo, certificates, null, signerInfos);           

            contentInfo = new ContentInfo(CmsObjectIdentifiers.SignedData, signedData);

            byte[] Signature = contentInfo.GetDerEncoded();

            using (PdfReader reader = new PdfReader(temp))
            using (PdfDocument document = new PdfDocument(reader))
            using (FileStream fout = new FileStream(dest, FileMode.Create))
            {
                PdfSigner.SignDeferred(document, "Signature", fout, new ExternalPrecalculatedSignatureContainer(Signature));
            }
        }

        public static byte[] ConvertToBytes(string data)
        {
            var datasplited = data.Split("=");
            if (datasplited.Count() > 1)
            {
                var base64string1 = HttpUtility.UrlDecode(datasplited[1]);
                return Convert.FromBase64String(base64string1);
            }
            var base64string = HttpUtility.UrlDecode(data);
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