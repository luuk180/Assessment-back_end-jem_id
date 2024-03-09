using Assessment_back_end.Data;
using Assessment_back_end.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_back_end.Services;

public class ArtikelService
{
    private readonly DatabaseContext _context;

    public ArtikelService(DatabaseContext context)
    {
        _context = context;
    }

    public ActionResult Create(Artikel artikel)
    {
        var check = VerifyLength(artikel);
        if (check != null) return new BadRequestObjectResult(check);
        if (_context.Artikelen.Any(a => a.Code == artikel.Code))
            return new BadRequestObjectResult("Er bestaat al een artikel met dezelfde code.");
        _context.Artikelen.Add(artikel);

        try
        {
            return _context.SaveChanges() > 0
                ? new OkObjectResult("Het artikel is succesvol aangemaakt!")
                : new BadRequestObjectResult("Er is geen artikel aangemaakt.");
        }
        catch
        {
            return new BadRequestObjectResult("Dit artikel kon niet worden aangemaakt.");
        }

        
    }

    public ActionResult Edit(Artikel artikel)
    {
        var check = VerifyLength(artikel);
        if (check != null) return new BadRequestObjectResult(check);
        if (!_context.Artikelen.Any(a => a.Code == artikel.Code)) return new BadRequestObjectResult("Het artikel bestaat niet.");

        _context.Artikelen.Update(artikel);
        return _context.SaveChanges() > 0 ? 
            new OkObjectResult("Het artikel is bijgewerkt.") : 
            new BadRequestObjectResult("Het artikel kon niet worden aangemaakt.");
    }

    public ActionResult Delete(string code)
    {
        if (!_context.Artikelen.Any(a => a.Code == code))
            return new BadRequestObjectResult("Er bestaat geen artikel met deze code.");
        _context.Artikelen.Remove(_context.Artikelen.First(a => a.Code == code));

        return _context.SaveChanges() > 0 ? 
            new OkObjectResult($"Het artikel met code {code} is verwijderd.") : 
            new BadRequestObjectResult($"Het artikel met code {code} kon niet worden verwijderd.");
    }

    public ActionResult Get(string? filter, string? naam, string? kleur, string? productgroep, int? van, int? tot, string? sorteerveld, int? sorteer_van, int? sorteer_tot)
    {
        var artikelen = _context.Artikelen.ToList();
        if (filter != null)
        {
            switch (filter)
            {
                case "naam":
                    if (naam == null)
                        return new BadRequestObjectResult("Bij sorteren op naam moet je ook een naam meegeven.");
                    artikelen = artikelen.Where(a => a.Naam.Contains(naam)).ToList();
                    break;
                case "potmaat":
                    artikelen = artikelen.Where(a => a.PotMaat > van && a.PotMaat < tot).ToList();
                    break;
                case "kleur":
                    artikelen = artikelen.Where(a => a.Kleur == kleur).ToList();
                    break;
                case "productgroep":
                    artikelen = artikelen.Where(a => a.ProductGroep == productgroep).ToList();
                    break;
            }
        }

        if (sorteerveld != null)
        {
            switch (sorteerveld)
            {
                case "code":
                    artikelen = artikelen.OrderBy(a => a.Code).ToList();
                    break;
                case "naam":
                    artikelen = artikelen.OrderBy(a => a.Naam).ToList();
                    break;
                case "potmaat":
                    artikelen = artikelen.OrderBy(a => a.PotMaat).ToList();
                    break;
                case "planthoogte":
                    artikelen = artikelen.OrderBy(a => a.PlantHoogte).ToList();
                    break;
                case "kleur":
                    artikelen = artikelen.OrderBy(a => a.Kleur).ToList();
                    break;
                case "productgroep":
                    artikelen = artikelen.OrderBy(a => a.ProductGroep).ToList();
                    break;
            }

            if (sorteer_van != null)
            {
                artikelen.Reverse();
                artikelen = artikelen.SkipLast(sorteer_van.Value).ToList();
                artikelen.Reverse();
            };
            if (sorteer_tot != null)
            {
                artikelen = artikelen.SkipLast(sorteer_tot.Value).ToList();
            }
        }
        return new OkObjectResult(artikelen);
    }

    private static string? VerifyLength(Artikel artikel)
    {
        if (artikel.Code.Length > 13) return "Code is te lang.";
        if (artikel.Naam.Length > 50) return "Naam is te lang.";

        return null;
    }
}