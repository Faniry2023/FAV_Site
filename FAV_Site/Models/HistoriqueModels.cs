using System.ComponentModel.DataAnnotations;

namespace FAV_Site.Models
{
    public class HistoriqueModels
    {
        [Key]
        public Guid Id { get; set; }
        public string? Id_acheteur {  get; set; }
        public string? Id_vendeur { get; set; }
        public string? les_id_produit { get; set; }
        public double Prix_a_payser { get; set; }
        public DateTime Date_achat { get; set; }
        public string? les_quantite { get; set; }
    }
}
