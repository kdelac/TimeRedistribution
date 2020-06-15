# TimeRedistribution

<body><p>Solution se sastoji od dva glavna projekta, jedan služi za pozivanje web API-ja (TimeRedistribution), a drugi(AppoitmentRedistribution) je za promjenu termina. Ostala tri projekta služe za obradu podataka.</p> 

<h3>Promjena termina</h3>
<p>Za promjenu termina koristi se RescheduleService koji ima glavnu metodu koja prima tri parametrea. Prvi parametar je broj minuta s kojima se određuje kašnjenje. drugi parametar je datum, odnosno dan za koji će se radit provjera, treći parametar je status, on se postavlja u ovom slučaju na "Waiting", odnosno provjeravaju se svi termini koji su na čekanju. Na početku metoda vrati sve doktore koji imaju zakazane termine. Potom se za svakog doktora uzmu termini na dan kada je postavljen datum u metodi i sa postavljenim statusom. Potom se provjeravaju svi termini koje treba premjestiti, ako se sljedeći termin ne preklapa prestaje se sa pregledom termina trenutnog doktora i prelazi se na idućeg doktora.</p>

<h3>AppointmentRedistribution</h3>
<p>Ovo je projekt tipa service worker koji poziva preraspodijelu vremena, u njemu se može postaviti svakih koliko vremena hoćemo da se preraspodijela pokrene</p>

<h3>SignService</h3>
Koristim se nugetom: dotnet add package BouncyCastle --version 1.8.6.1
U pk varijabla sadrži privatni ključ korisnika. Prije toka krajni korisnik će morati upisat šifru koja mu stoji u certifikatu. For petlja služi da certifikate upišemo pravilnim raedom kako kad bi se provjeravali nebi došlo do greške. Zatim učitamo pdf koji smo uzeli u pdfreader te napravimo novi pdf koji će biti potpisan. Appearence koristimo kako bi postavili sve što želimo da piše u potpisu, može se i fizički na stranicu ispisat sve što je potrebno. Zatim se dodijeli privatni ključ koji smo uzeli napočetku i zatim pomoću metode SingnDetached pitpišemo pdf.

<h3>Potpisivanje</h3>
<p>Za svaku osobu treba se izdat certifikat po kojem će se potvrditi identitet te osobe, svaka osoba ima lozinku za svoj certifikat. Certifikati se dobivaju od FINA-e. Prilikom potpisivanja koristi se izdani certifikat i upisuje se lozinka kako bi se potvrdio identitet. Zatim pomoću BouncyCastle nugeta pdf-u se dodijeljuje potpis, prvo se provjeri lozinka i certifikat i ako je sve uredu dokument se potpiše sa svim zadanim podacima. Ako se u kojem slučaju potpisani pdf dokument pokuša promijeniti. Kad se pdf otvori u adobe acrobat readeru vide se svi podaci o certifikati i o osobi koja ih je potpisala</p>
</body>
