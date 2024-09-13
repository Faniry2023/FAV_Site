using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAV_Site.Models
{
    public class ProduitModels
    {
        [Key]
        public Guid Id_produit {  get; set; }
        private string? id_vendeur;
        private string? id_utilisateur;
        private string? nom_produit;
        private string? marque;
        private string? description_produit;
        public DateTime? Date_pub { get; set; }
        private int nb_produit_reste;
        private int nb_produit_vendu;
        private int nbTotalProd;
        private double prix;
        private double val_prix_promo;
        private double prix_promo;
        private string? autre_description;
        private string? type;
        private string? categorie;
        private string? id_client_prix_promo;
        private bool promotion;

        public ProduitModels() { }

        public ProduitModels(string? id_vendeur, string? id_utilisateur, int nb_produit_vendu, string? nom_produit, string? marque, string? description_produit,int nb_produit_reste, int nbTotalProd, double val_prix_promo, double prix, double prix_promo, string? type, string? autre_description, string? categorie, bool promotion, string? id_client_prix_promo)
        {
            this.id_vendeur = id_vendeur;
            this.id_utilisateur = id_utilisateur;
            this.nom_produit = nom_produit;
            this.marque = marque;
            this.description_produit = description_produit;
            this.nb_produit_reste = nb_produit_reste;
            this.nbTotalProd = nbTotalProd;
            this.val_prix_promo = val_prix_promo;
            this.prix = prix;
            this.prix_promo = prix_promo;
            this.autre_description = autre_description;
            this.type = type;
            this.nb_produit_vendu = nb_produit_vendu;
            this.id_client_prix_promo = id_client_prix_promo;
            this.categorie = categorie;
            this.promotion = promotion;
        }

        public string? Id_vendeur { get {  return id_vendeur; } set { id_vendeur = value; } }
        public string? Id_utilisateur { get { return id_utilisateur; } set { id_utilisateur= value; } }
        public string? Nom_produit { get { return nom_produit; } set { nom_produit = value; }}
        public string? Marque { get { return marque; } set { marque = value; } }
        public string? Description_produit { get { return description_produit; } set { description_produit = value; } }
        public int Nb_produit_reste { get {  return nb_produit_reste;} set { nb_produit_reste= value; } }
        public int Nb_produit_vendu { get { return nb_produit_vendu; } set { nb_produit_vendu = value; } }
        public int Nb_total_prod { get { return nbTotalProd; } set { nbTotalProd = value; } }
        public double Val_prix_promo { get { return val_prix_promo; } set { val_prix_promo = value; } }
        public double Prix { get { return prix; } set {  prix = value; } }
        public double Prix_promo { get { return prix_promo; } set { prix_promo = value; } }
        public string? Autre_description { get {  return autre_description; } set {  autre_description = value; } }
        public string? Type { get { return type; } set {  type = value; } }
        public string? Categorie { get { return categorie; } set {  categorie = value; } }
        public string? Id_client_prix_promo { get { return id_client_prix_promo; } set { id_client_prix_promo = value; } }
        public bool Promotion { get { return promotion; } set { promotion = value; } }
    }
}
