using FAV_Site.Models;

namespace FAV_Site.Helper
{
    public class VendeurAvecCesProduit
    {
        public string? IdVendeur {  get; set; }
        public string? nomVendeur { get; set; }
        public List<PanierModels> panierModels { get; set; }
        public double prixChaqueVendeur { get; set; }
    }
}
