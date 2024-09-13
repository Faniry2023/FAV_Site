using System.ComponentModel.DataAnnotations;

namespace FAV_Site.Models
{
    public class ImageModel
    {
        [Key]
        public Guid Id { get; set; }
        public string? Id_produit { get; set; }
        public string? ImageUrlCouv {  get; set; }
        public string? ImageUrl_1 { get; set; }
        public string? ImageUrl_2 { get; set; }
        public string? ImageUrl_3 { get; set; }
        public string? ImageUrl_4 { get; set; }
    }
}
