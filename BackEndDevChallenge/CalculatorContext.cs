    using BackEndDevChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BackEndDevChallenge
{
    public class CalculatorContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public CalculatorContext(DbContextOptions<CalculatorContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<MathProblem> MathProblems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
