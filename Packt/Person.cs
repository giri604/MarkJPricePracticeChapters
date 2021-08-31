using System;
using System.Collections.Generic;

namespace Packt.Shared
{
    public partial class Person : Object
    {
        //Fields
        public string Name; 
        public DateTime DateOfBirth;

        public WondersOfTheAncientWorld FavoriteAncientWonder;
        public WondersOfTheAncientWorld BucketList;

        public List<Person> Children = new List<Person>();  //also Used in Indexer Example

        public (string, int) GetFruit()     //Tuple
        {
            return ("Apples", 5);
        }

        public (string Name, int Number) GetNamedFruit()    //Tuple
        {
            return (Name: "Apple", Number: 5);
        }

    }
}
