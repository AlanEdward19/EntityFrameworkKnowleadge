﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkKnowleadge.Domain.Common;

namespace EntityFrameworkKnowleadge.Domain
{
    public class League : BaseDomainObject
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }

        public void PrintData()
        {
            Console.WriteLine($"Liga com Identificação: {Id}" +
                              $"\n\t Nome: {Name}");
        }
    }
}
