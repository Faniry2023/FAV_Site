using FAV_Site.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAV_Site.Models
{
    public class CommandeModels
    {
        [Key]
        public Guid Id_commande {  get; set; }
        public string? Id_acheteur { get; set; }
        public string? Id_vendeur { get; set; }
        public string? lesIdProduit { get; set; }
        //ataovy liste ito de ampifanarana @ les id produit fotsiny 
        public string? quantite {  get; set; }
        public double prixTotal {  get; set; }
        public DateTime? Date_pub { get; set; }
        public string? Lieu {  get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public bool is_livrer { get; set; }
    }
}
