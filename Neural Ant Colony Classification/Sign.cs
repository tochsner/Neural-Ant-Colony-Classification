using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Ant_Colony_Classification
{
    class Sign
    {
        public static int GetSign(double number)
        {
            return number > 0 ? 1 : -1;
        }
    }
}
