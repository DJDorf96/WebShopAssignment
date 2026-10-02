

using System.Security.Cryptography;

Shop shop = new Shop();

shop.OpretKunde("Jens Jensen", 12345678);
shop.OpretOrdre(12345678);

shop.FindManglendeBetaling();


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
}