using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recruitment_challenge.Classes
{
    public class Unit
    {
        public Guid UnitGroupId { get; init; }
        public string Name { get; init; }
        public string AddressInformation { get; init; }
    }

    public class UnitCollection
    {
        public List<Unit> Items { get; init; }
    }
}
