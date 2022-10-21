using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkKnowleadge.Data;
using EntityFrameworkKnowleadge.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkKnowleadge.ConsoleApp
{
    public class funcs
    {
        private readonly FootballLeagueDbContext _context = new FootballLeagueDbContext();

        public League CriarLeague()
        {
            var newLeague = new League();

            Console.WriteLine("Digite o nome da liga");
            newLeague.Name = Console.ReadLine();

            return newLeague;
        }

        public Team CriarTeam()
        {
            var newTeam = new Team();

            Console.WriteLine("Insira o nome do Time");
            newTeam.Name = Console.ReadLine();

            Console.WriteLine("Insira o numero da liga que ele pertence");
            newTeam.LeagueId = int.Parse(Console.ReadLine());

            return newTeam;
        }

        public async void insertNewLeague(League league)
        {
            await _context.Leagues.AddAsync(league);

            Console.WriteLine($"Inserindo nova liga de nome: {league.Name}");
        }

        public async void insertNewTeam(Team team)
        {
            await _context.Teams.AddAsync(team);

            Console.WriteLine($"Inserindo novo time de nome: {team.Name}\nId do Time: {team.LeagueId}");
        }

        public async void GeneralLeagueSearch()
        {
            var leagues = await _context.Leagues.ToListAsync();

            foreach (var league in leagues)
            {
                league.PrintData();
            }

        }

        public async void GeneralTeamSearch()
        {
            var teams = await _context.Teams.ToListAsync();

            foreach (var team in teams)
            {
                var league = await _context.Leagues.FirstOrDefaultAsync(league => league.Id == team.Id);

                team.PrintData(league);
            }
        }

        public async Task<League> NameLeagueSearch(string name)
        {
            var league = await _context.Leagues.FirstOrDefaultAsync(league => league.Name == name);

            league.PrintData();

            return league;
        }
        public async Task<Team> NameTeamSearch(string name)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(team => team.Name == name);
            var league = await _context.Leagues.FirstOrDefaultAsync(league => league.Id == team.LeagueId);

            team.PrintData(league);

            return team;
        }

        public async void SaveChanges()
        {
            Console.WriteLine("Salvando todas as alterações");
            await _context.SaveChangesAsync();
        }

        public async void UpdateLeague(League leagueReference)
        {
            Console.WriteLine("Insira o novo nome desta liga");
            var newName = Console.ReadLine();

            Console.WriteLine($"Valores antigos dessa liga:");
            leagueReference.PrintData();

            leagueReference.Name = newName;
            Console.WriteLine($"Novos valores para essa liga:");
            leagueReference.PrintData();

            _context.Leagues.Update(leagueReference);

        }
        public async void UpdateTeam(Team teamReference)
        {
            Console.WriteLine("Insira o novo nome deste time");
            var newName = Console.ReadLine();

            Console.WriteLine("Insira o Id da liga que este time pertencerá");
            var leagueID = int.Parse(Console.ReadLine());

            var league = await _context.Leagues.FirstOrDefaultAsync(league => league.Id == teamReference.LeagueId);

            Console.WriteLine($"Valores antigos desse time:");
            teamReference.PrintData(league);

            teamReference.Name = newName;
            teamReference.LeagueId = leagueID;

            league = await _context.Leagues.FirstOrDefaultAsync(league => league.Id == teamReference.LeagueId);

            Console.WriteLine($"Novos valores para esse time:");
            teamReference.PrintData(league);

            _context.Teams.Update(teamReference);
        }

        public async void DeleteLeague(League leagueReference)
        {
            Console.WriteLine($"Informações dessa liga:");
            leagueReference.PrintData();

            Console.WriteLine("Deletando do banco");

            _context.Leagues.Remove(leagueReference);
        }
        public async void DeleteTeam(Team teamReference)
        {
            var league = await _context.Leagues.FirstOrDefaultAsync(league => league.Id == teamReference.LeagueId);

            Console.WriteLine($"Valores antigos desse time:");
            teamReference.PrintData(league);

            Console.WriteLine("Deletando do banco");

            _context.Teams.Remove(teamReference);
        }
    }
}
