using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkKnowleadge.Domain
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void PrintData()
        {
            Console.WriteLine($"Liga com Identificação: {Id}" +
                              $"\n\t Nome: {Name}");
        }
    }
}
