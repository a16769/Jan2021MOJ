using System.ComponentModel.DataAnnotations;
namespace Models;

public class MeteoroloskaPrilika
{
    [Key]
    public int ID { get; set; }
    public int Mesec { get; set; }
    public int Temperatura { get; set; }
    public int Padavine { get; set; }
    [Range(0, 31)]
    public int SuncaniDani { get; set; }
    public virtual Grad Grad { get; set; }
}