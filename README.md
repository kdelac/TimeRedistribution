# TimeRedistribution

<body><p>Solution se sastoji od dva glavna projekta, jedan služi za pozivanje web API-ja (TimeRedistribution), a drugi(AppoitmentRedistribution) je za promjenu termina. Ostala tri projekta služe za obradu podataka.</p> 

<h3>Promjena termina</h3>
<p>Za promjenu termina koristi se RescheduleService koji ima glavnu metodu koja prima tri parametrea. Prvi parametar je broj minuta s kojima se određuje kašnjenje. drugi parametar je datum, odnosno dan za koji će se radit provjera, treći parametar je status, on se postavlja u ovom slučaju na "Waiting", odnosno provjeravaju se svi termini koji su na čekanju. Na početku metoda vrati sve doktore koji imaju zakazane termine. Potom se za svakog doktora uzmu termini na dan kada je postavljen datum u metodi i sa postavljenim statusom. Potom se provjeravaju svi termini koje treba premjestiti, ako se sljedeći termin ne preklapa prestaje se sa pregledom termina trenutnog doktora i prelazi se na idućeg doktora.</p>

<h3>AppointmentRedistribution</h3>
<p>Ovo je projekt tipa service worker koji poziva preraspodijelu vremena, u njemu se može postaviti svakih koliko vremena hoćemo da se preraspodijela pokrene</p>

</body>
