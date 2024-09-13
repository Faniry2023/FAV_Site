using System.Text.Json.Serialization;
namespace FAV_Site.Helper
{
    public class PositionPerModels
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nom")]
        public string? Nom { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
    }
}
