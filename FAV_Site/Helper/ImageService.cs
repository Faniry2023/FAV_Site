using Google.Apis.Upload;

namespace FAV_Site.Helper
{
    public class ImageService
    {
        public static string SaveImage(byte[] imageBytes, string file)
        {
            if(imageBytes == null || imageBytes.Length == 0)
            {
                throw new ArgumentNullException("Image data is invalid");
            }
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if(!Directory.Exists(wwwrootPath))
            {
                Directory.CreateDirectory(wwwrootPath); 
            }
            string fileName = $"{Guid.NewGuid()}_{DateTime.Now:yyyyMMdd_HHmmss}_{file}.jpg";
            string fullImagePath = Path.Combine(wwwrootPath, fileName);
            File.WriteAllBytes(fullImagePath, imageBytes);
            return "/uploads/"+fileName; 
        }

        public static byte[] GetImageAsByteArray(string relativeImagePath)
        {
            // Supprimer le '/' initial si présent dans le chemin relatif
            if (relativeImagePath.StartsWith("/"))
            {
                relativeImagePath = relativeImagePath.Substring(1);
            }

            // Chemin complet du fichier dans wwwroot
            string fullImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativeImagePath);

            // Vérifier si le fichier existe
            if (File.Exists(fullImagePath))
            {
                // Lire le fichier et le convertir en tableau de bytes
                return File.ReadAllBytes(fullImagePath);
            }
            else
            {
                throw new FileNotFoundException("Le fichier image n'existe pas.");
            }
        }
    }
}
