using System.ComponentModel.DataAnnotations;

namespace FAV_Site.Models
{
    public class PanierModels
    {
        [Key]
        public Guid Id { get; set; }
        public string? Id_produit { get; set; }
        public string? id_acheteur { get; set; }
        public string? id_vendeur { get; set; }
        public double prix_total {  get; set; }
        public int quantite { get; set; }
    }
}
