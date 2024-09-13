using FAV_Site.Data;
using FAV_Site.Helper;
using FAV_Site.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using FAV_Site.GmailServicesHelp;

namespace FAV_Site.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataContext dataContext;
        private static Log_UtilisateurModels? loginModel;
        private static LoginAndRegister? LandR;
        private static string? CodeConfirme;
        public LoginController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        //premier affichage de la page de connexion
        public IActionResult Index()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Accueil");
            }
            return View();
        }

        //Insertion de nouveau utilisateur
        [HttpPost]
        public async Task<IActionResult> LogAndReg(LoginAndRegister loginAndRegister)
        {
            bool plus = false;
           if(loginAndRegister != null)
            {
                //loginAndRegister.Utilisateur.Photo = IformToByte.IFormFileToByte(loginAndRegister.Photo);
                if (loginAndRegister.Log_Utilisateur?.Password == loginAndRegister?.Confirme)
                {
                    if (loginAndRegister.Utilisateur?.Contact_ut?.Length > 4)
                    {
                        plus = true;
                        if (loginAndRegister?.Utilisateur?.Contact_ut?.Substring(0, 4) == "+261")
                        {
                            double contactTest = 0;
                            string contactString = loginAndRegister.Utilisateur.Contact_ut.Substring(1, loginAndRegister.Utilisateur.Contact_ut.Length - 1);
                            if (loginAndRegister.Utilisateur?.Contact_ut?.Length <= 13 && Double.TryParse(contactString, out contactTest))
                            {
                                if(loginAndRegister.Utilisateur != null)
                                {
                                    var logTest = await dataContext.LoginUtilisateur.ToListAsync();
                                    Log_UtilisateurModels usLo = new();
                                    bool val = false;
                                    foreach(var item in logTest)
                                    {
                                        if (item.Email.ToLower().Equals(loginAndRegister.Log_Utilisateur.Email.ToLower()))
                                        {
                                            val = true; break;
                                        }
                                    }
                                    if (val)
                                    {
                                        ViewData["msgEmail"] = "Cette email existe deja dans notre base de donné";
                                        return View("Index");
                                    }
                                    else
                                    {
                                        if (loginAndRegister.Utilisateur.Type_ut.Equals("vendeur"))
                                        {
                                            // Sauvegarde temporaire du fichier sur le serveur
                                            string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                                            using (var stream = System.IO.File.Create(tempFilePath))
                                            {
                                                await loginAndRegister.Photo.CopyToAsync(stream);
                                            }

                                            // Stocker le chemin dans le modèle
                                            loginAndRegister.PhotoPath = tempFilePath;
                                            TempData["log"] = JsonConvert.SerializeObject(loginAndRegister.Log_Utilisateur);
                                            TempData["use"] = JsonConvert.SerializeObject(loginAndRegister.Utilisateur);
                                            TempData["path"] = JsonConvert.SerializeObject(tempFilePath);
                                            return RedirectToAction("Index", "VendeurSigin");
                                        }
                                        Log_UtilisateurModels adminUser = new();
                                        adminUser.Email = loginAndRegister.Log_Utilisateur?.Email;
                                        adminUser.Password = loginAndRegister.Log_Utilisateur?.Password;
                                        await dataContext.LoginUtilisateur.AddAsync(adminUser);

                                        UtilisateurModels utilisateur = new();
                                        utilisateur.Id_ad = adminUser.Id_log.ToString().ToUpper();
                                        utilisateur.Nom_ut = loginAndRegister.Utilisateur?.Nom_ut;
                                        utilisateur.Prenom_ut = loginAndRegister.Utilisateur?.Prenom_ut;
                                        utilisateur.Contact_ut = loginAndRegister.Utilisateur?.Contact_ut;
                                        utilisateur.Pays_ut = loginAndRegister.Utilisateur?.Pays_ut;
                                        utilisateur.Ville_ut = loginAndRegister.Utilisateur?.Ville_ut;
                                        utilisateur.Nationnelite_ut = loginAndRegister.Utilisateur?.Nationnelite_ut;
                                        utilisateur.Type_ut = loginAndRegister.Utilisateur.Type_ut;
                                        utilisateur.Photo = await IformToByte.IFormFileToByte(loginAndRegister.Photo);
                                        utilisateur.ImgLocation = Changement.ByteToImageLocation(utilisateur.Photo);

                                        /*_SUPPROIMER POUR UTILISER EMAIL*/
                                        await dataContext.Utilisateurs.AddAsync(utilisateur);

                                        await dataContext.SaveChangesAsync();
                                        return RedirectToAction("Reussir");
                                        /*_FIN SUPPRESSION_*/



                                        /*############_DECOMENTER POUR UTILISER EMAIL_#############*/
                                        /*LandR = new();
                                        LandR.Utilisateur = new();
                                        LandR.Log_Utilisateur = new();
                                        LandR.Utilisateur = utilisateur;
                                        LandR.Log_Utilisateur = adminUser;
                                        CodeConfirme = CodeDeConfirmation.MyCode();
                                        var emailService = new SmtpEmailService();
                                        string recipientAddress = adminUser.Email; // Remplacez par l'adresse email du destinataire
                                        string subject = "Code de confirmation";
                                        string body = $"Votre code de confirmation est {CodeConfirme}"; // Contenu de l'email
                                        await emailService.SendEmailAsync(recipientAddress, subject, body);
                                        return RedirectToAction("CodeConfirmePage");*/
                                    }
                                }
                            }
                            else
                            {
                                ViewData["messageContact"] = "le champ contact devrait avoir au maximum 13 caracteres et des nombre";
                                ViewData["msgEmail"] = "";
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            ViewData["messageContactDebut"] = "Veuillez commencer par +261";
                            ViewData["msgEmail"] = "";
                            return RedirectToAction("Index");
                        }
                    }
                    if (!plus)
                    {
                        ViewData["messageContactDebut"] = "Veuillez remplir le champ contact";
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewData["msgEmail"] = "";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CodeConfirmePage()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CodeConfirmePage(RecCodeConf mycode)
        {
            if(mycode != null)
            {
                string mycodeconfirme = mycode.Nb1.ToString() + mycode.Nb2.ToString() + mycode.Nb3.ToString() + mycode.Nb4.ToString() + mycode.Nb5.ToString() + mycode.Nb6.ToString() + mycode.Nb7.ToString();
                if (mycodeconfirme.Equals(CodeConfirme))
                {
                    UtilisateurModels userModel = new();
                    Log_UtilisateurModels userLogModel = new();
                    userModel = LandR.Utilisateur;
                    userLogModel = LandR.Log_Utilisateur;
                    await dataContext.Utilisateurs.AddAsync(userModel);
                    await dataContext.LoginUtilisateur.AddAsync(userLogModel);
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

        [HttpPost]
        public async Task<IActionResult> LoginHere(LoginAndRegister logAndReg, string ReturnUrl)
        {
            if(logAndReg != null)
            {
                var userLoginTest = logAndReg.Log_Utilisateur;
                var toutUser = await dataContext.LoginUtilisateur.ToListAsync();
                var userLogin = new Log_UtilisateurModels();
                bool valEmail = false;
                bool valMDp = false;
                foreach(var item in toutUser)
                {
                    if(item != null)
                    {
                        if (item.Email.Equals(userLoginTest?.Email))
                        {
                            valEmail = true;
                        }
                        if (item.Password.Equals(userLoginTest?.Password))
                        {
                            valMDp = true;
                        }
                        if(valEmail && valMDp)
                        {
                            userLogin = item;
                            break;
                        }
                    }
                }
                if(valEmail && valMDp)
                {
                    var utilisateur = new UtilisateurModels();
                    utilisateur = await dataContext.Utilisateurs.FirstOrDefaultAsync(u => u.Id_ad.Equals(userLogin.Id_log.ToString()));
                    string idUserConnected = utilisateur.Id_ut.ToString();
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, idUserConnected),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims,"Login");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Accueil");
                }
            }
            return View("Index");
        }

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
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SendEmail()
        {
            var service = GmailServiceHelper.GetGmailService();
            string to = "tinafaniry0802@gmail.com";
            string subject = "Code confirmation";
            string bodyText = "votre code de confirmation est 14687";
            await GmailServiceHelper.SendEmailAsync(service, to, subject, bodyText);
            return Ok("Email sent successfully");
        }
    }
}
