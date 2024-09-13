using FAV_Site.Data;
using FAV_Site.Helper;
using FAV_Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FAV_Site.Controllers
{
    public class AccueilController : Controller
    {
        private readonly DataContext? dataContext;
        private static string? msgQunatite;
        private static ProdAndPub? prodAndPub;
        private static List<ProduitModels>? produits;
        public AccueilController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["isConnectedOrNo"] = false;
            UtilisateurModels utilisateur = new();
            string idUserConnected = string.Empty;
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                ViewData["isConnectedOrNo"] = true;
                var claimRecherche = ((ClaimsIdentity)claimsPrincipal.Identity).FindFirst(ClaimTypes.Name);
                if (claimRecherche != null)
                {
                    idUserConnected = claimRecherche.Value;
                    utilisateur = await dataContext.Utilisateurs.FirstOrDefaultAsync(p => p.Id_ut.ToString().ToUpper().Equals(idUserConnected.ToUpper()));
                    ViewData["Nom"] = utilisateur.Nom_ut;
                    ViewData["image"] = utilisateur.ImgLocation;
                }
            }

            List<HelperModel> helperModel1 = new();
            List<HelperModel> NewArrivagehelperModel = new();
            List<HelperModel> NewElechelperModel = new();
            List<HelperModel> NewVehicuhelperModel = new();

            
            if (dataContext != null)
            {
                int countV = 1;
                int countE = 1;
                int countVeh = 1;
                produits = new();
                produits = await dataContext.Produits.OrderByDescending(p => p.Date_pub).ToListAsync();
                foreach(var produit in produits)
                {
                    HelperModel one = new();
                    var img = await dataContext.ImageProduit.FirstOrDefaultAsync(i => i.Id_produit.ToUpper().Equals(produit.Id_produit.ToString().ToUpper()));
                    one.Produit = produit;
                    one.Image = img;
                    
                    if (produit.Categorie.Equals("Vêtement"))
                    {
                        if (countV <= 8)
                        {
                            NewArrivagehelperModel.Add(one);
                            countV++;
                        }
                    }
                    if (produit.Categorie.Equals("Electronique"))
                    {
                        if(countE <= 8)
                        {
                            NewElechelperModel.Add(one); countE++;
                        }
                    }
                    if (produit.Categorie.Equals("Véhicules"))
                    {
                        if(countVeh <= 8)
                        {
                            NewVehicuhelperModel.Add(one); countVeh++;
                        }
                    }
                    helperModel1.Add(one);
                }
                List<PubliciteModels> pubs = new();
                var toutPub = await dataContext.Publicites.ToListAsync();
                pubs = toutPub;

                prodAndPub = new();
                //nouvvele arrivage
                prodAndPub.NewVehiculhelperModels = NewVehicuhelperModel;
                prodAndPub.NewElectroniquehelperModels = NewElechelperModel;
                prodAndPub.NewArrivagehelperModels = NewArrivagehelperModel;
                prodAndPub.helperModels = helperModel1;
                //tout les produit
                prodAndPub.publicites = pubs;
            }
            return View(prodAndPub);
        }

        //View Produit
        [HttpGet]
        public async Task<IActionResult> ViewProduct(string id)
        {

            UtilisateurModels utilisateurss = new();

            ComProd commentaire = new();
            
            string idUserConnected = string.Empty;
            ViewData["msgQ"] = msgQunatite;
            ViewData["isConnectedOrNo"] = false;
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                commentaire.recIDProdAndCommentaire = new RecIDProdAndCommentaire();
                ViewData["isConnectedOrNo"] = true;
                var claimsPrincipal1 = HttpContext.User as ClaimsPrincipal;
                if (claimsPrincipal != null)
                {
                    var claimRecherche = ((ClaimsIdentity)claimsPrincipal1.Identity).FindFirst(ClaimTypes.Name);
                    if (claimRecherche != null)
                    {
                        idUserConnected = claimRecherche.Value;
                        utilisateurss = await dataContext.Utilisateurs.FirstOrDefaultAsync(p => p.Id_ut.ToString().ToUpper().Equals(idUserConnected.ToUpper()));
                        ViewData["Nom"] = utilisateurss.Nom_ut;
                        ViewData["image"] = utilisateurss.ImgLocation;
                    }
                }
                commentaire.utilisateurConnected = await dataContext.Utilisateurs.FirstOrDefaultAsync(u => u.Id_ut.ToString().ToUpper().Equals(idUserConnected.ToUpper()));

            }
            
            commentaire.helperModel = new HelperModel();
            commentaire.utilisateurEtVendeur = new UtilisateurEtVendeur();
            string Id = id.ToUpper();
            var produit = await dataContext.Produits.FirstOrDefaultAsync(p => p.Id_produit.ToString().ToUpper().Equals(Id));
            var image = await dataContext.ImageProduit.FirstOrDefaultAsync(i => i.Id_produit.ToUpper().Equals(Id));
            var vendeur = await dataContext.Vendeurs.FirstOrDefaultAsync(p => p.Id_uti_ven.ToString().ToUpper().Equals(produit.Id_vendeur.ToUpper()));
            var utilisateur = await dataContext.Utilisateurs.FirstOrDefaultAsync(u => u.Id_ut.ToString().ToUpper().Equals(produit.Id_utilisateur.ToUpper()));
            var produitDesciVraque = produit.Autre_description.Substring(0, produit.Autre_description.Length - 3);
            string[] chaqueDescri = produitDesciVraque.Split('/');
            int i = 0;
            foreach (var item in chaqueDescri)
            {
                string[] valEtTitr = item.Split(':');
                DescriptionPreci des = new();
                des.titre = valEtTitr[0];
                des.valeur = valEtTitr[1];
                i++;
                commentaire.descriptionPrecis.Add(des);
            }
            var commentaireBrute = await dataContext.Commentaires.Where(p => p.Id_produit.ToUpper().Equals(id.ToUpper())).ToListAsync();
            if(commentaireBrute is not null)
            {
                commentaire.helperModel.Produit = new ProduitModels();
                commentaire.helperModel.Produit = produit;
                commentaire.helperModel.Image = image;
                commentaire.utilisateurEtVendeur.utilisateur = utilisateur;
                commentaire.utilisateurEtVendeur.vendeur = vendeur;
                commentaire.listAcheteurCommentaire = new List<AchteurCommentaire>();
                foreach(var com in commentaireBrute)
                {
                    AchteurCommentaire ac = new();
                    ac.commentaire = com;
                    var achetaire = await dataContext.Utilisateurs.FirstOrDefaultAsync(u => u.Id_ut.ToString().ToUpper().Equals(com.Id_utilisateur));
                    ac.acheteur = achetaire;
                    commentaire.listAcheteurCommentaire.Add(ac);
                }
                return View(commentaire);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RecCommentaire(string idProd, string com)
        {
            UtilisateurModels utilisateur = new();
            ViewData["isConnectedOrNo"] = false;
            string idP = idProd.ToUpper();
            string idUserConnected = string.Empty;
            bool validation = false;
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                ViewData["isConnectedOrNo"] = true;
                validation = true;
                var claimsPrincipal1 = HttpContext.User as ClaimsPrincipal;
                if (claimsPrincipal != null)
                {
                    var claimRecherche = ((ClaimsIdentity)claimsPrincipal1.Identity).FindFirst(ClaimTypes.Name);
                    if (claimRecherche != null)
                    {
                        idUserConnected = claimRecherche.Value;
                        utilisateur = await dataContext.Utilisateurs.FirstOrDefaultAsync(p => p.Id_ut.ToString().ToUpper().Equals(idUserConnected.ToUpper()));
                        ViewData["Nom"] = utilisateur.Nom_ut;
                        ViewData["image"] = utilisateur.ImgLocation;
                    }
                }
            }
            if(validation)
            {
                if(idProd !=  string.Empty && com != string.Empty)
                {
                    var commentaireSave = new CommentaireModels();
                    commentaireSave.Id_utilisateur = idUserConnected.ToUpper();
                    commentaireSave.Id_produit = idP;
                    commentaireSave.Commentaire = com;
                    await dataContext.Commentaires.AddAsync(commentaireSave);
                    await dataContext.SaveChangesAsync();
                    
                    return RedirectToAction("ViewProduct", new {id = idP});
                }
            }
            return RedirectToAction("Index", "Login");
        }
        public async  Task<IActionResult> MonPanier()
        {
            UtilisateurModels utilisateur = new();
            string idUserConnected = string.Empty;
            bool isConnect = false;
            ViewData["isConnectedOrNo"] = false;
            var claimsPrincipal1 = HttpContext.User as ClaimsPrincipal;
            if (claimsPrincipal1 != null)
            {
                ViewData["isConnectedOrNo"] = true;
                var claimRecherche = ((ClaimsIdentity)claimsPrincipal1.Identity).FindFirst(ClaimTypes.Name);
                if (claimRecherche != null)
                {
                    idUserConnected = claimRecherche.Value;
                    isConnect = true;
                    utilisateur = await dataContext.Utilisateurs.FirstOrDefaultAsync(p => p.Id_ut.ToString().ToUpper().Equals(idUserConnected.ToUpper()));
                    ViewData["Nom"] = utilisateur.Nom_ut;
                    ViewData["image"] = utilisateur.ImgLocation;
                }
            }

            if (isConnect)
            {
                var MonPanier = await dataContext.PanierModels.Where(p => p.id_acheteur.ToUpper().Equals(idUserConnected.ToUpper())).ToListAsync();
                List<HelpPanier> listeHelpPanier = new();
                foreach(var item in MonPanier)
                {
                    HelpPanier h = new();
                    var produit = await dataContext.Produits.FirstOrDefaultAsync(p => p.Id_produit.ToString().ToUpper().Equals(item.Id_produit.ToUpper()));
                    if(produit != null)
                    {
                        var image = await dataContext.ImageProduit.FirstOrDefaultAsync(i => i.Id_produit.ToUpper().Equals(produit.Id_produit.ToString().ToUpper()));
                        h.Produit = produit;
                        h.Image = image;
                        h.Quantite = item.quantite;
                        h.PrixAPayer = produit.Prix * item.quantite;
                        listeHelpPanier.Add(h);

                        
                    }
                }
                return View(listeHelpPanier);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> VoirPanier(int quantite, string idProduit)
        {
            string idUserConnected = string.Empty;
            bool isConnect = false;
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                var claimsPrincipal1 = HttpContext.User as ClaimsPrincipal;
                if (claimsPrincipal != null)
                {
                    isConnect = true;
                    var claimRecherche = ((ClaimsIdentity)claimsPrincipal1.Identity).FindFirst(ClaimTypes.Name);
                    if (claimRecherche != null)
                    {
                        idUserConnected = claimRecherche.Value;
                    }
                }

            }
            if (isConnect)
            {
                if (quantite > 0 && idProduit != null && idProduit != string.Empty)
                {
                    var produit = await dataContext.Produits.FirstOrDefaultAsync(p => p.Id_produit.ToString().ToUpper().Equals(idProduit.ToUpper()));
                    if (produit != null)
                    {
                        int nbTotalProd = produit.Nb_total_prod - quantite;
                        if (nbTotalProd > 0)
                        {
                            PanierModels panier = new();
                            panier.Id_produit = idProduit.ToUpper();
                            panier.id_acheteur = idUserConnected.ToUpper();
                            panier.id_vendeur = produit.Id_vendeur.ToUpper();
                            panier.quantite = quantite;
                            panier.prix_total = produit.Prix * quantite;
                            msgQunatite = string.Empty;
                            await dataContext.PanierModels.AddAsync(panier);
                            await dataContext.SaveChangesAsync();

                            return RedirectToAction("MonPanier");
                        }
                        else
                        {
                            msgQunatite = "Le nombre de produit éxiste est insuffisant pour le moment";
                            return RedirectToAction("ViewProduct", new { id = idProduit.ToUpper() });
                        }
                    }
                }
            }
            return RedirectToAction("Index","Login");
        }
        public async Task<IActionResult> Header()
        {
            string idUserConnected = string.Empty;
            ViewData["isConnectedOrNo"] = false;
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var utilisateur = new UtilisateurModels();
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                ViewData["isConnectedOrNo"] = true;
                var claimRecherche = ((ClaimsIdentity)claimsPrincipal.Identity).FindFirst(ClaimTypes.Name);
                if (claimRecherche != null)
                {
                    idUserConnected = claimRecherche.Value;
                    utilisateur = await dataContext.Utilisateurs.FirstOrDefaultAsync(p => p.Id_ut.ToString().ToUpper().Equals(idUserConnected.ToUpper()));
                }
            }
            ViewData["Nom"] = utilisateur.Nom_ut;
            ViewData["image"] = utilisateur.ImgLocation;
            return View();
        }

        public async Task<IActionResult> Recherche(string search)
        {
            ViewData["isConnectedOrNo"] = false;
            UtilisateurModels utilisateur = new();
            string idUserConnected = string.Empty;
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                ViewData["isConnectedOrNo"] = true;
                var claimRecherche = ((ClaimsIdentity)claimsPrincipal.Identity).FindFirst(ClaimTypes.Name);
                if (claimRecherche != null)
                {
                    idUserConnected = claimRecherche.Value;
                    utilisateur = await dataContext.Utilisateurs.FirstOrDefaultAsync(p => p.Id_ut.ToString().ToUpper().Equals(idUserConnected.ToUpper()));
                    ViewData["Nom"] = utilisateur.Nom_ut;
                    ViewData["image"] = utilisateur.ImgLocation;
                }
            }


            var prodSearch = new ProdAndPub();
            prodSearch.helperModels = prodAndPub.helperModels.Where(p =>
            p.Produit.Nom_produit.Contains(search.Trim()) ||
            p.Produit.Marque.Contains(search.Trim())|| 
            p.Produit.Description_produit.Contains(search.Trim())).ToList();
            prodSearch.NewVehiculhelperModels = prodAndPub.NewVehiculhelperModels.Where(p =>
            p.Produit.Nom_produit.Contains(search.Trim()) ||
            p.Produit.Marque.Contains(search.Trim()) ||
            p.Produit.Description_produit.Contains(search.Trim())).ToList();
            prodSearch.NewElectroniquehelperModels = prodAndPub.NewElectroniquehelperModels.Where(p =>
            p.Produit.Nom_produit.Contains(search.Trim()) ||
            p.Produit.Marque.Contains(search.Trim()) ||
            p.Produit.Description_produit.Contains(search.Trim())).ToList();
            prodSearch.NewArrivagehelperModels = prodAndPub.NewArrivagehelperModels.Where(p =>
            p.Produit.Nom_produit.Contains(search.Trim()) ||
            p.Produit.Marque.Contains(search.Trim()) ||
            p.Produit.Description_produit.Contains(search.Trim())).ToList();

            prodSearch.publicites = prodAndPub.publicites.Where(p => 
            p.Nom_pub.Contains(search.Trim()) || p.Descri_pub.Contains(search.Trim())).ToList();
            return View(prodSearch);
        }
    }

}
