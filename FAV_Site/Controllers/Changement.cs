namespace FAV_Site.Controllers
{
    public class Changement
    {
        public static string ByteToImageLocation(byte[] Imgbyte)
        {
            string imgLoc = string.Empty;
            byte[] imgEnByte = Imgbyte;
            string base64String = Convert.ToBase64String(imgEnByte);
            return string.Format("data:image/png;base64,{0}", base64String);
        }
    }
}
