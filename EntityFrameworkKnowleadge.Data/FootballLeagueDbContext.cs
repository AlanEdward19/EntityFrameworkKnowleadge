using EntityFrameworkKnowleadge.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkKnowleadge.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    @"Data Source=LAPTOP-J71CJ04F\SQLEXPRESS;Initial Catalog=EntityTesteBD;Integrated Security=True") //Ta assim para testes, pessima pratica
                /*.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name}, LogLevel.Information)
                .EnableSensitiveDataLogging()*/;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // So é necessário por conta que não está seguindo a nomeclatura correta, na classe Match
        {
            modelBuilder.Entity<Team>()
                .HasMany(m => m.HomeMatches)
                .WithOne(m => m.HomeTeam)
                .HasForeignKey(m => m.HomeTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); /* Um time, tem muitos homeMatches e na tabela Match, esse time, referencia somente um HomeTeam, assim ele consegue assimilar que o HomeTeamId pertence ao Time que está fazendo essa relação
                                                    * e por convenção, dizemos que quando esse time tentar ser deletado, deve-se apagar todos os registros de matches dele da tabela match, antes de poder proceder com o deletamento desse time.
                                                    */
            modelBuilder.Entity<Team>()
                .HasMany(m => m.AwayMatches)
                .WithOne(m => m.AwayTeam)
                .HasForeignKey(m => m.AwayTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Coach> Coaches { get; set; }
    }
}
