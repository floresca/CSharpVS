using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC_4_BouncingBall
{
    class Program
    {
        static void Main(string[] args)
        {
            MotherSeesBall(1.00, 1.00, 1);
        }

        public static int MotherSeesBall(double h, double bounce, double window)
        {
            double ballHeight = h;
            int count = 0;

            if (h > window && bounce < 1 && bounce > 0 && window > 0)
            {
                do
                {
                    count++;
                    ballHeight = bounce * ballHeight;
                }
                while (ballHeight > window);

                count = (count * 2) - 1;
                return count;
            }
            else
            {
                return -1;
            }
        }
    }
}