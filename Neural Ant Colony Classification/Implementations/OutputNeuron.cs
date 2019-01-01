using System;
using System.Collections.Generic;
using System.Linq;

namespace Neural_Ant_Colony_Classification
{
    class OutputNeuron : INeuron
    {
        private List<IAnt> ants = new List<IAnt>();

        private int _actualOutputSign = 1;
        public int ActualOutputSign
        {
            get => _actualOutputSign;
            set
            {
                if (value == 1 || value == -1)
                    _actualOutputSign = value;
                else
                    throw new ArgumentOutOfRangeException("Sign must be either 1 or -1.");
            }
        }

        public int OutputSign => Sign.GetSign(ants.Sum(x => x.Sign));

        public double OutputValue => 1.0 * ants.Where(x => x.Sign == 1).Count() / Math.Max(ants.Count, 1);

        public void AddAnt(IAnt ant)
        {
            ants.Add(ant);
        }          
        
        public void PrepareToFire()
        {
            ants.Clear();
        }

        public void DistributeReward(double learningRate)
        {
            double reward = ActualOutputSign * learningRate;

            foreach (IAnt ant in ants)
            {
                ant.DistributeReward(reward);
            }
        }

        public void Fire()
        {           
        }

        public void AddNeighbour(INeuron neuron)
        {
        }
    }
}
