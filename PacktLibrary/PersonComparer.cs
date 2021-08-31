using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Packt.Shared
{
    public class PersonComparer : IComparer<Person>
    {
        public int Compare([AllowNull] Person x, [AllowNull] Person y)
        {
            //Compare The Name Lenght...
            int result = x.Name.Length.CompareTo(y.Name.Length);
            if(result == 0)
            {
                //...Then Compare By Name
                return x.Name.CompareTo(y.Name);
            }
            else
            {
                //...otherwise comapre by the lenght
                return result;
            }

        }
    }
}
