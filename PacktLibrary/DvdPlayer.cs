using System;
using System.Collections.Generic;
using System.Text;

namespace Packt.Shared
{
    public class DvdPlayer : IPlayable
    {
        public void Pause()
        {
            Console.WriteLine("DVD Player is Pausing");
        }

        public void Play()
        {
            Console.WriteLine("DVD Player is Playing");
        }

        
    }
}
