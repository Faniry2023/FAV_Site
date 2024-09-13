using FAV_Site.Models;

namespace FAV_Site.Helper
{
    public class HelperModel
    {
        public ProduitModels? Produit { get; set; }
        public ImageModel? Image { get; set; }
        public double Prix { get; set; }
        public int quantite {  get; set; }
        public List<DescriptionPreci>? descriptionPrecis { get; set; }
    }
}
