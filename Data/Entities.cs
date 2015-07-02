using System.Data.Entity;

namespace Data
{
    public class Entities : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; } 
        
        public Entities() : base("name=Entities")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        static Entities()
        {
            Database.SetInitializer<Entities>(new Seeder());
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