using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC_6_FareEstimation
{
    class Program
    {
        static void Main(string[] args)
        {
            int rideDistance = 7;
            double[] costPerMile = { 1.1, 1.8, 2.3, 3.5 };
            double[] costPerMinute = { 0.2, 0.35, 0.4, 0.45 };
            int rideTime = 30;

            var stuff = new UberRuns();
            stuff.calc(costPerMile, rideDistance);
            stuff.calc(costPerMinute, rideTime);
            stuff.add(costPerMile, costPerMinute);

        }
    }

    class UberRuns
    {
        public void calc(double[] array, int param)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i] * param;
            }
        }

        public double[] add(double[] rideCost, double[] costPerMinute)
        {
            for (int i = 0; i < costPerMinute.Length; i++)
            {
                rideCost[i] = rideCost[i] + costPerMinute[i];
            }

            return rideCost;
        }
    }

}
