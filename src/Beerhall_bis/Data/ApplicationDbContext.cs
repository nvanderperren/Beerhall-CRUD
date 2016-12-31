﻿using Beerhall_bis.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beerhall_bis.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Brewer> Brewers { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brewer>(MapBrewer);
            modelBuilder.Entity<Beer>(MapBeer);
            modelBuilder.Entity<Location>(MapLocation);

        }

        private void MapLocation(EntityTypeBuilder<Location> l)
        {
            //Table name
            l.ToTable("Location");

            //Primary Key
            l.HasKey(t => t.PostalCode);

            //Properties
            l.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
        }

        private void MapBeer(EntityTypeBuilder<Beer> b)
        {
            //Table name
            b.ToTable("Beer");
            // Properties
            b.Property(t => t.Name).IsRequired().HasMaxLength(100);
        }

        private void MapBrewer(EntityTypeBuilder<Brewer> b)
        {
            //Table name
            b.ToTable("Brewer");

            //Primary Key
            b.HasKey(t => t.BrewerId);

            //Properties
            b.Property(t => t.Name)
                .HasColumnName("BrewerName")
                .IsRequired()
                .HasMaxLength(100);

            b.Property(t => t.ContactEmail)
                .HasMaxLength(100);

            b.Property(t => t.Street)
                .HasMaxLength(100);

            b.Property(t => t.BrewerId)
                .ValueGeneratedOnAdd();

            //Associations
            b.HasMany(t => t.Beers)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(t => t.Location)
               .WithMany()
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
