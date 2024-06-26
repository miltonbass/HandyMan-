﻿using Microsoft.EntityFrameworkCore;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.DTOs;

namespace HandyMan_.Backend.Data
{
    public class DataContext : IdentityDbContext<User>  
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.SetCommandTimeout(600);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }

        public DbSet<SurveyDefinitionEntity> SurveyDefinitions { get; set; }

        public DbSet<SurveyResponseDTO> SurveyResponses { get; set; }
        public DbSet<AnswersDTO> AnswerSurveysDTO { get; set; }

        public DbSet<TemporalOrder> TemporalOrders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<City>().HasIndex(c => new { c.StateId, c.Name }).IsUnique();
            modelBuilder.Entity<Service>().HasIndex(c => new { c.UserId, c.Name }).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(s => new { s.CountryId, s.Name }).IsUnique();
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