using FAV_Site.Models;

namespace FAV_Site.Helper
{
    public class AffichePanCom
    {
        public string? nomVendeur { get; set; }
        public List<HelperModel> help { get; set; }
        public double prixChaqueVendeur { get; set; }
    }
}
