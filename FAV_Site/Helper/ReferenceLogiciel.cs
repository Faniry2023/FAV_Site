using FAV_Site.Data;
using Microsoft.EntityFrameworkCore;

namespace FAV_Site.Helper
{
    public class ReferenceLogiciel
    {
        private static readonly DataContext dataContext;

        public  async Task<string> ValeurReference()
        {
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
            return valeur;
        }
    }
}
