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
            if (number >= 0)
                return 1;
            else
                return -1;
        }
    }
}
