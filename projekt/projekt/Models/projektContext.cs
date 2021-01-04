using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projekt.Models;

namespace projekt.Models
{
    public class projektContext: DbContext
    {
        

        public projektContext(DbContextOptions<projektContext>options): base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

            modelBuilder.Entity<FilmAktor>().HasKey(cg => new { cg.AktorId, cg.FilmId });
            modelBuilder.Entity<Grade>().HasKey(g => new { g.AktorId, g.FilmId });
           
        }
        public DbSet<Film> Films { get; set; }
        public DbSet<Cena> Cenas { get; set; }
        public DbSet<Kategoria> Kategorias { get; set; }
        public DbSet<projekt.Models.Aktor> Aktor { get; set; }
        public DbSet<projekt.Models.FilmAktor> FilmAktors { get; set; }
        public DbSet<Grade> Grades { get; set; }

    }
}
