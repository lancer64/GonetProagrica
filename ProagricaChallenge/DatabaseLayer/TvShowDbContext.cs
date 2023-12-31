﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProagricaChallenge.DatabaseLayer.Models;

namespace ProagricaChallenge.DatabaseLayer
{
    public class TvShowDbContext : DbContext
    {
        public DbSet<TvShow> TvShows { get; set; } = null!;

        public TvShowDbContext() { }

        public TvShowDbContext(DbContextOptions<TvShowDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //This avoids conflict between EntityFrameworkCore.SQL and EntityFrameworkCore.InMemory
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=LAPTOP-BNAG8JNC;database=myTvShows;trusted_connection=true;");
            }
        }
    }
}
