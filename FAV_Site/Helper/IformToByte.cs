namespace FAV_Site.Helper
{
    public class IformToByte
    {
        public static async Task<byte[]> IFormFileToByte(IFormFile formFile)
        {
            byte[] byteImg = [0];
            using(var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                byteImg = memoryStream.ToArray();
            }
            return byteImg;
        }
    }
}
