using FAV_Site.Models;

namespace FAV_Site.Helper
{
    public class LoginAndRegister
    {
        public UtilisateurModels? Utilisateur { get; set; }
        public Log_UtilisateurModels? Log_Utilisateur { get; set; }
        public string? Confirme { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; }

        public async Task<byte[]> MyPhoto()
        {
            var lePhoto = await IformToByte.IFormFileToByte(Photo);
            return lePhoto;
        }
    }
}
