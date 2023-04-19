using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Models;

public class Grad
{
    [Key]
    public int ID { get; set; }
    [Required]
    [MaxLength(20)]
    public string Naziv { get; set; } = null!;
    public double Longitude { get; set; }
    public double Latitude { get; set; }

    [JsonIgnore]
    public virtual List<MeteoroloskaPrilika> Prilike { get; set; } = null!;
}