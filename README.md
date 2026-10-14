CLIVerktøy

Cliverktøy er en konsoll-applikasjon i C# som gjenskaper enkel fil-håndtering med kommandoer inspirert av unix/linux terminaler.
Denne applikasjonen bruker Spectre.Console for og gi en interactive meny.

kommandoer

ls – Lister opp alle filer og mapper.

cat – Leser og printer ut den valgte filen.

echo – Printer tekst til konsoll (ikke tilgjengelig gjennom Spectre.Console).

pwd – Endrer filmappen som applikasjonen hådnterer.

head – Leser og printer de etterspørte linjene fra toppen av den valgte filen.

tail – Leser og printer de etterspørte linjene fra bunnen av den valgte filen.

wc – Leser og printer informasjon om filen: antall ord, antall linjer, antall karakterer og antall bytes.

touch – Lager en ny fil.

cp – Kopierer en fil til en filmappe og generer et unikt kopi navn.

mv – Flytter en fil til en annen mappe.

rm – Sletter den angitte filen.

help – Viser tilgjengelige kommandoer 

exit – Exits the application. (ikke tilgjengelig gjennom Spectre.Console).


krav

.NET SDK kompatibel med prosjektets rammeverk.

Spectre.Console NuGet package.


initialisering og bruk

klone eller laste ned repository, åpne en terminal i project mappen.

gjennopprett avhengigheter:

dotnet restore

Kjør applikasjon:

dotnet run


Applikasjonen tilbyr bruk som en guided meny, eller tekst-basert kommandoer ved start.

Den akksepterer også Command-line arguments:


dotnet run -- ls
dotnet run -- cat mappe1\Alpha.txt
dotnet run -- echo Hello World
dotnet run -- head mappe2\Omega.txt 5
dotnet run -- tail mappe3\Beta.txt 100
dotnet run -- wc mappe1\Alpha.txt
dotnet run -- touch touch.txt Files
dotnet run -- cp mappe1/Alpha.txt Files/mappe2
dotnet run -- mv mappe2/Omega.txt Files/mappe3
dotnet run -- rm mappe3/Beta.txt
dotnet run -- help


Prosjekt struktur

Program.cs – startpunkt for applikasjon og valg av modus.

Menu.cs – Håndterer bruker inputs, meny navigering og kommandoer.

FileHandler.cs – Implementerer fil og mappe operasjoner.

begrensninger

Prosjekt eksister hovedsakelig som en øvelse og implimenterer ikke helt identisk funksjoner fra et offisielt shell.
Kommando-parsing er veldig enkel. Fil-stier og filnavn med mellomrom er ikke støttet.

Applikasjon arbeider på et spsifisert omeråde innenfor prosjekt mappen. Den kan ikke operere utenom dette.

vær ekstra forsiktig ved bruk av kommandoer som skaper eller sletter filer.

bruk av KI:

Ki har blitt brukt under prosjektet. Men bare som et verktøy for og hjelpe med feilsøking. Lage dokumentasjon.
Og gi forslag til enkelte API.kommandoer