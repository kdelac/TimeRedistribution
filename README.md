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

<h3>RxNET i signalR</h3>
<p>
  U SignalR projektu nalazi se SignalR, RxNET. Pomoću projekta prate se klijenti u ordinaciji. Kada korisnik stisne tipku za prijavu u ordinaciju pomoću SignalR web socket šalju se njegov id i id ordinacije u koju se želi prijaviti. Zatim se pomoću rxNeta publisha event. Nakon što se event publisho u backgroun servisu se subscriba na taj event i kada se dogodi u bazi se odradi upis prijave ili odjave. Ovisi koji je gumb stisnut SignalR šalje različite podatke i događa se prijava ili odjava iz ordinacije. U bazi se provjerava koliko je ljudi u ordinaciji, a koliko čeka vani. Ako je dosegnut naksimalan broj pacijenata tog dana oglašava se obavjest, svakom prijavom pacijent dobije novo stanje. Ako je u ordinaciji dvoje, a maksimalno u ordinaciji može bit dvoje novi pacijent stavlja se na listu čekanja. Kada se neko odjavi briše se s popisa prijavljenih. Ako je pacijent bio u ordinaciji onda se briše i uzima se jedan iz reda čekanja, gleda se po vremenu dolaska.  Sve se odvija pomoću rxNeta koji publisha evente i zatim se subscrajba na njih. Sve se odvijea pomoću event handlera.
</p>

<h3>gRPC</h3>
<p>
  gRPC je framework koji koristi RPC(Remote procedure call), praktičan je zato što se može implementirati sa većinom dostupnih tehnologija.   
  gRPC služi za komunikaciju između klijenta i poslužitelja. Dobar je i za komunikaciju između dva servisa. Može imati jednosmjernu vezu gdje se podaci samo pošalju. Može imati streaming podataka sa servera prema klijentu i obratno, a može i streamati podatke u oba smjera istovremeno. gRPC se može koristiti na velikom broju platformi i zbog toga je dobar za korištenje. Poziva se kao funkcija i vrlo je lak za korištenje. Servis se definira u .proto datoteci. Klijen je u mogućnosti specificirati koliko želi čekati da mu servis vrati podatke. Pošto se se podaci enkodiraju nisu čitljivi kao kod HTTP requestova.
  Pomoću gRPC-a aplikacija može direkto pozivati netode s drugog servera kao da je metoda lokalna. Omogućava lakšu implementaciju aplikacija koje imaju servise na više servera.
  Za implementaciju je potreban .proto file u kojemu su definirane metode i funkcije koje se koriste. I klijentska i serverska strana moraju imati isti .proto file kako bi se metode mogle koristit.
  Problem kod ovoga je što se ne može deployat na azure zbog toga što koristi http/2. Za sad još nije izašala verzija da bi se moglo deployat na Azure.
  
  Prednosti RPC-a što ima komunikaciju prema serveru preko proceduralnih poziva, može se koristiti u distribuiranom okruženju, ali i lokalno, nisu čitljive poruke krajnjem korisniku, lagano je prepraviti kod i kako bi poboljšao preformanse izostavlja neke od protokola.
  Nedostatci: korsite se failovi za definiranje, svaki sustav mora imati isti fail kako bi radio, lako dođe do neuspjeha zbog komunikacije s raznim tehnologijama, nema standardnu implementaciju pa zbog toga može biti implementiran na razne načine.
  
  Implementirao sam gRPC servis koji kreira novi račun. U .proto fileu definirane su metode koje se koriste. Taj isti .proto file kopirao sam u servise i pomoću njega implementirao klijentsku stranu. U servisu sam morao samo pozvati metodu za kreiranje billinga.
</p>
</body>

<h3>JMeter</h3>
<p>
JMeter je aplikacija koja između ostalog služi za ispitivanje brzine API-ja. U JMetru sam napravio nekoliko poziva apija. Napravio sam jedan poziv za doktore, jedan za pacijente jer njihov id je potreban kod kreiranja appointmenta. U jmetru je napravljeno i kreiranje appoitmenta, zatim se dohvate svi appoitmenti uzme jedan radnom id pa se potom brise taj appointment. U Jmetru ima mnogo parametara koji se mogu pratiti kod izvršavanja apija. Svaki je imao određenu propusnost pa sam kako bi ubrzao proces na nekoliko tablica stavio indexe. Nakon što sam postavio indaexe na tablicu doktora, pacijenta i appointmenta dobio sam bolje rezultate nego bez tih idexa. Probao sam ubrzati i sa storanim proceduurama, ali iz nekog razloga sustav radi sporije. JMetar je dobra aplikacija jer u njoj možemo provjeriti nedostatke apija.
</p>

<h3>Nosql</h3>
<p>
U kodu je složen rad s nosql podacima. Napravljen je context za spajanje na mongodb i neki generički repozitori za crud operacije. Dodao sam indexe u bazu i znatno se povečala brzina pretraživanja po određeno atributu. Čak i 300 puta se ubrza proces pretraživanja po nekom atributu ako se stavi index. Kombinacijom indexa još se može ubrzati proces pretraživanja. Na 1,3milijuna podataka pretraživanje po atributu je trajalo nekoliko sekundi, nakon što sam stavio index ubrzalo se na manje od 10ms za isti atribut.
</p>

<h3>DDD</h3>
<p>
Ovaj koncept radi se na način da se sav kod mora slagati s nekom poslovnom domenom. Kada se primjeni ovaj koncept s lakoćom se može vidjeti što neka aplikacija radi, odnosno što se sve dešava  toj jednoj domeni. Piše se na način da i osoba koja nije programer može s lakoćom primjetiti kako sve funkcionira. Domenom nazivamo dio koji se odnosi na neko područje. Npr. u mom projektu domena je raspored. Svaka domena ima svoj context kako bi se olakšao rad. Kad bi u nekoj organizaciji radili aplikaciju s mnoštvom domena, a samo jednim contextom došlo bi do mnogo konflikata. Kada se primjenjuje ovaj koncept nije preporučeno da dve razlićite domene imaju iste tablice u bazi. Kako između domena ne bi došlo do konflikata svaka domena ima svoj domenski model i dio koji je pisan razumljivim jezikom. 
  Meni osobno jako se sviđa ovaj način implementacije, ima dosta više posla ali je lako za shvato što, kako i gdje.
  
 Napravio sam eventdispacher, koji prima domain eventove i aktivira ih pomoću mediatora. U mediatormodul sam registiro dispaecher i koristio ga u repozitorijima nakon što se dogodi insert u bazu. Da se radi o EF pozivali bi ga nakon što se pozove SaveCanges. Napravio sam dva hendlera koja upravljaju tim eventovima, trenutno samo u konzolu ispisuju što se točno dogodilo.
</p>
</body>

<h3>Confluence</h3>
<p>
  Confluenc nudi kreiranje nekoliko prostora za dokumentiranje. Imamo prostor u kojemu definiramo tehničku dokumentaciju, korisničke zahtjeve i ostalo što se veže na dokumentaciju projekta. Zatim imamo prostor u kojem se bilježe timske aktivnosti (što tim radi, kad su bili sastanvi i što se na tim sastancima dogovorilo). Zatim imamo dokumentaciju koja je povezana s softwearom koji je u izradi. On prati događaje na Jiri, unutar njega mogu se definirati novi taskovi na Jiri, referencirat već postojeći taskovi na Jiri odnosno u ovom prostoru piše se sve što je usko vezano za softwear. Postoji još nekoliko prostora: Osobni prostor, gdje se mogu zapisivati osobna zapažanja i bilješke, te prostor gdje se dijele s drugima u timu nova znanja, kako nešto napraviti dobro... Prilikom kreiranja prostora nudi nam se tih nekoliko opcija: 
</p>
<img src="https://github.com/kdelac/TimeRedistribution/blob/master/Documents/prostor.png?raw=true">
<p>Štp se tiče Jire i Confluenca prilikom Softweare project prostora mora se dodijeliti s kojim projektom na Jiri projekt biti spojen. Kada se stranice uređuju mogu se dodavati razni linkovi. Postoje insertovi za razne chartove na Jiri i sve što Jira nudi, mogu se dodavati isječci koda... Kad govorimo o prostoru koji nije usko povezan s Jirom možemo mu dodijeliti linkove koji šalje direktno na Jira projekt. Isto tako na Jiri se može dodati poveznica koja vodi direktno na tu dokumentaciju. </p>
</body>

<figure>
  <img src="https://github.com/kdelac/TimeRedistribution/blob/master/Documents/spacej.png">
  <figcaption style="text-align:center">Dodavanje prostora na Jiru</figcaption>
</figure>

<figure>
  <img src="https://github.com/kdelac/TimeRedistribution/blob/master/Documents/spacs.png" >
  <figcaption style="text-align:center">Dodavanje linka na Jiru na neki od prostora</figcaption>
</figure>

<p>Pomoću linkova povezani su neki od prostora i Jira.</p>

<figure>
  <img src="https://github.com/kdelac/TimeRedistribution/blob/master/Documents/dodavanje.png?raw=true" >
  <figcaption style="text-align:center">Razne mogućnosti koje se mogu dodati na stranicu</figcaption>
</figure>

</body>
