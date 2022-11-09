using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkKnowleadge.Domain.Common;

namespace EntityFrameworkKnowleadge.Domain
{
    public class Coach : BaseDomainObject
    {
        public string Name { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
