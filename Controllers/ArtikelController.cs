using Assessment_back_end.Data;
using Assessment_back_end.Models;
using Assessment_back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_back_end.controllers;

[ApiController]
[Route("[controller]")]
public class ArtikelController(DatabaseContext context): ControllerBase
{
    private readonly ArtikelService _service = new (context);
    
    [HttpPost]
    public ActionResult Create([FromBody] Artikel artikel)
    {
        return _service.Create(artikel);
    }

    [HttpPut]
    public ActionResult Edit([FromBody] Artikel artikel)
    {
        return _service.Edit(artikel);
    }

    [HttpDelete]
    [Route("{code}")]
    public ActionResult Delete(string code)
    {
        return _service.Delete(code);
    }

    [HttpGet]
    public ActionResult Get(string? filter, string? naam, string? kleur, string? productgroep, int? van, int? tot, string? sorteerveld, int? sorteer_van, int? sorteer_tot)
    {
        return _service.Get(filter, naam, kleur, productgroep, van, tot, sorteerveld, sorteer_van, sorteer_tot);
    }
}