using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkKnowleadge.Domain
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LeagueId { get; set; }
        public virtual League League { get; set; }


        public void PrintData(League league)
        {
            Console.WriteLine($"\nTime com Indentificação: {Id}" +
                              $"\n\t Nome: {Name}" +
                              $"\n\t Pertence a liga: {league.Name}");
        }

    }
}
