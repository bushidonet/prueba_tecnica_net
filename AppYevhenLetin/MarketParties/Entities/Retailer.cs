using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PruebaYevhenLetin.Entity;

public class Retailer
{
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; } 
        [JsonPropertyName("reCode")]
        public string ReCode { get; set; }
        [JsonPropertyName("reName")]
        public string ReName { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("codingScheme")]
        public string CodingScheme { get; set; }

        
}