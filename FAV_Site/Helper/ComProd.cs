using FAV_Site.Models;

namespace FAV_Site.Helper
{
    public class ComProd
    {
        public HelperModel? helperModel { get; set; }
        public List<DescriptionPreci>? descriptionPrecis { get; set; } = new();
        public UtilisateurEtVendeur? utilisateurEtVendeur {  get; set; }
        public UtilisateurModels? utilisateurConnected {  get; set; }
        public  List<AchteurCommentaire>? listAcheteurCommentaire { get; set; }
        public RecIDProdAndCommentaire? recIDProdAndCommentaire;
    }
}
