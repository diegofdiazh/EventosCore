using EventosCore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;


namespace EventosCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Aeropuertos> Aeropuertos { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}
