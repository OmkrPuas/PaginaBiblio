using BibliotecaAPI.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Data
{
    public class BibliotecaDbContext : IdentityDbContext
    {
        public DbSet<AutorEntity> Autores { get; set; }
        public DbSet<LibroEntity> Libros { get; set; }

        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutorEntity>().ToTable("Autores");
            modelBuilder.Entity<AutorEntity>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<AutorEntity>().HasMany(t => t.Libros).WithOne(p => p.Autor);

            modelBuilder.Entity<LibroEntity>().ToTable("Libros");
            modelBuilder.Entity<LibroEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<LibroEntity>().HasOne(p => p.Autor).WithMany(t => t.Libros);

            //dotnet tool install --global dotnet-ef
            //dotnet ef migrations add InitialCreate
            //dotnet ef database update
        }
    }
}
