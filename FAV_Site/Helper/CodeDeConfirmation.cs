namespace FAV_Site.Helper
{
    public class CodeDeConfirmation
    {
        public static string MyCode()
        {
            string reponse = string.Empty;
            for(int i = 1; i<= 7; i++)
            {
                Random rad = new Random();
                reponse += rad.Next(0,10).ToString();
            }
            return reponse;
        }
    }
}
