

Shop shop = new Shop();

shop.OpretKunde("Jens Jensen", 12345678);

class Shop
{
    private List<Kunde> kundeliste = new List<Kunde>();
    public void OpretKunde(string navn, int telefonnummer)
    {
        Kunde kunde = new Kunde(navn, telefonnummer);
        kundeliste.Add(kunde);
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
}