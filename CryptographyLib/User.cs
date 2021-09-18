using System;
using System.Collections.Generic;
using System.Text;

namespace Packt.Shared
{
    public class User
    {
        public string Name { get; set; }
        public string Salt { get; set; }
        public string SaltedHashedPassword { get; set; }
    }
}
