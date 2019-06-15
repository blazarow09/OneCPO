using Microsoft.EntityFrameworkCore;

namespace OneCPO.Data
{
    public class OneCPODbContext : DbContext
    {
        public OneCPODbContext()
        {
        }

        public OneCPODbContext(DbContextOptions<OneCPODbContext> options)
            : base(options)
        {
        }
    }
}