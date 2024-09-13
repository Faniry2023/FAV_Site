namespace FAV_Site.Models
{
    public class PaymentModels
    {
        public byte[]? QrCodePay {  get; set; }
        public string? QrCodePayLoc { get; set; }
        public bool MV {  get; set; }
        public bool AM { get; set; }
        public bool OM { get; set; }
        public bool VISA {  get; set; }
        public string? Description { get; set; }
        public string? Num {  get; set; }
        public string? NumCarte { get; set; }
        public string? DateExpiration { get; set; }
        public int codeSecurite {  get; set; }
    }
}
