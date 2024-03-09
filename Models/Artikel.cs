namespace Assessment_back_end.Models;

public class Artikel
{
    public string Code { get; set; }
    public string Naam { get; set; }
    public int PotMaat { get; set; }
    public int PlantHoogte { get; set; }
    public string? Kleur { get; set; }
    public string ProductGroep { get; set; }
}