using System.Data.Entity;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DatabaseContext(): base("name=DatabaseContext")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        static DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(new Seeder());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //TODO: Upon seeding the database a DateTime field(datetime) is inserted into db as (datetime2)
            //Blog has '1' DateField currently
            modelBuilder.Entity<Blog>()
           .Property(p => p.DateCreated)
           .HasColumnType("datetime2");


            //Post has '1' DateField currently
            modelBuilder.Entity<Post>()
           .Property(p => p.DateCreated)
           .HasColumnType("datetime2");
        }
    }




}