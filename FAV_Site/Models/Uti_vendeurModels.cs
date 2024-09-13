using System.ComponentModel.DataAnnotations;

namespace FAV_Site.Models
{
    public class Uti_vendeurModels
    {
        [Key]
        public Guid Id_uti_ven {  get; set; }
        private string? id_uti;
        private  string? id_ad;
        private string? nom_societe;
        private string? lieuVen;
        private string? refLogiciel;

        public Uti_vendeurModels(string id_uti, string id_ad, string nom_societe, string lieuVen, string refLogiciel)
        {
            this.id_uti = id_uti;
            this.id_ad = id_ad;
            this.nom_societe = nom_societe;
            this.lieuVen = lieuVen;
            this.refLogiciel = refLogiciel;
        }

        public Uti_vendeurModels()
        {
            id_uti = "inconnue";
            id_ad = "inconnue";
            nom_societe = "inconnue";
            lieuVen = "inconnue";
            refLogiciel = "inconnue";
        }
        public string? Id_uti { get { return id_uti; } set { id_uti= value; } }
        public string? Id_ad { get { return id_ad; } set { id_ad = value; } }
        public string? Nom_Societe { get { return nom_societe; }set { nom_societe = value; } }
        public string? LienVen { get {  return lieuVen; } set { lieuVen = value; } }
        public string? RefLogiciel { get { return refLogiciel; } set { refLogiciel = value; } }
    }
}
