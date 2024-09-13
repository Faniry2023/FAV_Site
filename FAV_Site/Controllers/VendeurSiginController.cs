using FAV_Site.Data;
using FAV_Site.GmailServicesHelp;
using FAV_Site.Helper;
using FAV_Site.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FAV_Site.Controllers
{
    public class VendeurSiginController : Controller
    {
        private readonly DataContext? dataContext;
        private static UtilisateurModels? utilisateurModel;
        private static Log_UtilisateurModels? log_utilisateurModel;
        private static Uti_vendeurModels? uti_VendeurModels;
        private static string? CodeConfirme;
        private static string? refLogi;
        public VendeurSiginController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<IActionResult> Index()
        {
            if(TempData["use"] is string utilisateurJson && TempData["log"] is string loginJson && TempData["path"] is string photoPath)
            {
                if(utilisateurJson != null && loginJson != null)
                {
                    //récupération des données temporaires via Page d'inscription
                    UtilisateurModels uti = JsonConvert.DeserializeObject<UtilisateurModels>(utilisateurJson);
                    Log_UtilisateurModels logUs = JsonConvert.DeserializeObject<Log_UtilisateurModels>(loginJson);
                    var path = JsonConvert.DeserializeObject<string>(photoPath);
                    if (System.IO.File.Exists(path))
                    {
                        uti.Photo = await System.IO.File.ReadAllBytesAsync(path);
                        System.IO.File.Delete(path); // Supprimer le fichier temporaire après utilisation
                    }
                    uti.ImgLocation = Changement.ByteToImageLocation(uti.Photo);
                    utilisateurModel = uti;
                    log_utilisateurModel = logUs;
                    //generate reflogiciel
                    string valeur = string.Empty;
                    var vendeur = await dataContext.Vendeurs.ToListAsync();
                    bool isAccept = true;
                    do
                    {
                        isAccept = true;
                        GenerateRef reflog = new();
                        valeur = reflog.CodeReferenceLogicie();
                        foreach (var item in vendeur)
                        {
                            if (item.RefLogiciel.Equals(valeur))
                            {
                                valeur = string.Empty;
                                isAccept = false;
                                break;
                            }
                        }
                    } while (!isAccept);
                    refLogi = valeur;
                    //fin generate ref logiciel
                    ViewData["refe"] = refLogi;
                    ViewData["prenom"] = uti.Prenom_ut;
                    return View();
                }
                
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(Uti_vendeurModels vendeur)
        {
            
            if (vendeur != null)
            {
                var vend = vendeur;
                vend.RefLogiciel = refLogi;
                uti_VendeurModels = vend;
                /*##################_A_SUPPRIMER POUR UTILISER EMAIL_####################################*/
                
                await dataContext.LoginUtilisateur.AddAsync(log_utilisateurModel);
                utilisateurModel.Id_ad = log_utilisateurModel.Id_log.ToString().ToUpper();
                await dataContext.Utilisateurs.AddAsync(utilisateurModel);
                vend.Id_uti = utilisateurModel.Id_ut.ToString().ToUpper();
                vend.Id_ad = log_utilisateurModel.Id_log.ToString().ToUpper();
                await dataContext.Vendeurs.AddAsync(vend);

                await dataContext.SaveChangesAsync();
                return RedirectToAction("Reussir");
                
                /*###################################_FIN SUPPRESSION_##########################*/

                //decomenter pour utiliser l'email
                /*
                CodeConfirme = CodeDeConfirmation.MyCode();
                var emailService = new SmtpEmailService();
                string recipientAddress = log_utilisateurModel.Email; // Remplacez par l'adresse email du destinataire
                string subject = "Code de confirmation";
                string body = $"Bonjour {utilisateurModel.Prenom_ut} . Votre code de confirmation est {CodeConfirme}"; // Contenu de l'email
                await emailService.SendEmailAsync(recipientAddress, subject, body);
                return RedirectToAction("CodeConfirmePage");   */
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult CodeConfirmePage()
        {
            ViewData["error"] = string.Empty;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CodeConfirmePage(RecCodeConf mycode)
        {
            if (mycode != null)
            {
                string mycodeconfirme = mycode.Nb1.ToString() + mycode.Nb2.ToString() + mycode.Nb3.ToString() + mycode.Nb4.ToString() + mycode.Nb5.ToString() + mycode.Nb6.ToString() + mycode.Nb7.ToString();
                if (mycodeconfirme.Equals(CodeConfirme))
                {
                    UtilisateurModels userModel = new();
                    Log_UtilisateurModels userLogModel = new();
                    Uti_vendeurModels ven = new();
                    userLogModel = log_utilisateurModel;
                    await dataContext.LoginUtilisateur.AddAsync(userLogModel);
                    utilisateurModel.Id_ad = userLogModel.Id_log.ToString().ToUpper();
                    userModel = utilisateurModel;
                    await dataContext.Utilisateurs.AddAsync(userModel);
                    ven = uti_VendeurModels;
                    ven.Id_uti = userModel.Id_ut.ToString().ToUpper();
                    ven.Id_ad = userLogModel.Id_log.ToString().ToUpper();
                    await dataContext.Vendeurs.AddAsync(ven);
                    await dataContext.SaveChangesAsync();
                    return RedirectToAction("Reussir");
                }
                else
                {
                    ViewData["error"] = "Code de Confirmation incorrecte";
                    return View();
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Reussir()
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
            if (utilisateurModel.Type_ut != null || utilisateurModel.Type_ut != string.Empty)
            {
                ViewData["message"] = utilisateurModel.Type_ut;
            }
            
            return View();
        }
    }
}
