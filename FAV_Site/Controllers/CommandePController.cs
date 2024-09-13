using FAV_Site.Data;
using FAV_Site.Helper;
using FAV_Site.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;
using System.Security.Claims;
using System.Text;

namespace FAV_Site.Controllers
{
    public class CommandePController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IConfiguration configuration;
        private static string? prenom;
        private static double longitudeC;
        private static double latitudeC;
        private static string adresse;
        private static string idUserCon;
        public CommandePController(DataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
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
                    prenom = utilisateur.Prenom_ut;
                }
            }
            List<AffichePanCom> afficher = new();
            List<VendeurAvecCesProduit> vendeurAvecCesProduits = new();
            var toutPanierAcheteur = await dataContext.PanierModels.Where(p => p.id_acheteur.ToUpper().Equals(idUserConnected.ToUpper())).ToListAsync();
            foreach(var tp in toutPanierAcheteur)
            {
                if(vendeurAvecCesProduits.Count > 0)
                {
                    foreach(var vnv in vendeurAvecCesProduits)
                    {
                        if (vnv.IdVendeur.ToUpper().Equals(tp.id_vendeur.ToUpper()))
                        {
                            break;
                        }
                        if (!vnv.IdVendeur.ToUpper().Equals(tp.id_vendeur.ToUpper()))
                        {
                            Uti_vendeurModels vender = new Uti_vendeurModels();
                            vender = await dataContext.Vendeurs.FirstOrDefaultAsync(v => v.Id_uti_ven.ToString().ToUpper().Equals(tp.id_vendeur.ToUpper()));
                            VendeurAvecCesProduit tem = new();
                            tem.IdVendeur = vender.Id_uti_ven.ToString().ToUpper();
                            tem.nomVendeur = vender.Nom_Societe;
                            List<PanierModels> panier = new();
                            panier = toutPanierAcheteur.Where(i => i.id_vendeur.ToUpper().Equals(vender.Id_uti_ven.ToString().ToUpper())).ToList();
                            tem.panierModels = panier;
                            vendeurAvecCesProduits.Add(tem);
                            break;
                        }
                    }
                }
                if(vendeurAvecCesProduits.Count == 0)
                {
                    Uti_vendeurModels vender = new Uti_vendeurModels();
                    vender = await dataContext.Vendeurs.FirstOrDefaultAsync(v => v.Id_uti_ven.ToString().ToUpper().Equals(tp.id_vendeur.ToUpper()));
                    VendeurAvecCesProduit tem = new();
                    tem.IdVendeur = vender.Id_uti_ven.ToString().ToUpper();
                    tem.nomVendeur = vender.Nom_Societe;
                    List<PanierModels> panier = new();
                    panier = toutPanierAcheteur.Where(i => i.id_vendeur.ToUpper().Equals(vender.Id_uti_ven.ToString().ToUpper())).ToList();
                    tem.panierModels = panier;
                    vendeurAvecCesProduits.Add(tem);
                }
            }
            double totalP = 0;
            foreach (var aff in vendeurAvecCesProduits)
            {
                aff.prixChaqueVendeur = 0;
                AffichePanCom temps = new();
                temps.help = new List<HelperModel>();
                temps.nomVendeur = aff.nomVendeur;
                foreach (var h in aff.panierModels)
                {
                    HelperModel hm = new();
                    ProduitModels pro = new();
                    ImageModel im = new();
                    pro = await dataContext.Produits.FirstOrDefaultAsync(p => p.Id_produit.ToString().ToUpper().Equals(h.Id_produit.ToUpper()));
                    im = await dataContext.ImageProduit.FirstOrDefaultAsync(i => i.Id_produit.ToUpper().Equals(pro.Id_produit.ToString().ToUpper()));
                    hm.Produit = pro;
                    hm.Image = im;
                    hm.Prix = toutPanierAcheteur.FirstOrDefault(p => p.Id_produit.ToUpper().Equals(pro.Id_produit.ToString().ToUpper())).prix_total;
                    hm.quantite = toutPanierAcheteur.FirstOrDefault(q => q.Id_produit.ToUpper().Equals(pro.Id_produit.ToString().ToUpper())).quantite;
                    aff.prixChaqueVendeur += hm.Prix;
                    temps.help.Add(hm);
                }
                totalP += aff.prixChaqueVendeur;
                temps.prixChaqueVendeur = aff.prixChaqueVendeur;
                afficher.Add(temps);
                
            }
            ViewData["prixTotaux"] = totalP;
            return View(afficher);
        }
        public IActionResult LieuLivraison()
        {
            ViewBag.GoogleMapsApiKey = configuration["GoogleMaps:ApiKey"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SavePosition([FromBody] PositionPerModels model)
        {
            UtilisateurModels utilisateur = new();
            string idUserConnected = string.Empty;
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                var claimRecherche = ((ClaimsIdentity)claimsPrincipal.Identity).FindFirst(ClaimTypes.Name);
                if (claimRecherche != null)
                {
                    idUserConnected = claimRecherche.Value;
                    utilisateur = await dataContext.Utilisateurs.FirstOrDefaultAsync(p => p.Id_ut.ToString().ToUpper().Equals(idUserConnected.ToUpper()));
                    idUserCon = idUserConnected.ToUpper();
                }
            }
            if (model == null || string.IsNullOrEmpty(model.Nom))
            {
                return BadRequest("Invalid data.");
            }
            longitudeC = model.Longitude;
            latitudeC = model.Latitude;
            adresse = model.Nom;

            return Ok(new { redirectUrl = Url.Action("AttentPayment") });
        }
        [HttpGet]
        public async Task<IActionResult> AttentPayment()
        {
            StringBuilder AScanner = new();
            var toutPanier = await dataContext.PanierModels.Where(p => p.id_acheteur.ToUpper().Equals(idUserCon)).ToListAsync();
            var vendeur = new List<Uti_vendeurModels>();
            foreach (var tout in toutPanier)
            {

                if (vendeur.Count > 0)
                {
                    foreach (var v in vendeur)
                    {
                        if (v.Id_uti_ven.ToString().ToUpper().Equals(tout.id_vendeur.ToUpper()))
                        {
                            break;
                        }
                        if (!v.Id_uti_ven.ToString().ToUpper().Equals(tout.id_vendeur.ToUpper()))
                        {
                            Uti_vendeurModels ve = new();
                            ve = await dataContext.Vendeurs.FirstOrDefaultAsync(v => v.Id_uti_ven.ToString().ToUpper().Equals(tout.id_vendeur.ToUpper()));
                            vendeur.Add(ve);
                            break;
                        }
                    }
                }
                if (vendeur.Count == 0)
                {
                    Uti_vendeurModels ve = new();
                    ve = await dataContext.Vendeurs.FirstOrDefaultAsync(v => v.Id_uti_ven.ToString().ToUpper().Equals(tout.id_vendeur.ToUpper()));
                    vendeur.Add(ve);
                }
            }
            foreach(var leQr in vendeur)
            {
                var mesPanier = toutPanier.Where(p => p.id_vendeur.ToUpper().Equals(leQr.Id_uti_ven.ToString().ToUpper()));
                double prix = 0;
                foreach(var it in mesPanier)
                {
                    prix += it.prix_total;
                }
                UtilisateurModels us = new();
                us = await dataContext.Utilisateurs.FirstOrDefaultAsync(v => v.Id_ut.ToString().ToUpper().Equals(leQr.Id_uti.ToUpper()));
                AScanner.Append(us.Contact_ut + ";" + prix + "/");
            }
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(AScanner.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20); // Taille de 20 pixels par module
            PaymentModels pay = new();
            // Convertir l'image Bitmap en tableau d'octets pour l'enregistrement
            using (var ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                pay.QrCodePay = ms.ToArray();
                byte[] imageQr = pay.QrCodePay;
                string base64String_1 = Convert.ToBase64String(imageQr);
                pay.QrCodePayLoc = string.Format("data:image/png;base64,{0}", base64String_1);
            }
            return View(pay);
        }
        public async Task<IActionResult> Payement(string paymentMethod)
        {
            string enAttend = paymentMethod;
            var toutPanier = await dataContext.PanierModels.Where(p => p.id_acheteur.ToUpper().Equals(idUserCon)).ToListAsync();
            var CmdSave = new List<CommandeModels>();
            var vendeur = new List<Uti_vendeurModels>();
            //Insertion des commandes avec plusieur triages par vendeur et par produit
            foreach (var tout in toutPanier)
            {
                if(vendeur.Count > 0)
                {
                    foreach(var ven in vendeur)
                    {
                        if (ven.Id_uti_ven.ToString().ToUpper().Equals(tout.id_vendeur.ToUpper()))
                        {
                            break;
                        }
                        if (!ven.Id_uti_ven.ToString().ToUpper().Equals(tout.id_vendeur.ToUpper()))
                        {
                            CommandeModels commande = new CommandeModels();
                            Uti_vendeurModels vend = new();
                            vend = await dataContext.Vendeurs.FirstOrDefaultAsync(v => v.Id_uti_ven.ToString().ToUpper().Equals(tout.id_vendeur.ToUpper()));
                            vendeur.Add(vend);
                            commande.Id_acheteur = idUserCon;
                            commande.Id_vendeur = vend.Id_uti_ven.ToString().ToUpper();
                            commande.longitude = longitudeC;
                            commande.latitude = latitudeC;
                            commande.Lieu = adresse;
                            commande.lesIdProduit = "";
                            commande.quantite = "";
                            commande.prixTotal = 0;
                            commande.Date_pub = DateTime.Now;
                            var produitVend = toutPanier.Where(p => p.id_vendeur.ToUpper().Equals(ven.Id_uti_ven.ToString().ToUpper())).ToList();
                            foreach (var pro in produitVend)
                            {
                                commande.quantite += pro.quantite + ":";
                                commande.lesIdProduit += pro.Id_produit + ":";
                                commande.prixTotal += pro.prix_total;
                            }
                            CmdSave.Add(commande);
                            HistoriqueModels historique = new();
                            historique.Id_acheteur = idUserCon.ToUpper();
                            historique.Id_vendeur = commande.Id_vendeur;
                            historique.les_id_produit = commande.lesIdProduit.ToUpper().Substring(0,commande.lesIdProduit.Length - 1);
                            historique.les_quantite = commande.quantite.ToUpper().Substring(0, commande.quantite.Length - 1);
                            historique.Prix_a_payser = commande.prixTotal;
                            await dataContext.Historiques.AddAsync(historique);
                            break;
                        }
                    }
                }
                if(vendeur.Count == 0)
                {
                    CommandeModels commande = new CommandeModels();
                    Uti_vendeurModels vend = new();
                    vend = await dataContext.Vendeurs.FirstOrDefaultAsync(v => v.Id_uti_ven.ToString().ToUpper().Equals(tout.id_vendeur.ToUpper()));
                    vendeur.Add(vend);
                    commande.Id_acheteur = idUserCon;
                    commande.Id_vendeur = vend.Id_uti_ven.ToString().ToUpper();
                    commande.longitude = longitudeC;
                    commande.Date_pub = DateTime.Now;
                    commande.latitude = latitudeC;
                    commande.Lieu = adresse;
                    commande.lesIdProduit = "";
                    commande.quantite = "";
                    commande.prixTotal = 0;
                    var produitVend = toutPanier.Where(p => p.id_vendeur.ToUpper().Equals(vend.Id_uti_ven.ToString().ToUpper())).ToList();
                    foreach(var pro in produitVend)
                    {
                        commande.quantite += pro.quantite + ":";
                        commande.lesIdProduit += pro.Id_produit + ":";
                        commande.prixTotal += pro.prix_total;
                    }
                    CmdSave.Add(commande);
                    HistoriqueModels historique = new();
                    historique.Id_acheteur = idUserCon.ToUpper();
                    historique.Id_vendeur = commande.Id_vendeur;
                    historique.les_id_produit = commande.lesIdProduit.ToUpper().Substring(0, commande.lesIdProduit.Length - 1);
                    historique.les_quantite = commande.quantite.ToUpper().Substring(0, commande.quantite.Length - 1);
                    historique.Prix_a_payser = commande.prixTotal;
                    historique.Date_achat = DateTime.Now;
                    await dataContext.Historiques.AddAsync(historique);
                }
            }
            //Gestion des produit et ajout du commende dans le donné
            foreach(var item in CmdSave)
            {
                var Vendeur = await dataContext.Vendeurs.FirstOrDefaultAsync(v => v.Id_uti_ven.ToString().ToUpper().Equals(item.Id_vendeur.ToUpper()));
                var Utililisateur = await dataContext.Utilisateurs.FirstOrDefaultAsync(v => v.Id_ut.ToString().ToUpper().Equals(Vendeur.Id_uti.ToUpper()));
               
                await dataContext.Commandes.AddAsync(item);
                StatMois sta = new StatMois();
                sta.id_produit = item.lesIdProduit.ToUpper();
                string dateNow = DateTime.Now.ToString().Substring(3, 2);
                string toutQuant = item.quantite.Substring(0, item.quantite.Length - 1);
                string toutPro = item.lesIdProduit.Substring(0, item.lesIdProduit.Length - 1);
                string[] p = toutPro.Split(':');
                string[] q = toutQuant.Split(':');
                //lancement des id de produit et chaque quantite en parallele
                for(int i = 0; i < q.Length; i++)
                {
                    int qu = int.Parse(q[i]);
                    string idPro = p[i].ToUpper();
                    ProduitModels produit = await dataContext.Produits.FirstOrDefaultAsync(p => p.Id_produit.ToString().ToUpper().Equals(idPro));
                    if(produit != null)
                    {
                        produit.Nb_produit_reste = produit.Nb_produit_reste - qu;
                        produit.Nb_produit_vendu = produit.Nb_total_prod - produit.Nb_produit_reste;
                    }                    
                    // incrémentation du nb produit par mois en verifiant le mois
                    switch (dateNow)
                    {
                        case "01":
                            sta.jan += qu; break;
                        case "02":
                            sta.fev += qu; break;
                        case "03":
                            sta.mar += qu; break;
                        case "04":
                            sta.avr += qu; break;
                        case "05":
                            sta.mai += qu; break;
                        case "06":
                            sta.jui += qu; break;
                        case "07":
                            sta.juill += qu; break;
                        case "08":
                            sta.aou += qu; break;
                        case "09":
                            sta.sep += qu; break;
                        case "10":
                            sta.oct += qu; break;
                        case "11":
                            sta.nov += qu; break;
                        case "12":
                            sta.dec += qu; break;
                        default:
                            break;
                    }
                }
                
                
            }
            //suppression du panier
            foreach(var item in vendeur)
            {
                var pan = await dataContext.PanierModels.Where(p => p.id_vendeur.ToUpper().Equals(item.Id_uti_ven.ToString().ToUpper())).ToListAsync();
                foreach(var pp in pan)
                {
                    var panOn = await dataContext.PanierModels.FirstOrDefaultAsync(p => p.Id.ToString().ToUpper().Equals(pp.Id.ToString().ToUpper()));
                    dataContext.PanierModels.Remove(panOn);
                }
            }

            //Sauvegarde de tout ca dans la SqlServer
            await dataContext.SaveChangesAsync();
            return RedirectToAction("PayementReussir");
        }

        public IActionResult PayementReussir()
        {
            ViewData["prenom"] = prenom;
            return View();
        }
    }
}
