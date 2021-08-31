using System;
using System.Collections.Generic;
using System.Text;

namespace Packt.Shared
{
    public class ThingOfDefaults
    {
        public int Population;
        public DateTime when;
        public string Name;
        public List<Person> People;

        public ThingOfDefaults()
        {
            Population = default;
            when = default;
            Name = default;
            People = default;
        }
    }
}
