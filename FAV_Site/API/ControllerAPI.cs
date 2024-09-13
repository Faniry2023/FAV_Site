using FAV_Site.Controllers;
using FAV_Site.Data;
using FAV_Site.Helper;
using FAV_Site.Migrations;
using FAV_Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAV_Site.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerAPI : ControllerBase
    {
        private readonly DataContext? dataContext;
        private static ProduitModels? produitModelTemp;
        public ControllerAPI(DataContext? dataContext)
        {
            this.dataContext = dataContext;

        }
        // GET: api/<ControllerAPI>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ControllerAPI>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //ajoutPhotoSurPub produit
        [HttpPost("test")]
        public async Task<ActionResult<ProduitModels>> test([FromBody] Log_UtilisateurModels produit)
        {
            dataContext.LoginUtilisateur.Add(produit);
            await dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduit), new { id = "produitModelTemp" });
        }
        //retoure produit
        [Route("GetProduitAdd/{prod}")]
        [HttpGet]
        public ActionResult<ProduitModels> GetProduitAdd(ProduitModels prod)
        {
            if (prod != null)
            {
                return prod;
            }
            return NotFound();
        }


        // POST api/<ControllerAPI>
        [HttpPost("PostAddNewProduct")]
        public async Task<ActionResult<ProduitModels>> PostAddNewProduct([FromBody] ProduitModels produit)
        {
            DateTime now = DateTime.Now;
            if (dataContext != null)
            {
                if (produit != null)
                {
                    produit.Date_pub = now;
                    produit.Nb_produit_reste = produit.Nb_total_prod;
                    produit.Nb_produit_vendu = 0;
                    StatMois mois = new()
                    {
                        jan = Rand(),
                        fev = Rand(),
                        mar = Rand(),
                        avr = Rand(),
                        mai = Rand(),
                        jui = Rand(),
                        juill = Rand(),
                        aou = Rand(),
                        sep = 0,
                        oct = 0,
                        nov = 0,
                        dec = 0,
                    };
                    await dataContext.AddAsync(produit);
                    mois.id_produit = produit.Id_produit.ToString().ToUpper();
                    await dataContext.Mois.AddAsync(mois);
                    produitModelTemp = produit;
                    await dataContext.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetProduit), new { id = produitModelTemp.Id_produit });
                }
            }
            return NotFound();

        }


        //Modifier Produit
        [HttpPost("PostModNewProduct")]
        public async Task<ActionResult<ProduitModels>> PostModNewProduct([FromBody] ProduitModels produit)
        {
            DateTime now = DateTime.Now;
            if (dataContext != null)
            {
                if(produit != null)
                {
                    var mesProduit = await dataContext.Produits.FirstOrDefaultAsync(p => p.Id_produit.ToString().ToUpper().Equals(produit.Id_produit.ToString().ToUpper()));
                    mesProduit.Promotion = produit.Promotion;
                    mesProduit.Nb_total_prod = produit.Nb_total_prod;
                    mesProduit.Prix_promo = produit.Prix_promo;
                    double prixTemps = 0;
                    prixTemps = produit.Prix;
                    double pourcentage = ((produit.Prix * produit.Prix_promo) / 100);
                    mesProduit.Prix = prixTemps - pourcentage;
                    mesProduit.Val_prix_promo = prixTemps;
                    dataContext.SaveChangesAsync();
                }
                return CreatedAtAction(nameof(GetProduit), new { id = produitModelTemp.Id_produit });
            }
            return NotFound();

        }



        public int Rand()
        {
            Random rand = new Random();
            return rand.Next(1, 150);
        }
        [Route("GetProduit/{id}")]
        [HttpGet]
        public async Task<ActionResult<ProduitModels>> GetProduit(string id)
        {
            if (dataContext is not null)
            {
                var produit = await dataContext.Produits.SingleOrDefaultAsync(p => p.Id_produit.ToString().Equals(id));
                if (produit != null)
                {
                    return produit;
                }
            }
            return NotFound();
        }

        [Route("GetAllProduits")]
        [HttpGet]
        public async Task<ActionResult<List<ProduitModels>>> GetAllProduits()
        {
            if (dataContext is not null)
            {
                var produit = await dataContext.Produits.ToListAsync();
                if (produit != null)
                {
                    return produit;
                }
            }
            return NotFound();
        }


        [HttpPost("PostAddNewImage")]
        public async Task<ActionResult<Image_produitModels>> PostAddNewImage([FromBody] Image_produitModels images)
        {
            ImageModel imageModel = new();
            if (images == null)
            {
                return BadRequest();
            }
            if (dataContext is not null)
            {
                imageModel.Id_produit = produitModelTemp.Id_produit.ToString().ToUpper();
                imageModel.ImageUrlCouv = ImageService.SaveImage(images.Image_couv, "prod_couv");
                imageModel.ImageUrl_1 = ImageService.SaveImage(images.Image_1, "prod_1");
                imageModel.ImageUrl_2 = ImageService.SaveImage(images.Image_2, "prod_2");
                imageModel.ImageUrl_3 = ImageService.SaveImage(images.Image_3, "prod_3");
                imageModel.ImageUrl_4 = ImageService.SaveImage(images.Image_4, "prod_4");
                await dataContext.ImageProduit.AddAsync(imageModel);
                if (dataContext is not null)
                {
                    await dataContext.SaveChangesAsync();
                }
            }
            return CreatedAtAction(nameof(GetProduit), new { id = imageModel.Id_produit.ToString() });
        }

        [Route("GetAllUser")]
        [HttpGet]
        public async Task<ActionResult<List<UtilisateurModels>>> GetAllUser()
        {
            if (dataContext is not null)
            {
                var utilisateur = await dataContext.Utilisateurs.ToListAsync();
                if (utilisateur != null)
                {
                    return utilisateur;
                }
            }
            return NotFound();
        }
        [Route("GetAllImage")]
        [HttpGet]
        public async Task<ActionResult<List<Image_produitModels>>> GetAllImage()
        {
            if (dataContext is not null)
            {
                List<Image_produitModels> image = new();
                var imageUrl = await dataContext.ImageProduit.ToListAsync();
                if(imageUrl != null)
                {
                    foreach(var imUrl in imageUrl)
                    {
                        Image_produitModels im = new();
                        im.Id_image = imUrl.Id.ToString().ToUpper();
                        im.Id_produit = imUrl.Id_produit;
                        im.Image_couv = ImageService.GetImageAsByteArray(imUrl.ImageUrlCouv);
                        im.Image_1 = ImageService.GetImageAsByteArray(imUrl.ImageUrl_1);
                        im.Image_2 = ImageService.GetImageAsByteArray(imUrl.ImageUrl_2);
                        im.Image_3 = ImageService.GetImageAsByteArray(imUrl.ImageUrl_3);
                        im.Image_4 = ImageService.GetImageAsByteArray(imUrl.ImageUrl_4);
                        image.Add(im);
                    }
                }
                if (image != null)
                {
                    return image;
                }
            }
            return NotFound();
        }
        [Route("GetAllUserVendeur")]
        [HttpGet]
        public async Task<ActionResult<List<Uti_vendeurModels>>> GetAllUserVendeur()
        {
            if (dataContext != null)
            {
                var listeVendeur = await dataContext.Vendeurs.ToListAsync();
                if (listeVendeur != null)
                {
                    return listeVendeur;
                }
            }
            return NotFound();
        }
        [Route("GetAllLoginUser")]
        [HttpGet]
        public async Task<ActionResult<List<Log_UtilisateurModels>>> GetAllLoginUser()
        {
            if (dataContext != null)
            {
                var listeLogin = await dataContext.LoginUtilisateur.ToListAsync();
                if (listeLogin != null)
                {
                    return listeLogin;
                }
            }
            return NotFound();
        }

        [HttpPost("AjoutPublicite")]
        public async Task<ActionResult> AjoutPublicite([FromBody] PubliciteModels pub)
        {
            if (pub == null)
            {
                return BadRequest();
            }
            if (dataContext is not null)
            {
                pub.Date_pub = DateTime.Now;
                pub.PhotoUrl = ImageService.SaveImage(pub.Photo,"publicite");
                pub.Photo = null;
                dataContext.Publicites.Add(pub);
                if (dataContext is not null)
                {
                    await dataContext.SaveChangesAsync();
                }
            }
            return Ok();
        }
        [Route("GetAllPublicite")]
        [HttpGet]
        public async Task<ActionResult<List<PubliciteModels>>> GetAllPublicite()
        {
            if (dataContext != null)
            {
                var publicitesFalse = await dataContext.Publicites.ToListAsync();
                var publicitesTrue = new List<PubliciteModels>();
                foreach(var pub in publicitesFalse)
                {
                    PubliciteModels publi = new();
                    publi = pub;
                    publi.Photo = ImageService.GetImageAsByteArray(pub.PhotoUrl);
                    publicitesTrue.Add(publi);
                }
                if (publicitesTrue != null)
                {
                    return publicitesTrue;
                }
            }
            return NotFound();
        }
        [Route("GetAllCommande")]
        [HttpGet]
        public async Task<ActionResult<List<CommandeModels>>> GetAllCommande()
        {
            if (dataContext != null)
            {
                var commandes = await dataContext.Commandes.ToListAsync();
                if (commandes != null)
                {
                    return commandes;
                }
            }
            return NotFound();
        }
        [HttpPost("AjoutStat")]
        public async Task<ActionResult<StatMois>> AjoutStat([FromBody] StatMois statMois)
        {
            if (statMois == null)
            {
                return BadRequest();
            }
            if (dataContext is not null)
            {
                statMois.id_produit = produitModelTemp.Id_produit.ToString().ToUpper();
                await dataContext.Mois.AddAsync(statMois);
                if (dataContext is not null)
                {
                    await dataContext.SaveChangesAsync();
                }
            }
            return CreatedAtAction(nameof(GetProduit), new { id = statMois.id_produit });
        }
        [Route("GetAllStat")]
        [HttpGet]
        public async Task<ActionResult<List<StatMois>>> GetAllStat()
        {
            if (dataContext != null)
            {
                var stat = await dataContext.Mois.ToListAsync();
                if (stat != null)
                {
                    return stat;
                }
            }
            return NotFound();
        }
        [HttpPost("ModifieCommande")]
        public async Task<ActionResult<CommandeModels>> ModifieCommande([FromBody] CommandeModels commande)
        {
            if (dataContext != null)
            {
                var comMod = await dataContext.Commandes.FirstOrDefaultAsync(c => c.Id_commande.ToString().ToUpper().Equals(commande.Id_commande.ToString().ToUpper()));
                if (comMod != null)
                {
                    comMod.is_livrer = true;
                    await dataContext.SaveChangesAsync();
                    return Ok(comMod);
                }
            }
            return NotFound();
        }

        [HttpPost("AnnulerCommande")]
        public async Task<ActionResult<CommandeModels>> AnnulerCommande([FromBody] CommandeModels commande)
        {
            if (dataContext != null)
            {
                var comMod = await dataContext.Commandes.FirstOrDefaultAsync(c => c.Id_commande.ToString().ToUpper().Equals(commande.Id_commande.ToString().ToUpper()));
                if (comMod != null)
                {
                    comMod.is_livrer = false;
                    await dataContext.SaveChangesAsync();
                    return Ok(comMod);
                }
            }
            return NotFound();
        }
    }
}
