using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebTravelTest.Entities.Models;

namespace WebTravelTest.Entities.DbContexts
{
    public class ApplicationDbContext : TravelTestContext<ApplicationDbContext>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
