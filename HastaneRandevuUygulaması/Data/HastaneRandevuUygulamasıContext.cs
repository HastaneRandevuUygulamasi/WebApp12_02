using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuUygulaması.Models;

namespace HastaneRandevuUygulaması.Data
{
    public class HastaneRandevuUygulamasıContext : DbContext
    {
        public HastaneRandevuUygulamasıContext (DbContextOptions<HastaneRandevuUygulamasıContext> options)
            : base(options)
        {
        }

        public DbSet<HastaneRandevuUygulaması.Models.Doktor> Doktor { get; set; } = default!;

        public DbSet<HastaneRandevuUygulaması.Models.Hasta>? Hasta { get; set; }

        public DbSet<HastaneRandevuUygulaması.Models.Hastane>? Hastane { get; set; }

        public DbSet<HastaneRandevuUygulaması.Models.Poliklinik>? Poliklinik { get; set; }

        public DbSet<HastaneRandevuUygulaması.Models.Randevu>? Randevu { get; set; }
        public DbSet<HastaneRandevuUygulaması.Models.il>? il { get; set; }
        public DbSet<HastaneRandevuUygulaması.Models.ilce>? ilce { get; set; }
    }
}
