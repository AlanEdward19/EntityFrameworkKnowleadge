using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkKnowleadge.Domain.Common;

namespace EntityFrameworkKnowleadge.Domain
{
    public class Team : BaseDomainObject
    {

        public string Name { get; set; }
        public int LeagueId { get; set; }
        public virtual League League { get; set; }
        public virtual Coach  Coach { get; set; }

        public virtual List<Match> HomeMatches { get; set; }
        public virtual List<Match> AwayMatches { get; set; }


        public void PrintData()
        {
            Console.WriteLine($"\nTime com Indentificação: {Id}" +
                              $"\n\t Nome: {Name}" +
                              $"\n\t Pertence a liga: {League.Name}");
        }

    }
}
