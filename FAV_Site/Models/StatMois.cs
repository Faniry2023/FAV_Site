using System.ComponentModel.DataAnnotations;

namespace FAV_Site.Models
{
    public class StatMois
    {
        [Key]
        public Guid id { get; set; }
        public string? id_produit {  get; set; }
        public int jan {  get; set; }
        public int fev { get; set; }
        public int mar {  get; set; }
        public int avr { get; set; }
        public int mai { get; set; }
        public int jui { get; set; }
        public int juill { get; set; }
        public int aou { get; set; }
        public int sep {  get; set; }
        public int oct {  get; set; }
        public int nov {  get; set; }
        public int dec {  get; set; }
    }
}
