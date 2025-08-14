namespace exercise1.infra.DbContex
{
    public class customerDbContext : DbContext
    {
        public DbSet<CustomerModel> Customers { get; set; }
        public customerDbContext(DbContextOptions<customerDbContext> options) : base(options)
        {

        }
    }
}
