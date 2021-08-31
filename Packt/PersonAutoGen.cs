using System;
using System.Collections.Generic;
using System.Text;

namespace Packt.Shared
{
    public partial class Person
    {
        public string Origin
        {
            get
            {
                return $"{Name} was born ";
            }
        }

        public string Greeting => $"{Name} Says Hello.";
        public int Age => System.DateTime.Now.Year - DateOfBirth.Year;

        public string FavoriteIceCream;

        private string favouritePrimaryColor;
        public string FavouritePrimaryColor
        {
            get { return favouritePrimaryColor; }
            set
            {
                switch (value.ToLower())
                {
                    case "red":
                    case "green":
                    case "blue":
                        favouritePrimaryColor = value;
                        break;
                    default:
                        throw new System.ArgumentException($"{value} is not a primary color. " + "choose from: red, green, blue");

                }
            }
        }

        public Person this[int index]
        {
            get
            {
                return Children[index];
            }
            set
            {
                Children[index] = value;
            }
        }


    }
}
