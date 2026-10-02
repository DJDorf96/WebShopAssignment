

using System.Security.Cryptography;

Shop shop = new Shop();

shop.OpretKunde("Jens Jensen", 12345678); // Opretter en kunde.
shop.OpretOrdre(12345678); // Opretter en ordre gennem telefonnummeret.

shop.FindManglendeBetaling(); // Tjekker systemet for manglende betalinger og udskriver hvem der mangler at betale.

shop.RegistrerIndbetaling(1); // Registerer at Ordrenummer 1 har betalt.
Console.WriteLine("----------------------------------------------------------");
shop.RegistrerIndbetaling(1); // Viser her at AfventerBetaling har ændret sig fra true til false.
Console.WriteLine("----------------------------------------------------------");
shop.AfsendOrdre(1); // Afsender en ordre hvor AfventerBetaling = false + fjerner ordren fra ordrelisten.
Console.WriteLine("----------------------------------------------------------");
shop.AfsendOrdre(1); // Beviser at en betalt ordre der er afsendt, nu er slettet fra ordrelisten.
Console.WriteLine("----------------------------------------------------------");
shop.OpretOrdre(12345678); // Beviser at kunden stadig eksisterer.


class Shop
{
    private List<Kunde> kundeliste = new List<Kunde>();
    private List<Ordre> ordreliste = new List<Ordre>();
    public void OpretKunde(string navn, int telefonnummer)
    {
        Kunde kunde = new Kunde(navn, telefonnummer);
        kundeliste.Add(kunde);
    }
    public void OpretOrdre(int telefonnummer)
    {
        int i = 0;
        while(i < kundeliste.Count)
        {
            Kunde kunde = kundeliste[i];
            if(kunde.HarTelefonnummer(telefonnummer))
            {
                Ordre ordre = new Ordre(kunde);
                ordreliste.Add(ordre);
                Console.WriteLine("Ordren blev oprettet!");
                return;
            }
            i++;
        }
        Console.WriteLine("Kunden findes ikke.");
        
    }
    public void FindManglendeBetaling()
    {
        int i = 0;
        while(i < ordreliste.Count)
        {
            Ordre ordre = ordreliste[i];
            if(ordre.ManglerBetaling())
            {
                Console.WriteLine("Vi har fundet en manglende betaling!");
                Console.WriteLine($"OrdreNummer: {ordre.HentOrdreNummer()}, Navn: {ordre.HentKundeNavn()}");
            }
            i++;

        }
    }
    public void RegistrerIndbetaling(int ordrenummer)
    {
        int i = 0;
        while(i < ordreliste.Count)
        {
            Ordre ordre = ordreliste[i];
            if(ordre.HarOrdreNummer(ordrenummer))
            {
                if(ordre.ManglerBetaling())
                {
                    ordre.ÆndreBetaling();
                    Console.WriteLine(ordre.HentOrdreNummer());
                    Console.WriteLine(ordre.ManglerBetaling());
                    Console.WriteLine("Ordren er nu betalt.");
                    return;
                }
                Console.WriteLine("Ordren er allerede betalt.");
                return;
            }
            i++;
        }
        Console.WriteLine("Ordrenummeret findes ikke i systemet.");
    }
    public void AfsendOrdre(int ordrenummer)
    {
        int i = 0;
        while(i < ordreliste.Count)
        {
            Ordre ordre = ordreliste[i];
            if(ordre.HarOrdreNummer(ordrenummer))
            {
                Console.WriteLine("Vi har fundet ordren.");
                if(ordre.ManglerBetaling())
                {
                    Console.WriteLine("Ordren mangler betaling. Afsendelse annulleret.");
                    return;
                }
                else
                {
                    Console.WriteLine("Ordren er betalt. Afsendelse påbegyndt.");
                    ordreliste.Remove(ordre);
                    return;
                }
            }
            i++;
        }
        Console.WriteLine("Ordren findes ikke i systemet.");
    }
}

class Kunde
{
    private string Navn;
    private int TelefonNummer;
    public Kunde(string navn, int telefonnummer)
    {
        Navn = navn;
        TelefonNummer = telefonnummer;
    }
    public bool HarTelefonnummer(int telefonnummer)
    {
        return TelefonNummer == telefonnummer;
    }
    public string HentNavn() // "Dørklokke" til at hente et navn, nu hvor Navn er Private.
    {
        return Navn;
    }
}

class Ordre
{
    private static int OrdreTæller = 0;
    private int OrdreNummer;
    private bool AfventerBetaling = true;
    private Kunde kunde;
    public Ordre(Kunde kunde)
    {
        OrdreTæller++;
        OrdreNummer = OrdreTæller;
        this.kunde = kunde;
    }
    public bool ManglerBetaling()
    {
        return AfventerBetaling;
    }
    public string HentKundeNavn()
    {
        return kunde.HentNavn();
    }
    public int HentOrdreNummer()
    {
        return OrdreNummer;
    }
    public bool ÆndreBetaling()
    {
        AfventerBetaling = false;
        return AfventerBetaling;
    }
    public bool HarOrdreNummer(int ordrenummer) // Tjekker om ordrenummer findes.
    {
        return OrdreNummer == ordrenummer;
    }
}