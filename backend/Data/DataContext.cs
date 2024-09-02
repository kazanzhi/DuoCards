using backend.Modal;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> context) : base(context)
        {

        }

        public DbSet<Card> Cards { get; set; }
    }
}
