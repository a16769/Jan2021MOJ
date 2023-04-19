using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
namespace Models;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options){}

    public DbSet<Grad> Gradovi { get; set; }
    public DbSet<MeteoroloskaPrilika> Prilike { get; set; }
}