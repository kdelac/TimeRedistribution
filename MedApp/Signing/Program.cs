using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.X509;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using System.Web;

namespace Signing
{
    class Program
    {
        private static readonly string temp = @"C:\\Users\CSVarazdin\Desktop\prep2.pdf";
        private static readonly string src = @"C:\\Users\CSVarazdin\Desktop\dummy.PDF";
        private static readonly string dest = @"C:\\Users\CSVarazdin\Desktop\signed.pdf";

        /// <summary>
        /// Data recived from web service after sending base64 digested pdf document
        /// </summary>
        private static string dataToBeSigned = "base64DataToBeSigned=iWQA%2BRiYauuY6SRw4d0%2Fno6SYllT9FK8jIHV45zUAGo%3D";
        private static string certificate = "base64Certificate=MIIICzCCBfOgAwIBAgIRAPOHXo1LAwpEAAAAAF2lsacwDQYJKoZIhvcNAQELBQAwSDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gQ0EgMjAxNDAeFw0yMDEwMTMwNjMzMDVaFw0yMjEwMTMwNjMzMDVaMIGsMQswCQYDVQQGEwJIUjEbMBkGA1UEChMSQVZJT04gTUVESUEgRC5PLk8uMRYwFAYDVQRhEw1IUjc3MTgyMDI0NjU2MQ8wDQYDVQQHEwZaQUdSRUIxEDAOBgNVBAQMB1pSSUxJxIYxDzANBgNVBCoTBk1BVEVKQTEXMBUGA1UEAwwOTUFURUpBIFpSSUxJxIYxGzAZBgNVBAUTEkhSODU5ODc2OTM4NDMuMS4zNTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAJ8NXK9K3taWQWOCvLoYE3uzm5Ml1VOMW5sZjtTbNWzGPscwCvfcS7tUrjap5O9Kv7yiO0jAtXFnWJHexuGPUwKwh%2B%2BgkcBjcY0DTn9BLStgdaGXBkE%2BUUP0s%2Bg9MDiUco3Y%2BKHT%2BmnCSalieSlb9%2BP4sTA3VBAJCPJ4wtEWhmqxImSM3nzV%2Fasp6uk7djT%2B9QeD1IVjRfbgEj2UAI70CGO4k0WZVsoLRxyoRFnJKbBl6gCUXLNjGGTwqaUB9JFTSpkQ0N1mhZmNJUguD4o8OJXoeDkgJHhqOnjXp6T1yMb41oMWqu%2F%2BCF5FF%2F%2BoiWZDOLp53QmMByMFQTEe1EIqaMkCAwEAAaOCA4kwggOFMA4GA1UdDwEB%2FwQEAwIGQDCBzAYDVR0gBIHEMIHBMIGzBgkrfIhQBSAMBgIwgaUwTAYIKwYBBQUHAgEWQGh0dHBzOi8vd3d3LmZpbmEuaHIvcmVndWxhdGl2YS1kb2t1bWVudGktaS1wb3R2cmRlLW8tc3VrbGFkbm9zdGkwVQYIKwYBBQUHAgEWSWh0dHBzOi8vd3d3LmZpbmEuaHIvZW4vbGVnaXNsYXRpb24tZG9jdW1lbnRzLWFuZC1jb25mb3JtYW5jZS1jZXJ0aWZpY2F0ZXMwCQYHBACL7EABADB9BggrBgEFBQcBAQRxMG8wKAYIKwYBBQUHMAGGHGh0dHA6Ly9kZW1vMjAxNC1vY3NwLmZpbmEuaHIwQwYIKwYBBQUHMAKGN2h0dHA6Ly9kZW1vLXBraS5maW5hLmhyL2NlcnRpZmlrYXRpL2RlbW8yMDE0X3N1Yl9jYS5jZXIwgaMGCCsGAQUFBwEDBIGWMIGTMAgGBgQAjkYBATByBgYEAI5GAQUwaDAyFixodHRwczovL2RlbW8tcGtpLmZpbmEuaHIvcGRzL1BEU1FDMS0wLWVuLnBkZhMCZW4wMhYsaHR0cHM6Ly9kZW1vLXBraS5maW5hLmhyL3Bkcy9QRFNRQzEtMC1oci5wZGYTAmhyMBMGBgQAjkYBBjAJBgcEAI5GAQYBMBgGA1UdEQQRMA%2BBDW16cmlsaWNAY3MuaHIwggEYBgNVHR8EggEPMIIBCzCBpqCBo6CBoIYoaHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY3JsL2RlbW8yMDE0LmNybIZ0bGRhcDovL2RlbW8tbGRhcC5maW5hLmhyL2NuPUZpbmElMjBEZW1vJTIwQ0ElMjAyMDE0LG89RmluYW5jaWpza2ElMjBhZ2VuY2lqYSxjPUhSP2NlcnRpZmljYXRlUmV2b2NhdGlvbkxpc3QlM0JiaW5hcnkwYKBeoFykWjBYMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBDQSAyMDE0MQ4wDAYDVQQDEwVDUkwyMjAfBgNVHSMEGDAWgBQ7hFoU9cU84Ug7XdEnNXvVZbwOKjAdBgNVHQ4EFgQU8Zzmxt1qOZccPbkjdE8%2F9X%2Bc0MMwCQYDVR0TBAIwADANBgkqhkiG9w0BAQsFAAOCAgEABvRIhDdfahCD4QIhNe%2F2yrOtnBbU19K5zZ9FQYfaTJdOBp8mInzyzDqGjLrv9wWvaFq4DE%2FxRjLQV3EHD3MYWZvi2eyJ9%2Faw7Q%2FhFqLi9kDBm94%2B%2Bx28BlNY%2FtmH%2BEUKzpvGntUhywFPPr3IGGQE2LLdb1Zw6Ur9mAcxq7mfRqJBt3goXxqurWT0fTCaUJ95Fgrc17rdgcME99%2Fm3CEemts62gvRn7PTRO2XRGZlWA%2F5gt%2FPTtX%2BaFUszLDq%2FpWbYgdAxtdIUrrmAkUkfpUM9dXS1YoVYNYItuulAbMCIRaBILsMdu6AKsS4MDz5x4Bqpo97KsWjwuFyhI3NSQ02%2FFKWlOjf6UVoOOJ3JYPFO92S2NsjxNZ9r%2FMw6EJlJwNkb9cGVmVyy2ZA8UcyIe3GqlPy55UcAAkxiy6qz9fC2OR9aBArkvqBvsrDMHjr3oILJoYTxOJS9MKZ5mmJ0IFUZBK9FHJTYY%2Bfo3AV%2Fy0p38LaHxqWY9wnZ%2FT%2BEOwOCa9rwo5Jd8rpHtuY9WqYoscvRvkpzZJDK4xXDGnXibyAn0SBESd7OxGZwiX2dLaQWcU4rzHkQWcTRb3e37NORi4gRl4%2BNjos70HXcXH%2BQHQFsXuYyltEjMIo6TWzh5%2B9etrw17CHBuno2u678EsRG1Xr1VRCt8SpOdakWYJ%2BiH%2BlodE%3D";
        private static string signature = "base64Signature=T0Am92hjg5Q%2BCrMdrq8hQEiJ80UUyBWyAeo0JQoLGSq0LdK%2FZhZB2Mk9feCiohiDpA5Qyp2TCoa2FWbk6jK48ajAHKxmEc5wO4Niv2DlaWh8fv2lBj%2BOxpVrUDm7Y26o4ITQzC8VECLNHE6jQL7O6VCmCdhaOrSpnvHaCZctrFfrxWlTT4d6tQBtR%2FZnp%2Feam4PxQtoUxYkan8FYxxMacfOm3hEPdUJ2Wp2chiWDou4bn%2BMc6JERLqErmZcNrzASifiuvMn5padc%2FqNN71tJYCi1XlAgzrWgZqShgRTNxXD8CBbJD%2B2vCGkLJlrEuiCqyvMeFNc911zw%2FLn1P8ZCDw%3D%3D";  
        private static string chain = "base64CertificateChain=MIIFejCCA2KgAwIBAgINANtffOEAAAAAUygcnzANBgkqhkiG9w0BAQsFADBIMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBSb290IENBMB4XDTE0MDMxODA5NDUwMFoXDTM0MDMxODEwMTUwMFowSDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gUm9vdCBDQTCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAJfL3DlwWc7i4sNbDhuF1FhxptBX3JtkXil04uXgzRmfqukZ4jSCTpd9Vdc1xizes0Yxt9oxtaxIDKWLtnQ7lp6tuyon7%2B5h5Z4%2FUZ6d3CTKps%2F2F9UgYAzG1KXYIiUrGDhvJJEHhRG3wHcaagyR4YGv0kMmlzw%2FEmp3tNZVmHe4NEN0XCzCQp6Zn8DGofo3KuhfvEi5OkSGhR0kPSs7aEX654SQgp%2F%2BruzmjtItowGbjEM%2F1Q1pD8GDe53VSpMrkRm%2FtYzwGLwkAKwGJ%2FhynrhBxv6BmENFebhE0q72%2B%2BPkT4FsN%2F5wMPFaClrzUj6wBoToruIeIMnut5xd%2FVnkkN5wN8o4tzHTku3ODotFF8Kb%2F%2FkwmsRqxEvwi3Dz4G70xYA8%2Beysb0LsuO1MNmrxz2oYOd2lG1iXEQeEV1GRFM9IH25w7%2FiMBOIDX46HhrfCjqhVuWIALBodnIu9eaid4PAfHCTxOyXb6n5kE5e0K87cd9RVRZ7KglHyfTqLSbF9Jd7BqNy8bzBOc8hppVCbkW0C%2BucqUI6T2QsbyW81I1sOC5IpN6U3ADEWgG12w4pdgBEoJXbrIjMlM01STfGe3cp1KjJqkx0dpPGYoObZ%2Fvw23q2O7OGuNsLJuaAS3TN4UMSgC6GxOmElotlFmchKlIE4FclrcWXOqUWkbRY5Sp0fAgMBAAGjYzBhMA4GA1UdDwEB%2FwQEAwIBBjAPBgNVHRMBAf8EBTADAQH%2FMB8GA1UdIwQYMBaAFF9vWznJf0Hm5pEV%2BqG2tbLnglXVMB0GA1UdDgQWBBRfb1s5yX9B5uaRFfqhtrWy54JV1TANBgkqhkiG9w0BAQsFAAOCAgEAkiUD6hj4ZnPEhkNt2gnj4zlxed5DR%2BWOWTW3oen41rebQvbtH8lRsS2IYsLKR97TNzFIo5Aanrk9AA468L%2F5q4lvebXeCpKLbrX5mKyT3k%2BmWzyNFR%2FuMmDT7mFOP7iBMvIt3UsXdoZuTLwWpGzbfieBlQHk%2F87XA2bcaKWKwMXefPqt0g96QB59b3r1CsKasWuGzEqoK9BKuMhFElC0XzK19HdFmV6AUt1aEqw5bRduoMjpEg37p7ZjQl6VCnMWwTjy1Ttw%2B1H9f30dDRO%2FSgjibblOKrCpayxHnyULe2mteZAjX5OMaAEBtIYtWS%2FXt4lcqsRkA%2FerQsTFw2ED7fj7Ftbi8k7W73RTqcM%2BPuN1H6o0XmNU7TkgH9fgPnlw9E80EFQWVoVFOm0eMVfhuc24XHYwI5aixsZj0mU4MMmbbeSW3n5CzL0uWiaOcVCWgEoO8nkWhXnxkT2ASIxXnNThhY%2FTHPBaZDSS9T3puuqTm6jNXH%2FL8ZyEN8mR7iYKtPhLbIjo2SUFkgNT4TYi8i7bH3YkEC013EE4SBPO%2Frl9ZPAUjn7TPlPr7Kf15AdQZmyGD9itpBVutnKj799daeP2PeGG9J7Rz%2Bo%2BZ%2FjpkOdd6wFuP1XlTAR3h7luhzlvQT2Chrujth0zDlUR%2FqQVkKTOEark0byDjwMgKuvuQRk%3D%3BMIIG9zCCBN%2BgAwIBAgINAOceyVkAAAAAUygd5DANBgkqhkiG9w0BAQsFADBIMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBSb290IENBMB4XDTE0MDMyNTA0NDU0N1oXDTI0MDMyNTA1MTU0N1owSDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gQ0EgMjAxNDCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAMBi387JXbuIneYvs%2FNdvheVKb7RGrX5vuIrxaHw1iZNGru0T0pHwr%2FLE6BcFSLCJ5yc1Pr%2BrAjcTM9tQJAubZbLYx%2BOmZOJhHfCGbYtm35qpsVwF%2FhhCdNf5xfxLD2EdpSuM49xRy1%2F5C06qcA8o0GSyVBUUSaM6P4mI5t6zcUY1B1CW8aslv0AWIduvwMLeRXGEfO0jikiWF%2Bt1NcWI9toF%2BO%2BUoO7V%2B9893O%2B1PSaKuid7ZIA%2FOaBXsaIIpAHMsECIQ7ZZTpvexd%2FJSF6z62fRhBvWKUsHWCOckjp40nqtAgPqN8rVz0zzD%2BCJB44P5h%2FNgvaeP5HdAEnrcA8p5MxLaqZ%2BMZQAkLHNKNxsHuHNwETBDM%2FOq9eDCdG0UZW6YXm2DRzTagVaDduNKvqXooQej8QEycoONo5c9atFUZCTiBFS8wf6Hjp%2Fpl0UlfiHdUxayuhV2VDagQHlzMgnZFs3fww50r%2BcYEJXasBKoOVybT1ZDLz66eKOxfsyCHJfZhtkgM38TIEtsdfjcERjexWrYviAHj5Qz1dByacZGiDiB93dIVsX5jCnaRXZYdwbtD7EJGjkONntDO8EqIa%2BNwpH2DJyYz9FGtAR%2F5PhCusvbCu1jSJ31zpZTcX9doTqj9EcBNQOtwuTZEPakn0COGod9uW%2F9ah6UozKMvnJRU9AgMBAAGjggHeMIIB2jAOBgNVHQ8BAf8EBAMCAQYwEgYDVR0TAQH%2FBAgwBgEB%2FwIBADBRBgNVHSAESjBIMEYGByt8iFAFAQEwOzA5BggrBgEFBQcCARYtaHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY3BzL2NwZGVtb3Jvb3QxLTAucGRmMH4GCCsGAQUFBwEBBHIwcDAoBggrBgEFBQcwAYYcaHR0cDovL2RlbW8yMDE0LW9jc3AuZmluYS5ocjBEBggrBgEFBQcwAoY4aHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY2VydGlmaWthdGkvZGVtbzIwMTRfcm9vdF9jYS5jZXIwgaAGA1UdHwSBmDCBlTAyoDCgLoYsaHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY3JsL0RlbW9Sb290MjAxNC5jcmwwX6BdoFukWTBXMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBSb290IENBMQ0wCwYDVQQDEwRDUkwxMB8GA1UdIwQYMBaAFF9vWznJf0Hm5pEV%2BqG2tbLnglXVMB0GA1UdDgQWBBQ7hFoU9cU84Ug7XdEnNXvVZbwOKjANBgkqhkiG9w0BAQsFAAOCAgEANVRLUsyhXTHuaMYDRbyqIb2hLwm2VZzEyMgUYWNc%2B6WGhYKCcR0fADJco4%2FeAQ7rTMM5aSR%2FiAfkdC%2FDPpIWQt3IcnvBJxBZWRzYZ2DN7jHOj1GatAY%2BbE8ELGaZ%2FTiWcmePZEazemJ1oCMwpejqIQcnEpJ%2F51YGbMVCwLuEJ7I5%2BMVKbI6199ebSuLkxEt3wU1CUbEhScP0AICbxMEOnNYL37AXvhP3LSPJIc0xVM9xG26qMYDdtwsRjb5sxuca6Nso8newK9t3PU2khF%2FQcfu2OY1jPLdCVO8uNFvVCWpWb7fVPd3%2FTaM3rfaoDbFg%2FFBJGUZjEo0nwmblUDYMH5VAWcFmG%2FsOhkoSEtMyWMR7UL5PQEeT9Uxy708tsVgYZaG%2BG4bo6ZCGrEY%2FrDXoMneVAOrHIfBmyNLmfPqJLdrln%2Bu5Nt%2Bn1y52nfWbeuxLHNghdFA7g%2F%2FlJI2Wpk9Uc%2Bp5g%2B7XjTT6llUMZBBFiuuFTZtdtyF33JabUyr2QMCY6zkA1yxx8M4aoXavVlIPOegkFsgw9ohfHJlV5xB870JVkXd3RCVn%2FYPfA9GATfY1eF0hm6cCOMn16yq%2FSW%2FMjk0MA%2B%2BxiRkcUtyDQQZ7LZod%2FXBIC5fTde%2BOWUDNQpJ%2BKMDZpZWpDiQTZMl3xVlAQ%2BoDGvzuzoLcNykYzj5wLFQ%3D%3BMIIICzCCBfOgAwIBAgIRAPOHXo1LAwpEAAAAAF2lsacwDQYJKoZIhvcNAQELBQAwSDELMAkGA1UEBhMCSFIxHTAbBgNVBAoTFEZpbmFuY2lqc2thIGFnZW5jaWphMRowGAYDVQQDExFGaW5hIERlbW8gQ0EgMjAxNDAeFw0yMDEwMTMwNjMzMDVaFw0yMjEwMTMwNjMzMDVaMIGsMQswCQYDVQQGEwJIUjEbMBkGA1UEChMSQVZJT04gTUVESUEgRC5PLk8uMRYwFAYDVQRhEw1IUjc3MTgyMDI0NjU2MQ8wDQYDVQQHEwZaQUdSRUIxEDAOBgNVBAQMB1pSSUxJxIYxDzANBgNVBCoTBk1BVEVKQTEXMBUGA1UEAwwOTUFURUpBIFpSSUxJxIYxGzAZBgNVBAUTEkhSODU5ODc2OTM4NDMuMS4zNTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAJ8NXK9K3taWQWOCvLoYE3uzm5Ml1VOMW5sZjtTbNWzGPscwCvfcS7tUrjap5O9Kv7yiO0jAtXFnWJHexuGPUwKwh%2B%2BgkcBjcY0DTn9BLStgdaGXBkE%2BUUP0s%2Bg9MDiUco3Y%2BKHT%2BmnCSalieSlb9%2BP4sTA3VBAJCPJ4wtEWhmqxImSM3nzV%2Fasp6uk7djT%2B9QeD1IVjRfbgEj2UAI70CGO4k0WZVsoLRxyoRFnJKbBl6gCUXLNjGGTwqaUB9JFTSpkQ0N1mhZmNJUguD4o8OJXoeDkgJHhqOnjXp6T1yMb41oMWqu%2F%2BCF5FF%2F%2BoiWZDOLp53QmMByMFQTEe1EIqaMkCAwEAAaOCA4kwggOFMA4GA1UdDwEB%2FwQEAwIGQDCBzAYDVR0gBIHEMIHBMIGzBgkrfIhQBSAMBgIwgaUwTAYIKwYBBQUHAgEWQGh0dHBzOi8vd3d3LmZpbmEuaHIvcmVndWxhdGl2YS1kb2t1bWVudGktaS1wb3R2cmRlLW8tc3VrbGFkbm9zdGkwVQYIKwYBBQUHAgEWSWh0dHBzOi8vd3d3LmZpbmEuaHIvZW4vbGVnaXNsYXRpb24tZG9jdW1lbnRzLWFuZC1jb25mb3JtYW5jZS1jZXJ0aWZpY2F0ZXMwCQYHBACL7EABADB9BggrBgEFBQcBAQRxMG8wKAYIKwYBBQUHMAGGHGh0dHA6Ly9kZW1vMjAxNC1vY3NwLmZpbmEuaHIwQwYIKwYBBQUHMAKGN2h0dHA6Ly9kZW1vLXBraS5maW5hLmhyL2NlcnRpZmlrYXRpL2RlbW8yMDE0X3N1Yl9jYS5jZXIwgaMGCCsGAQUFBwEDBIGWMIGTMAgGBgQAjkYBATByBgYEAI5GAQUwaDAyFixodHRwczovL2RlbW8tcGtpLmZpbmEuaHIvcGRzL1BEU1FDMS0wLWVuLnBkZhMCZW4wMhYsaHR0cHM6Ly9kZW1vLXBraS5maW5hLmhyL3Bkcy9QRFNRQzEtMC1oci5wZGYTAmhyMBMGBgQAjkYBBjAJBgcEAI5GAQYBMBgGA1UdEQQRMA%2BBDW16cmlsaWNAY3MuaHIwggEYBgNVHR8EggEPMIIBCzCBpqCBo6CBoIYoaHR0cDovL2RlbW8tcGtpLmZpbmEuaHIvY3JsL2RlbW8yMDE0LmNybIZ0bGRhcDovL2RlbW8tbGRhcC5maW5hLmhyL2NuPUZpbmElMjBEZW1vJTIwQ0ElMjAyMDE0LG89RmluYW5jaWpza2ElMjBhZ2VuY2lqYSxjPUhSP2NlcnRpZmljYXRlUmV2b2NhdGlvbkxpc3QlM0JiaW5hcnkwYKBeoFykWjBYMQswCQYDVQQGEwJIUjEdMBsGA1UEChMURmluYW5jaWpza2EgYWdlbmNpamExGjAYBgNVBAMTEUZpbmEgRGVtbyBDQSAyMDE0MQ4wDAYDVQQDEwVDUkwyMjAfBgNVHSMEGDAWgBQ7hFoU9cU84Ug7XdEnNXvVZbwOKjAdBgNVHQ4EFgQU8Zzmxt1qOZccPbkjdE8%2F9X%2Bc0MMwCQYDVR0TBAIwADANBgkqhkiG9w0BAQsFAAOCAgEABvRIhDdfahCD4QIhNe%2F2yrOtnBbU19K5zZ9FQYfaTJdOBp8mInzyzDqGjLrv9wWvaFq4DE%2FxRjLQV3EHD3MYWZvi2eyJ9%2Faw7Q%2FhFqLi9kDBm94%2B%2Bx28BlNY%2FtmH%2BEUKzpvGntUhywFPPr3IGGQE2LLdb1Zw6Ur9mAcxq7mfRqJBt3goXxqurWT0fTCaUJ95Fgrc17rdgcME99%2Fm3CEemts62gvRn7PTRO2XRGZlWA%2F5gt%2FPTtX%2BaFUszLDq%2FpWbYgdAxtdIUrrmAkUkfpUM9dXS1YoVYNYItuulAbMCIRaBILsMdu6AKsS4MDz5x4Bqpo97KsWjwuFyhI3NSQ02%2FFKWlOjf6UVoOOJ3JYPFO92S2NsjxNZ9r%2FMw6EJlJwNkb9cGVmVyy2ZA8UcyIe3GqlPy55UcAAkxiy6qz9fC2OR9aBArkvqBvsrDMHjr3oILJoYTxOJS9MKZ5mmJ0IFUZBK9FHJTYY%2Bfo3AV%2Fy0p38LaHxqWY9wnZ%2FT%2BEOwOCa9rwo5Jd8rpHtuY9WqYoscvRvkpzZJDK4xXDGnXibyAn0SBESd7OxGZwiX2dLaQWcU4rzHkQWcTRb3e37NORi4gRl4%2BNjos70HXcXH%2BQHQFsXuYyltEjMIo6TWzh5%2B9etrw17CHBuno2u678EsRG1Xr1VRCt8SpOdakWYJ%2BiH%2BlodE%3D";
        private static byte[] hash;

        static void Main(string[] args)
        {
            //Print base64string from digested pdf to send to web service for signing.
            //Console.WriteLine($"2. {GetBytesToSign(src, temp, "signature")}");

            //Attach signature to pdf after web service returns signature and certificates
            EmbedSignature(temp, dest, "signature", signature, dataToBeSigned);

            
            
            Console.ReadLine();
        }

        /// <summary>
        /// Methods which returns base64 digested PDF.
        /// </summary>
        /// <param name="unsignedPdf">Path to pdf which needs to be signed</param>
        /// <param name="tempPdf">Path to temporary pdf</param>
        /// <param name="signatureFieldName">Name of field</param>
        /// <returns></returns>
        public static string GetBytesToSign(string unsignedPdf, string tempPdf, string signatureFieldName)
        {
            if (File.Exists(tempPdf))
                File.Delete(tempPdf);

            using (PdfReader reader = new PdfReader(unsignedPdf))
            {
                using (FileStream os = File.OpenWrite(tempPdf))
                {
                    StampingProperties sp = new StampingProperties();
                    sp.UseAppendMode();

                    PdfSigner pdfSigner = new PdfSigner(reader, os, sp);
                    pdfSigner.SetFieldName(signatureFieldName);

                    PdfSignatureAppearance appearance = pdfSigner.GetSignatureAppearance();
                    appearance.SetPageNumber(1);
                    appearance.SetPageRect(new Rectangle(100, 100));
                    appearance.SetLocation("Varazdin");

                    //Creating container for emty signature, with atrivute where digest is calculated.
                    //ExternalHashingSignatureContainer external = new ExternalHashingSignatureContainer(PdfName.Adobe_PPKLite, PdfName.Adbe_pkcs7_detached);
                    //pdfSigner.SignExternalContainer(external, 8192);
                    //hash = external.Hash;

                    //Creating container for empty signature.
                    IExternalSignatureContainer external = new ExternalBlankSignatureContainer(PdfName.Adobe_PPKLite, PdfName.Adbe_x509_rsa_sha1);
                    pdfSigner.SignExternalContainer(external, 8192);

                    //Digest from created new temporary PDF with empty space for signature.
                    FileStream oso = File.OpenRead(temp);
                    hash = DigestAlgorithms.Digest(oso, DigestAlgorithms.SHA256);

                    return Convert.ToBase64String(hash);
                }
            }
        }

        /// <summary>
        /// Signing container with data retrived from web service
        /// </summary>
        /// <param name="tempPdf"></param>
        /// <param name="signedPdf"></param>
        /// <param name="signatureFieldName"></param>
        /// <param name="signature"></param>
        /// <param name="tbs"></param>
        public static void EmbedSignature(string tempPdf, string signedPdf, string signatureFieldName, string signature, string tbs)
        {            
            //Convert given data from web service to bytes
            byte[] signedBytes = ConvertToBytes(signature);
            byte[] toBeSigned = ConvertToBytes(tbs);

            using (PdfReader reader = new PdfReader(tempPdf))
            {
                using (FileStream os = File.OpenWrite(signedPdf))
                {
                    PdfSigner signer = new PdfSigner(reader, os, new StampingProperties());
                    IExternalSignatureContainer external = new MyExternalSignatureContainer(signedBytes, GetChains(), toBeSigned);
                    PdfSigner.SignDeferred(signer.GetDocument(), signatureFieldName, os, external);
                }
            }
        }

        /// <summary>
        /// Convert data from url to bytes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Certificates to add to PDF
        /// </summary>
        /// <returns>X509Certificate array</returns>
        public static X509Certificate[] GetChains()
        {
            var b = chain.Split("=");
            var e = HttpUtility.UrlDecode(b[1]);
            var c = e.Split(";");

            var certa = certificate.Split("=");
            var certb = HttpUtility.UrlDecode(certa[1]);

            X509CertificateParser parser = new X509CertificateParser();

            var cert = parser.ReadCertificate(Convert.FromBase64String(certb));

            X509Certificate[] chains = new X509Certificate[c.Length+1];

            chains[0] = cert;

            for (int i = 0; i < c.Length; i++)
            {                
                var aaa = parser.ReadCertificate(Convert.FromBase64String(c[i]));
                chains[i+1] = aaa;
            }

            return chains;
        }
    }
}
