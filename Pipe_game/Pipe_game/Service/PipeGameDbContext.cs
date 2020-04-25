using System;
using System.Collections.Generic;
using System.Text;
using Pipe_game.entity;
using Microsoft.EntityFrameworkCore;

namespace Pipe_game.Service
{
    class PipeGameDbContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PipeGame;Trusted_Connection=True;");
        }
    }
}
