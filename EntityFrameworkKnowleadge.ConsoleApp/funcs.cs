using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly FootballLeagueDbContext _context;

        public funcs()
        {
            _context = new FootballLeagueDbContext();
        }

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

        public async void GeneralLeagueSearchWithLinq()
        {
            var leagues = await (from league in _context.Leagues select league).ToListAsync();

            foreach (var league in leagues)
            {
                league.PrintData();
            }

        }

        public async void GeneralTeamSearch()
        {
            var teams = await _context.Teams.Include(teamProperty => teamProperty.League).ToListAsync();

            foreach (var team in teams)
            {
                team.PrintData();
            }
        }

        public async void GeneralTeamSearchWithLinq()
        {
            var teams = await (
                from team in _context.Teams select team)
                .Include(teamProperty => teamProperty.League).ToListAsync();

            foreach (var team in teams)
            {
                team.PrintData();
            }
        }

        public async Task<League> NameLeagueSearch(string name)
        {
            var league = await _context.Leagues.Where(league => league.Name == name).FirstOrDefaultAsync();

            league.PrintData();

            return league;
        }

        public async Task<League> NameLeagueSearchWithLinq(string name)
        {
            var leagueRef = await (from league in _context.Leagues
                where league.Name == name
                select league).FirstOrDefaultAsync();

            leagueRef.PrintData();

            return leagueRef;
        }

        public async Task<Team> NameTeamSearch(string name)
        {
            var team = await _context.Teams.Where(team => team.Name == name).Include(teamProperty => teamProperty.League).FirstOrDefaultAsync();

            team.PrintData();

            return team;
        }

        public async Task<Team> NameTeamSearchWithLinq(string name)
        {

            var teamRef = await (from team in _context.Teams
                where team.Name == name
                select team).Include(teamProperty => teamProperty.League).FirstOrDefaultAsync();

            teamRef.PrintData();

            return teamRef;
        }

        public async void PartialNameLeagueSearch(string name)
        {
            var leagues = await _context.Leagues.Where(league => EF.Functions.Like(league.Name, $"%{name}%")).ToListAsync();

            Console.WriteLine($"Ligas que contém a frase : {name}");

            foreach (var league in leagues)
            {
                league.PrintData();
            }
        }

        public async void PartialNameLeagueSearchWithLinq(string name)
        {
            var leaguesRef = await (from leagues in _context.Leagues
                where EF.Functions.Like(leagues.Name, $"%{name}%")
                select leagues).ToListAsync();

            Console.WriteLine($"Ligas que contém a frase : {name}");

            foreach (var league in leaguesRef)
            {
                league.PrintData();
            }
        }

        public async void PartialNameTeamSearch(string name)
        {
            var teams = await _context.Teams.Where(team => EF.Functions.Like(team.Name, $"%{name}%"))
                .Include(teamProperty => teamProperty.League)
                .ToListAsync();

            Console.WriteLine($"Times que contém a frase : {name}");

            foreach (var team in teams)
            {
                team.PrintData();
            }
        }

        public async void PartialNameTeamSearchWithLinq(string name)
        {
            var teamsRef = await (from teams in _context.Teams
                                  where EF.Functions.Like(teams.Name, $"%{name}%")
                                  select teams)
                .Include(teamProperty => teamProperty.League).ToListAsync();

            Console.WriteLine($"Times que contém a frase : {name}");

            foreach (var team in teamsRef)
            {
                team.PrintData();
            }
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


            Console.WriteLine($"Valores antigos desse time:");
            teamReference.PrintData();

            teamReference.Name = newName;
            teamReference.LeagueId = leagueID;


            Console.WriteLine($"Novos valores para esse time:");
            teamReference.PrintData();

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

            Console.WriteLine($"Valores antigos desse time:");
            teamReference.PrintData();

            Console.WriteLine("Deletando do banco");

            _context.Teams.Remove(teamReference);
        }

        public async Task<List<string>> SelectJustNameOfTeam()
        {
            /// <summary>
            /// Example how to get only one property from a table with Ef Core
            /// </summary>
            
            var teamNames = await _context.Teams.Select(t => t.Name).ToListAsync();

            return teamNames;
        }

        public async Task<Object> AnonymousProjection()
        {
            /// <summary>
            /// Example how to create an anonymousType objetc, it can't be returned, but may be turned in a object to be returned if needed.
            /// </summary>
            var teams = await _context.Teams.Include(q => q.Coach)
                .Select(q => new
                {
                    TeamName = q.Name,
                    CoachName = q.Coach.Name,
                }).ToListAsync();

            /*
             *Observação, poderiamos também criar um metodo tipado (mais adequado inclusive), onde está somente "new", colocariamos o tipo, por exemplo:
             *
             * var teams = await _context.Teams.Include(q => q.Coach)
                .Select(q => new ClasseCriada
                {
                    TeamName = q.Name,
                    CoachName = q.Coach.Name,
                }).ToListAsync();

            Neste cenário teriamos que criar a "ClasseCriada" antes, porem desta forma saberiamos o que retornaria sempre, diferentemente de uma classe anonima, onde precisamos ver quais campos
            são retornados no código.

             */

            return teams;
        }

        public async Task<List<League>> FilteringWithRelatedData(string teamName)
        {
            /// <summary>
            /// Example how to filter on a table based on some info, that table has any related data.
            /// </summary>
            
            var leagues = await _context.Leagues
                .Where(q => q.Teams.
                    Any(x => x.Name.Contains(teamName))
                ).ToListAsync();

            return leagues;
        }

        public async Task<List<League>> SearchLeagueByNameRawSql(string name)
        {
            var league = await _context.Leagues.FromSqlRaw($"select * from league where name = '{name}'").ToListAsync();
            /* Maneira incorreta, pois desta forma estamos tirando a parametrização, e ficamos suscetíveis a sql injections */

            var league2 = await _context.Leagues.FromSqlRaw("select * from league where name = {0}", name).ToListAsync();
            /* Maneira correta, pois desta forma estamos inserindo a parametrização*/

            var league3 = await _context.Leagues.FromSqlInterpolated($"select * from league where name = {name}").ToListAsync();
            /* Outra forma de utilizarmos query SQL, com entity framework, desta forma ele automaticamente parametriza os valores para nós*/


            return league;
        }

        public async Task<List<T>> SearchLeagueBySqlRawNotAllColumns<T>(string name)
        {
            var league =
                 await _context.Database.ExecuteSqlInterpolatedAsync($"select name from league where name = {name}");

            throw new NotImplementedException();
        }
    }
}
