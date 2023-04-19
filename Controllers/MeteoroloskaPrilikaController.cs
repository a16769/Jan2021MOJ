using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Jan2021MOJ.Controllers;

[ApiController]
[Route("[controller]")]
public class MeteoroloskaPrilikaController : ControllerBase
{
    public Context Context { get; set; }

    public MeteoroloskaPrilikaController(Context context)
    {
        Context = context;
    }

    [Route("PreuzmiPrilike")]
    [HttpGet]
    public async Task<ActionResult> Preuzmi(int idGrada)
    {
        var grad = await Context.Gradovi.FindAsync(idGrada);

        var pr = Context.Prilike.Where(p => p.Grad == grad).ToList();
        if(pr == null)
        {
            return BadRequest("Trazena prilika ne postoji!");
        }

        //var prilika = await p.ToListAsync();

        try
        {
            return Ok(pr.Select(p => new
            {
                Grad = p.Grad,
                Mesec = p.Mesec,
                ID = p.ID,
                Temperatura = p.Temperatura,
                Padavine = p.Padavine,
                SuncaniDani = p.SuncaniDani
            }).ToList());

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [Route("DodajPriliku")]
    [HttpPost]
    public async Task<ActionResult> Dodaj(int idGrada, int mesec, int vrednostT, int vrednostP, int vrednostSD)
    {
        var grad = Context.Gradovi.Include(p => p.Prilike).Where(p => p.ID == idGrada).FirstOrDefault();
        if(grad == null)
        { 
            return BadRequest("Nepostojeci grad!");
        }

        if(mesec <=0)
        {
            return BadRequest("Unesite mesec!");
        }

        var provera = Context.Prilike.Where(p => p.Grad.ID == idGrada && p.Mesec == mesec).FirstOrDefault();

        if(provera != null)
        {
            return BadRequest("Vremenska prilika za ovaj grad i mesec vec postoji!");
        }

        if(vrednostSD <0 || vrednostSD > 31)
        {
            return BadRequest("Neispravan broj suncanih dana!");
        }

        MeteoroloskaPrilika prilika = new MeteoroloskaPrilika();
        prilika.Grad = grad;
        prilika.Mesec = mesec;
        prilika.Temperatura = vrednostT;
        prilika.Padavine = vrednostP;
        prilika.SuncaniDani = vrednostSD;

        try
        {
            Context.Prilike.Add(prilika);
            await Context.SaveChangesAsync();
            return Ok(prilika);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [Route("PromeniTemperaturu")]
    [HttpPut]
    public async Task<ActionResult> PromeniTemperatru(int idPrilike, int novaTemperatura)
    {
        if(idPrilike <= 0)
        {
            return BadRequest("Pogresan ID prilike!");
        }

        if(novaTemperatura < -50 || novaTemperatura > 50)
        {
            return BadRequest("Temperatura izvan granica opsega!");
        }

        var prilika = Context.Prilike.Where(p => p.ID == idPrilike).FirstOrDefault();
        if(prilika == null)
        {
            return BadRequest("Prilika sa unetim parametrima ne postoji!");
        }

        prilika.Temperatura = novaTemperatura;

        try
        {
            Context.Prilike.Update(prilika);
            await Context.SaveChangesAsync();
            return Ok(prilika);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [Route("PromeniPadavine")]
    [HttpPut]
    public async Task<ActionResult> PromeniPadavine(int idPrilike, int novePadavine)
    {
        if(idPrilike <= 0)
        {
            return BadRequest("Pogresan ID prilike!");
        }

        if(novePadavine <= 0 || novePadavine > 1000)
        {
            return BadRequest("Padavine izvan granica opsega!");
        }

        var prilika = Context.Prilike.Where(p => p.ID == idPrilike).FirstOrDefault();
        if(prilika == null)
        {
            return BadRequest("Prilika sa unetim parametrima ne postoji!");
        }

        prilika.Padavine = novePadavine;

        try
        {
            Context.Prilike.Update(prilika);
            await Context.SaveChangesAsync();
            return Ok(prilika);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [Route("PromeniSuncaneDane")]
    [HttpPut]
    public async Task<ActionResult> PromeniSD(int idPrilike, int noviSuncaniDani)
    {
        if(idPrilike <= 0)
        {
            return BadRequest("Pogresan ID prilike!");
        }

        if(noviSuncaniDani < 0 || noviSuncaniDani > 31)
        {
            return BadRequest("Suncani dani izvan granica opsega!");
        }

        var prilika = Context.Prilike.Where(p => p.ID == idPrilike).FirstOrDefault();
        if(prilika == null)
        {
            return BadRequest("Prilika sa unetim parametrima ne postoji!");
        }

        prilika.SuncaniDani = noviSuncaniDani;

        try
        {
            Context.Prilike.Update(prilika);
            await Context.SaveChangesAsync();
            return Ok(prilika);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}