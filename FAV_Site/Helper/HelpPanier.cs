using FAV_Site.Models;

namespace FAV_Site.Helper
{
    public class HelpPanier
    {
        public ProduitModels? Produit { get; set; }
        public ImageModel? Image { get; set; }
        public int Quantite { get; set; }
        public double PrixAPayer { get; set; }
    }
}
