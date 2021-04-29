using Microsoft.EntityFrameworkCore;
using PlaceApi.Db.Models;

namespace PlaceApi.Db
{
    public class PlaceDbCOntext: DbContext
    {
        public PlaceDbCOntext(DbContextOptions<PlaceDbCOntext> options) : base(options)
        {}
        public DbSet<Place> Places { get; set; }
    }
}