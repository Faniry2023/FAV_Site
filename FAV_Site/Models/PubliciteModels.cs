using System.ComponentModel.DataAnnotations;

namespace FAV_Site.Models
{
    public class PubliciteModels
    {
        [Key]
        public Guid Id_pub { get; set; }
        public string? Id_utilisateur { get; set; }
        public string? Nom_pub { get; set; }
        public string? Descri_pub { get; set; }
        public string? Autre_descri { get; set; }
        public byte[]? Photo { get; set; }
        public string? PhotoUrl {  get; set; }
        public DateTime Date_pub { get; set; }
        
    }
}
