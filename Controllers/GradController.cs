using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Jan2021MOJ.Controllers;

[ApiController]
[Route("[controller]")]
public class GradController : ControllerBase
{
    public Context Context { get; set; }

    public GradController(Context context)
    {
        Context = context;
    }

    [Route("DodajGrad")]
    [HttpPost]
    public async Task<ActionResult> DodajGrad(string naziv, double longitude, double latitude)
    {
        if(string.IsNullOrWhiteSpace(naziv))
        {
            return BadRequest("Unesite naziv grada!");
        }

        //E/W
        if(longitude > 180 || longitude < -180)
        {
            return BadRequest("Neispravna geografska duzina!");
        }

        //N/S
        if(latitude > 90 || latitude < -90)
        {
            return BadRequest("Neispravna geografska sirina!");
        }

        var g = new Grad();
        g.Naziv = naziv;
        g.Longitude = longitude;
        g.Latitude = latitude;

        try
        {
            Context.Gradovi.Add(g);
            await Context.SaveChangesAsync();
            return Ok("Podaci uspesno dodati! ID grada: "+g.ID);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PreuzmiGradove")]
    [HttpGet]
    public async Task<ActionResult> Preuzmi()
    {
        var gradovi = await Context.Gradovi.Include(p => p.Prilike).ToListAsync();

        try
        {
            return Ok(gradovi.Select(g => new 
            {
                ID = g.ID,
                Naziv = g.Naziv,
                Longitude = g.Longitude,
                Latitude = g.Latitude,
                Prilike = g.Prilike.Select(p => new
                {
                    ID = p.ID,
                    Mesec = p.Mesec,
                    Temperatura = p.Temperatura,
                    Padavine = p.Padavine,
                    SuncaniDani = p.SuncaniDani
                })

            }).ToList());
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PreuzmiGrad/{id}")]
    [HttpGet]
    public async Task<ActionResult> PreuzmiGrad(int id)
    {
        var grad = Context.Gradovi.Where(g => g.ID == id);

        if(grad == null)
        {
            return BadRequest("Grad sa trazenim id-jem ne postoji!");
        }

        try
        {
            var g = await grad.ToListAsync();

            return Ok(grad.Select(g => new
            {
                ID = g.ID,
                Naziv = g.Naziv,
                Longitude = g.Longitude,
                Latitude = g.Latitude,
                Prilike = g.Prilike.Select(p => new
                {
                    ID = p.ID,
                    Mesec = p.Mesec,
                    Temperatura = p.Temperatura,
                    Padavine = p.Padavine,
                    SuncaniDani = p.SuncaniDani
                })
            }).ToList());
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
