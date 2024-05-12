using Microsoft.EntityFrameworkCore;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HandyMan_.Shared.Entities;

namespace HandyMan_.Backend.Data
{
    public class DataContext : IdentityDbContext<User>  
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<PeopleType> PeopleTypes { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }

        public DbSet<SurveyDefinitionEntity> SurveyDefinitions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<City>().HasIndex(c => new { c.StateId, c.Name }).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(s => new { s.CountryId, s.Name }).IsUnique();

            modelBuilder.Entity<PeopleType>().HasIndex(pt => pt.Name).IsUnique();
            modelBuilder.Entity<People>().HasIndex(p => p.Identification).IsUnique();
            modelBuilder.Entity<SubscriptionType>().HasIndex(p => p.Name).IsUnique();

            //desabilitar eliminacion en cascada
            DisableCascadingDelete(modelBuilder);
        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}