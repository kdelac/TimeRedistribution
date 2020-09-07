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

<h3>Elasticsearch</h3>

<ol>
  <li>nakon indeksiranja podakata, kako su podaci zapisani u indeksu</li>
  <p>Svaki indeks ima svoje polje koje se naziva documents, koje služi npr. kao jedan redak u tablici. Podaci se u documents zapisuju u obliku jsona.</p>
  <li>pstoji li možda neki windows alat sa kojim se može zaviriti u indeks</li>
  <p>Postoji Kibana, pomoću nje se može provjeravati sva statistika i pomoću nje možemo slati API calove na elasticsearch</p>
  <li>koje su opcije indeksiranja</li>
  <p>Postoje mnoge opcije indeksiranja. Može se indeksirati već po postojećim modelima, samo onda treba ignorirati atribute koji nam nisu potrebni. Moguće je staviti ime polja koje indeksiramo, tip...</p>
  <li>kako si podignuo elastic (docker ili instalacija)</li>
  <p>Elastic sam podigao preko instalacije.</p>
  <li>koje su opcije pretrage po indeksu</li>
  <p>Sve se može vrlo lako pretraživati, mislim da nisam naišao na opciju koju nije moguće pretraživati. Može se napraviti i custom pretraživanje, recimo ako želimo da ima autocomplete i to je moguće.</p>
  <li>Nest koji si koristio za pretragu je neki search extension ili ima veze sa elasticom</li>
  <p>Nest je ekstenzija od elastica. Pomoću njega se u .net-u koristi elastic.</p>
  <li>Kakve su performanse</li>
  <p>Pretraživanje je jako brzo i rezultati se odmah dobiju. Maksimalno se od jednom može dobiti 10000 rezultata. Ali ako se straniči to je uredu.</p>
  <li>koliko se troši u tvom case-u memorije i diska</li>
  <p>Na oko 12000 zapisa troši jako malo memorije i oko 3mb diska.</p>
</ol>  
<p>Sve u svemu jako brzo se izvršava pretraga. Ako iz baze nije potrebno dovlačiti sve podatke o nekoj osobi onda je sasvim uredu zapisati sve u indeks i sve te podatke poslati na frontend. Ne troši puno resursa i praktično je za korištenje. Ima puno opcija koje se mogu koristiti.</p>

<h4>Autocomplete</h4>
<p>Implementiran je autocomplete, muči me samo mpiranje atributa tipa CompletionField koje bi nako mapiranja u elasticksearchu trebalo biti tipa Completion, a on ga u indexu mapira kao text. Što se tiče rada s autocompletom prilično je jednostavno, ako se zada riječ ili slovo onda pretažuje po riječima koje su zadane u CompletionFildu.</p>
<img src="https://github.com/kdelac/TimeRedistribution/blob/master/Documents/class.png">
<p>Na slici se vidi prikaz klase korisnika i polje tipa CompletionField na donjoj slici vidimo prikaz u kibani i da je type text, a trebao bi biti Completion.</p>
<img src="https://github.com/kdelac/TimeRedistribution/blob/master/Documents/kib.png">
<p>Za provjeru je potrebno skinuti kibanu i elasticsearch ili ih pulati u docker. U kibani u konzoli se može provjeriti mapiranje indeksa s pozivanjem API-a "GET users/_mapping" u konzoli.</p>

<h5>Rješeno</h5>
<p>Problem je bio u mapiranju i to u polju koje je tipa Type. Mapper nije mogao normalno mapirat zbog tog polja. Prebacio sam ga u string i sad radi sve kako treba. Pretraživanje je vrlo jednostavno kao što sam već rekao pretražuje se po riječima koje su spremljene u polje CompletionField. Postoji i fuzzines gdje definiramo koliko pogrešaka može biti u stringu kojeg napišemo. Npr. ako je 1 onda znaći da 1 solovo korisnik može pogriješiti i da će naći sve slične korisnike usprkos to jednoj pogrešci.</p>

<h3>Slanje emaila</h3>
<p>Napravio sam projekt tipa Worket pod nazivom MailerWorker. Radi na principu da cijelo vrijeme osluškuje port od activemq-a i kada poruka dođe na amq, program odmah u log zabilježi da je poruka došla, odnosno treba implementirati slanje email-a. U RescheduleService dodao sam metodu koja šalje event na activemq-kad se raspored promjeni. Što se sapajanja tiče u docker-compose file-u dfinirao sam 4. Prvi je activemq, koji radi na lokanom portu 8888, a spajanje workera na activemq korstim depends-on i zatim u connection stringu ne upisujem localhost nego naziv servisa koji sam definirao u docker-composeu i port koji acticemq koristi za osluškivanje: 'tcp://activemq:61616'.
</p>

<h3>Restore baze</h3>
<p>Treba pokrenuti docker-compose up kako bi se sve slike stvorile. Svi kontenjeri će se pokrenuti ali pošto još nema baze potrebno je nakon dodavanja baze restartati kontenjer appointmentredistribution. Kako bi se restorala baza potrebno je izvršiti dve linije u cmd-u. Linije se nalaze u txt datoteci pod nazivom komande. Na mjesto <idcontainera-sql> potrebno je staviti id kontenjera od mssql-a. Prva komandu je potrebno izvršiti iz mape MedApp, drugoj je potrebno promjeniti samo <idcontainera-sql> u id. Nakon što se to napravi u kontenjeru će se pojaviti baza sa svim podacima. Potrebno je izbrisati sve appointmente i dodati nove na dodaj termine zbog toga što se ti podaci prvovjeravaju za svaki dan. U pocMin treba staviti minute koje su dijeljive s 5 i bile su minimalno prije 5min (npr. sad je 14:10, u pocMin treba staviti 5) kako bi reschedule mogao raditi. U pocSat treba samo staviti koliko je sati i u razmak staviti 5. Dodaj termine će dodati određen broj termina. I sad je problem što preraspodijela termina iz nekog razloga ne radi.
</p>
  
<h3>ActiveMQ</h3>
<p>U projektu sam iskoristio Composite destinations za slanje poruka na ActiveMQ korištenje je vrlo jednostavno, slučaj je bio da na više servisa treba poslat istu poruku. To bi se moglo ostvariti pomnoću topica, ali ako imamo više istanci servisa, sve istance bi dobile istu tu poruku, a to ne želimo. Prvi pristup mi je bio napravit dva ili više Queue-a i onda pomoću svakog zasebno poslati dvije poruke. Pomoću composit destinationa postavljamo jednu poruku s više odredišta. Poruke se primaju kao običan queue. Kao odredište se može postaviti i queue i topic, ako je to potrebno. U mom slučaju rade se dva queue-a i svaki servis zasebno priba samo jednu poruku, nije bitno koliko istanci tog servisa postoji.
</p>

<h3>Identity Server</h3>
<p>Dodana su dva nova projekta u solution jedan je za identityserver4, a drugi je web mvc aplikacija koja koristit identity server. U identity serveru napravljena je klasa u kojoj su definirani podaci pomoću kojih se vrši autentifikacija. U njoj su 3 različita klijenta, prvi samo koristi clientcredentialse da zaštiti pristupanje api-u. Drugi klijent koristit hybrid flow za autorizaciju. Treći koristi implicit flow, odnosno Authorisation Code flow s PKCE-om koji služi za dodatnu sigurnost. Implicit flow najbolji je u kada se koristi s nativnim aplikacijama i sa SPA. U projekt je implementirano i povezivanje u bazu pomoću kojega sve podatke, klijente, role, claimove, možemo spremiti u bazu. U mvc projektu je implementiran openIDconnect ponoću kojega definiramo sve podatke koji nam omogućavaju autorizaciju. Koriste se dva scopa od kojih je jedan za pristup api-u.
</p>

<h3>Transakcija</h3>
<p>Za izvršavanje transakcije koristi se orcherstrator servis u kojemu se odvija logika transakcije. Kada se kreira novi appoitment u WebApi projktu samo se poziva api za kreiranje appoitmenta. Pomoću tog projekta se kreira novi appoitment s izvršavanjem transakcije. Nakon što se kreira appoitment kreira se event na amq-u i kreira se log u tablici da je kreiran appoitment koji služi u slučaju da je amq-u pao. Worker orcherstrator je zadužen za slušanje eventa i za provjeru log tablice ako event nije poslan. U slučaju neuspijeha transakcije, a taj slučaj je ako račun nije ispostavljen briše se appoitment i šalje se event o neuspijelosti transakcije. Svaki korak se bilježi u log tablicu. Ako transakcija ne uspije ili uspije, briše se iz tablice log i prebacuje se u drugu tablicu s datumom kad je transakcija uspješno ili neuspješno izvršena. Brisanje iz log tablice i prebacivanje u drugu vrši se pomoću dva trigera nad log tablicom. Za test potrebno je u bazi imati barem jednog doktora i jednog pacijenta i preko WebApi projekta dodat appoitment.
</p>

<h3>Unit testing</h3>
<p>
  Unit testing služi za provjeru programskog koda. Puno pomaže ako određeni dio koda mora biti implementiran na točno određeni način. Pa ako se kod promjeni unit test neće proči i odma će se znati što je potrebno napraviti kako bi kod bio dobar. Ima mnogo mogućnosti i načina testiranja koda. Kako bi napravili testiranje prvo je potrebno napraviti novi objekt repozitorija, servisa koji želimo testirati. Sve interface koji koriste ti servisi ili reposotpriji potrebno je mockati kako bi se postavili lažni podaci. Nakon toga se postavljaju podaci, koji tip servis ili repozitorij vraća, moguće je postaviti razne testne modele, testne liste kako bi se određena metoda mogla istestirati. Nakon što je sve postavljeno pregledava se dali servis vraća dobar tip, dali su podaci iz liste točni, dali vraća dobar broj elemenata u listi. Moguće je provjeriti i dali vraća neki exeption.
</p>
</body>
