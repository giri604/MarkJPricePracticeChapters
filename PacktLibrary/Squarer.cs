using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Packt.Shared
{
    public static class Squarer
    {
        public static double Square<T>(T input) where T : IConvertible
        {
            double d = input.ToDouble(Thread.CurrentThread.CurrentCulture);
            return d * d;
        }
    }
}
